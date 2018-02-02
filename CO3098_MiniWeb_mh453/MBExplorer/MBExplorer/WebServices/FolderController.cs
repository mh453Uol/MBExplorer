using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBExplorer.Persistence;
using AutoMapper;
using MBExplorer.Core.Models;
using MBExplorer.Core;
using MBExplorer.Utilities;
using MBExplorer.Core.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MBExplorer.WebServices
{
    [Route("service")]
    public class FolderController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private BookmarkManager BookmarkManager { get; set; }

        public FolderController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            BookmarkManager = new BookmarkManager();
        }

        //GET: service/create?folder=Majid&parent=Family
        [Route("create")]
        [HttpGet]
        public IActionResult CreateFolder(string folder, string parent = null)
        {
            //To create a valid folder parent path must be valid and folder name is unique
            //with respect to its parent folder. If no parent is specified then add as a root folder.
            if (parent == null)
            {
                var created = AddRootFolder(folder);
                return Ok(created);
            }

            var folders = _db.Folders.GetAll();

            var parentFolder = BookmarkManager.GetBookmarkByPath(folders.ToBookmarks(), parent);

            //If the folder is null which means the path doesnt exist then return false
            if (parentFolder != null)
            {
                var bookmarks = _db.Bookmarks.GetBookmarksByParentId(parentFolder.Id);

                var exists = bookmarks.Any(f => f.Name == folder);

                if (!exists)
                {
                    _db.Folders.Add(folder, parentFolder.Id);
                    _db.Complete();

                    return Ok(true);
                }
            }
            return Ok(false);
        }

        // GET: service/delete?path=Documents
        [HttpGet("delete")]
        public IActionResult Delete(string folder)
        {
            if (folder == null)
            {
                return Ok(false);
            }

            var folders = _db.Bookmarks.GetAll();

            var parent = BookmarkManager.GetBookmarkByPath(folders, folder);

            if (parent == null)
            {
                return Ok(false);
            }

            //If bookmark has children its a folder then. 
            //We need to get all sub folders and items and delete them aswell.
            if (parent.Children != null)
            {
                var bookmarks = BookmarkManager.GetAllFoldersAndItems((Folder)parent);
                _db.Bookmarks.Delete(bookmarks);
            }
            else
            {
                _db.Bookmarks.Delete(new List<Bookmark> { parent });
            }
            _db.Complete();
            return Ok(true);
        }

        // GET: service/structure
        [HttpGet("structure")]
        public IActionResult Structure(string folder)
        {
            if (folder == null)
            {
                return Ok(false);
            }

            var folders = _db.Folders.GetAll();

            var folderStrucuture = BookmarkManager.GetBookmarkByPath(folders.ToBookmarks(), folder);

            var dto = _mapper.Map<FolderDto>(folderStrucuture);

            return Ok(dto);
        }

        [HttpGet("count")]
        public IActionResult GetCountOfDirectAndIndirectSubFolders(string folder = null)
        {
            if (folder == null)
            {
                return NoContent();
            }

            var folders = _db.Folders.GetAll();
            var root = BookmarkManager.GetBookmarkByPath(folders.ToBookmarks(), folder);
            var foldersAsList = BookmarkManager.GetAllFoldersAndItems((Folder)root);

            var dto = new
            {
                direct = root.Children.Count(),
                indirect = foldersAsList.Count() - 1
            };

            return Ok(dto);
        }

        //GET: service/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var folder = _db.Folders.GetById(id);

            if (folder == null)
            {
                return NotFound();
            }

            return Ok(folder);
        }

        // GET service/edit/10?name=Majid
        [HttpPost("edit/{folderId}")]
        public IActionResult Edit(int folderId, string name)
        {
            var folders = _db.Folders.GetAll();

            var folder = folders.SingleOrDefault(f => f.Id == folderId);

            if (folder == null) { return NotFound(); }

            if (folder.Parent != null)
            {
                //check that the parent doesnt have a folder with the
                //new name. Since folders must be unique.

                if (folder.Parent.Children.Any(f => f.Name == name))
                {
                    return BadRequest();
                }

            }

            folder.Name = name;

            _db.Complete();

            return Ok(folder);
        }

        [HttpPost("move")]
        public IActionResult MoveBookmark(int? bookmarkId = null, int? newParentId = null)
        {
            if (bookmarkId == null)
            {
                return NoContent();
            }

            var bookmark = _db.Bookmarks.GetById(bookmarkId.Value);

            if (bookmark == null)
            {
                return NotFound();
            }

            bookmark.ParentId = newParentId;

            _db.Complete();

            return Ok(true);
        }

        [HttpGet("createStructure")]
        public IActionResult CreateStructure(string tree, string root)
        {
            //This function takes in string tree so 
            //tree=[Books[Fiction[Romance|Horror]|NonFiction]]&root=Documents
            //We take this string and create a tree.

            //Refactor..
            try
            {
                var folder = BookmarkManager.StringTreeParser(tree);

                if (folder == null)
                {
                    return Ok(false);
                }

                var folders = _db.Folders.GetAll().ToBookmarks();

                var rootFolder = BookmarkManager.GetBookmarkByPath(folders, root);

                if (rootFolder == null)
                {
                    return Ok(false);
                }

                //Specify as root
                folder.ParentId = rootFolder.Id;
                folder.Parent = null;

                _db.Folders.Add(folder);

                _db.Complete();
            }
            catch (Exception)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpPost("readonly")]
        public IActionResult ReadOnly(bool isreadonly, string path)
        {
            var bookmarks = _db.Bookmarks.GetAll();

            var parent = BookmarkManager.GetBookmarkByPath(bookmarks, path);

            var directory = BookmarkManager.GetAllFoldersAndItems((Folder)parent);

            foreach (Bookmark bookmark in directory)
            {
                bookmark.ReadOnly = isreadonly;
            }

            _db.Bookmarks.UploadRange(directory);

            _db.Complete();

            return Ok(directory);

        }
        public bool AddRootFolder(string folder)
        {
            var exists = _db.Folders.GetRootFolders().Any(f => f.Name == folder);

            if (!exists)
            {
                _db.Folders.Add(folder, null);
                _db.Complete();

                return true;
            }

            return false;
        }

    }
}
