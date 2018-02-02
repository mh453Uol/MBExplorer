using MBExplorer.Persistence.Repository.Interface;

namespace MBExplorer.Persistence
{
    public interface IUnitOfWork
    {
        IBookmarkRepository Bookmarks { get; set; }
        IFolderRepository Folders { get; set; }
        IItemRepository Items { get; set; }

        void Complete();
    }
}
