using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Models
{
    public class ItemTextFile : Item
    {
        public ItemTextFile() { }
        public ItemTextFile(string name, string content, int? parentId)
        {
            Name = name;
            FileContent = content;
            ParentId = parentId;
        }

        public String FileContent { get; set; }
    }
}
