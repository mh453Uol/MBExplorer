using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Models
{
    public abstract class Bookmark
    {
        public int Id { get; set; }
        public bool ReadOnly { get; set; }
        public int? ParentId { get; set; }
        public Bookmark Parent { get; set; }
        public List<Bookmark> Children { get; set; }
        public String Name { get; set; }
    }
}
