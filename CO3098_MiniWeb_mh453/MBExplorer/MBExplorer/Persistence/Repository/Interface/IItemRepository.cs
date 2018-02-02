using MBExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Persistence.Repository.Interface
{
    public interface IItemRepository
    {
        void Add(Item item);
        Item GetById(int id);
        void Update(Item item);
    }
}
