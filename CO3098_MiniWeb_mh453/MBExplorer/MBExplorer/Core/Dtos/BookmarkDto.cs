using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Dtos
{
    public class BookmarkDto
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
