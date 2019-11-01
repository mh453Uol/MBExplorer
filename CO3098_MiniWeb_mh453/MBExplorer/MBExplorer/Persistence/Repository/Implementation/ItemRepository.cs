using MBExplorer.Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBExplorer.Core.Models;
using Microsoft.EntityFrameworkCore;

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
            return (Item) _db.Bookmarks.First(i => i.Id == id);
        }

        public void Update(Item item)
        {
            _db.Update(item);
        }
    }
}
