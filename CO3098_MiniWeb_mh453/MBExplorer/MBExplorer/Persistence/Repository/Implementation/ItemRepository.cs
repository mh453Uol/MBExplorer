using MBExplorer.Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBExplorer.Core.Models;

namespace MBExplorer.Persistence.Repository.Implementation
{
    public class ItemRepository : IItemRepository
    {
        private readonly BookmarkDbContext _db;
        public ItemRepository(BookmarkDbContext db)
        {
            _db = db;
        }

        public void Add(Item item)
        {
            _db.Bookmarks.Add(item);
        }

        public Item GetById(int id)
        {
            return _db.Bookmarks.OfType<Item>().SingleOrDefault(i => i.Id == id);
        }

        public void Update(Item item)
        {
            _db.Update(item);
        }
    }
}
