using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Dtos
{
    public class TreeMapDto
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public int Items { get; set; }
    }
}
