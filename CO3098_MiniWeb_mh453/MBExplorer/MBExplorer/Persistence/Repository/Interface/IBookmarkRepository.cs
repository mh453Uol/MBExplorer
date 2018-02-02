using MBExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Persistence.Repository.Interface
{
    public interface IBookmarkRepository
    {
        List<Bookmark> GetAll();
        void Delete(List<Bookmark> bookmarks);
        Bookmark GetById(int id);
        void UploadRange(List<Bookmark> bookmarks);
        List<Bookmark> GetBookmarksByParentId(int parent);
    }

}
