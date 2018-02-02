using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Models
{
    public class ItemLink : Item
    {
        public ItemLink() { }

        public ItemLink(String name, String url, int? parentId)
        {
            Name = name;
            URL = url;
            ParentId = parentId;
        }

        public String URL { get; set; }
    }
}
