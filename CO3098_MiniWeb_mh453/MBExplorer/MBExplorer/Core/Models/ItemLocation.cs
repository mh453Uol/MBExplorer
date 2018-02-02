using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core.Models
{
    public class ItemLocation : Item
    {
        public ItemLocation() { }
        public ItemLocation(String name, double latitude,
            double longitude, int? parentId)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            ParentId = parentId;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}