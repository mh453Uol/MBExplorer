using MBExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Persistence.Repository.Interface
{
    public interface IFolderRepository
    {
        List<Folder> GetAll();
        List<Folder> GetRootFolders();
        void Add(string name, int? parentId);
        Folder GetById(int id);
        void Add(Folder folder);
    }
}
