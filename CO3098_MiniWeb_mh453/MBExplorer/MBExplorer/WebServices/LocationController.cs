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
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private BookmarkManager BookmarkManager { get; set; }

        public LocationController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            BookmarkManager = new BookmarkManager();
        }

        // GET: service/textfile/create
        [HttpGet]
        [Route("create")]
        public IActionResult Create(string name, double latitude,
            double longitude, string path)
        {
            if (String.IsNullOrEmpty(name) || latitude == 0.0 ||
                longitude == 0.0 || String.IsNullOrEmpty(path))
            {
                return Ok(false);
            }

            var folders = _db.Folders.GetAll();

            var parent = BookmarkManager.GetBookmarkByPath(folders.ToBookmarks(), path);

            if (parent != null)
            {
                var bookmarks = _db.Bookmarks.GetBookmarksByParentId(parent.Id);

                var exists = bookmarks.Any(b => b.Name == name);

                if (!exists)
                {
                    _db.Items.Add(new ItemLocation(name, latitude,
                        longitude, parent.Id));

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

            return Ok((ItemLocation)bookmark);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, string name,
            double latitude, double longitude)
        {
            if (String.IsNullOrEmpty(name) || latitude == 0.0 || longitude == 0.0)
            {
                return Ok(false);
            }

            var bookmark = _db.Items.GetById(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            var parentBookmarks = _db.Bookmarks.GetBookmarksByParentId(bookmark.ParentId.Value);

            var exists = parentBookmarks.Any(b => b.Name == name && b.Id != bookmark.Id);

            if (exists)
            {
                return BadRequest();
            }

            var location = (ItemLocation)bookmark;

            location.Name = name;
            location.Latitude = latitude;
            location.Longitude = longitude;

            _db.Items.Update(location);
            _db.Complete();

            return Ok(true);
        }

    }
}
