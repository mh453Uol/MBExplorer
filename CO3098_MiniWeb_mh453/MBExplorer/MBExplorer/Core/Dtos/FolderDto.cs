using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Dtos
{
    public class FolderDto
    {
        public string Folder { get; set; }
        public List<FolderDto> SubFolder { get; set; }
    }
}
