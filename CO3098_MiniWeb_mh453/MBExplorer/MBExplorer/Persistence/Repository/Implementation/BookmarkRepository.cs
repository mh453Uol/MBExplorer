using MBExplorer.Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBExplorer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MBExplorer.Persistence.Repository.Implementation
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly BookmarkDbContext _db;
        public BookmarkRepository(BookmarkDbContext db)
        {
            _db = db;
        }

        public void Delete(List<Bookmark> bookmarks)
        {
            _db.Bookmarks.RemoveRange(bookmarks);
        }

        public List<Bookmark> GetAll()
        {
            return _db.Bookmarks.ToList();
        }

        public List<Bookmark> GetBookmarksByParentId(int parent)
        {
            return _db.Bookmarks.Where(p => p.ParentId == parent).AsNoTracking().ToList();
        }

        public Bookmark GetById(int id)
        {
            return _db.Bookmarks.SingleOrDefault(b => b.Id == id);
        }

        public void UploadRange(List<Bookmark> bookmarks)
        {
            _db.UpdateRange(bookmarks);
        }


    }
}
