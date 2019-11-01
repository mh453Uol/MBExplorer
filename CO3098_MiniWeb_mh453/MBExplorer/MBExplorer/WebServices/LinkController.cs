using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBExplorer.Core.Models;
using MBExplorer.Core.Dtos;
using MBExplorer.Persistence;
using AutoMapper;
using MBExplorer.Core;
using MBExplorer.Utilities;

namespace MBExplorer.WebServices
{
    [Route("service/[controller]")]
    public class LinkController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private BookmarkManager BookmarkManager { get; set; }

        public LinkController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            BookmarkManager = new BookmarkManager();
        }

        // POST: service/link/create?
        [HttpPost]
        [Route("create")]
        public IActionResult Create(string title, string URL, string path)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(URL) ||
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
                    _db.Items.Add(new ItemLink(title, URL, parent.Id));
                    _db.Complete();
                    return Ok(true);
                }
            }

            return Ok(false);
        }

        //GET: service/link/21
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var bookmark = _db.Items.GetById(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            return Ok((ItemLink)bookmark);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, string title, string URL)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(URL))
            {
                return Ok(false);
            }

            var bookmark = _db.Items.GetById(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            var parentBookmarks = _db.Bookmarks.GetBookmarksByParentId(bookmark.ParentId.Value);

            var exists = parentBookmarks.Any(b => b.Name == title && b.Id != bookmark.Id);

            if (exists)
            {
                return BadRequest();
            }

            var link = (ItemLink)bookmark;

            link.Name = title;
            link.URL = URL;

            _db.Items.Update(link);

            _db.Complete();

            return Ok(true);
        }
    }
}
