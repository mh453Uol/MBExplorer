using MBExplorer.Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBExplorer.Core.Models;

namespace MBExplorer.Persistence.Repository.Implementation
{
    public class FolderRepository : IFolderRepository
    {
        private readonly BookmarkDbContext _db;
        public FolderRepository(BookmarkDbContext db)
        {
            _db = db;
        }

        public List<Folder> GetAll()
        {
            var folders = _db.Bookmarks.OfType<Folder>().ToList();

            return folders;
        }

        public List<Folder> GetRootFolders()
        {
            return _db.Bookmarks.Where(f => f.ParentId == null)
                    .OfType<Folder>()
                    .ToList();
        }

        public void Add(string name, int? parentId)
        {
            _db.Bookmarks.Add(new Folder(name, parentId));
        }

        public Folder GetById(int id)
        {
            return _db.Bookmarks.OfType<Folder>().SingleOrDefault(f => f.Id == id);
        }

        public void Add(Folder folder)
        {
            _db.Add(folder);
        }
    }
}
