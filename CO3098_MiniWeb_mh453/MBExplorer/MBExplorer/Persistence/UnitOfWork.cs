using MBExplorer.Persistence;
using MBExplorer.Persistence.Repository.Interface;

namespace MBExplorer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookmarkDbContext _db;
        public IBookmarkRepository Bookmarks { get; set; }
        public IFolderRepository Folders { get; set; }
        public IItemRepository Items { get; set; }

        public UnitOfWork(IBookmarkRepository bookmark,
            IFolderRepository folders,
            IItemRepository items,
            BookmarkDbContext db)
        {
            Bookmarks = bookmark;
            Folders = folders;
            Items = items;
            _db = db;
        }

        public void Complete()
        {
            _db.SaveChanges();
        }
    }
}
