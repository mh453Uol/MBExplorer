using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Models
{
    public class Folder : Bookmark
    {
        public Folder()
        {
            Children = new List<Bookmark>();
        }

        public Folder(String name, int? parentId) : this()
        {
            this.Name = name;
            this.ParentId = parentId;
        }
    }

}
