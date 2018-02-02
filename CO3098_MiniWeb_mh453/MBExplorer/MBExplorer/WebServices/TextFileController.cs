using System;
using Microsoft.AspNetCore.Mvc;
using MBExplorer.Core.Models;
using MBExplorer.Persistence;
using AutoMapper;
using MBExplorer.Core;
using MBExplorer.Utilities;
using System.Linq;

namespace MBExplorer.WebServices
{
    [Route("service/[controller]")]
    public class TextFileController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private BookmarkManager BookmarkManager { get; set; }

        public TextFileController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            BookmarkManager = new BookmarkManager();
        }

        // GET: service/textfile/create
        [HttpGet]
        [Route("create")]
        public IActionResult Create(string title, string content, string path)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(content) ||
                String.IsNullOrEmpty(path))
            {
                return Ok(false);
            }

            var folders = _db.Folders.GetAll();

            var parent = BookmarkManager.GetBookmarkByPath(folders.ToBookmarks(), path);

            if (parent != null)
            {
                var bookmarks = _db.Bookmarks.GetBookmarksByParentId(parent.Id);

                var exists = bookmarks.Any(b => b.Name == title);

                if (!exists)
                {
                    _db.Items.Add(new ItemTextFile(title, content, parent.Id));
                    _db.Complete();
                    return Ok(true);
                }
            }

            return Ok(false);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var bookmark = _db.Items.GetById(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            return Ok((ItemTextFile)bookmark);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, string title, string content)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(content))
            {
                return Ok(false);
            }

            var bookmark = _db.Items.GetById(id);

            if (bookmark == null)
            {
                return Ok(false);
            }

            var parentBookmarks = _db.Bookmarks.GetBookmarksByParentId(bookmark.ParentId.Value);

            var exists = parentBookmarks.Any(b => b.Name == title && b.Id != bookmark.Id);

            if (exists)
            {
                return Ok(false);
            }

            var textFile = (ItemTextFile)bookmark;

            textFile.Name = title;
            textFile.FileContent = content;

            _db.Items.Update(textFile);

            _db.Complete();

            return Ok(true);
        }
    }
}
