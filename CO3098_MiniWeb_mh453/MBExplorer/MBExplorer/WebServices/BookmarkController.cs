using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBExplorer.Core.Models;
using MBExplorer.Persistence;
using AutoMapper;
using MBExplorer.Core.Dtos;
using MBExplorer.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MBExplorer.WebServices
{
    [Route("api/[controller]")]
    public class BookmarkController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private BookmarkManager BookmarkManager { get; set; }
        public BookmarkController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bookmarks = _db.Bookmarks.GetAll();

            if (bookmarks == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<IEnumerable<BookmarkDto>>(bookmarks);

            return Ok(dto);
        }

        [HttpGet("treemap")]
        public IActionResult GetFromTreeMap()
        {
            var bookmarks = _db.Bookmarks.GetAll();

            var folders = bookmarks.OfType<Folder>().ToList();

            List<TreeMapDto> dto = new List<TreeMapDto>();

            foreach (Folder folder in folders)
            {
                dto.Add(new TreeMapDto()
                {
                    Name = folder.Name,
                    Parent = folder?.Parent?.Name,
                    Items = folder.Children.Count + 1
                });
            }

            return Ok(dto);
        }

        [HttpGet("orgchart")]
        public IActionResult OrganisationalChart()
        {
            var folders = _db.Folders.GetAll();

            List<OrgDto> dto = new List<OrgDto>();

            foreach (Folder folder in folders)
            {
                dto.Add(new OrgDto()
                {
                    Name = folder.Name,
                    Parent = folder.Parent == null ? "" : folder.Parent.Name
                });
            }

            return Ok(dto);
        }

    }
}
