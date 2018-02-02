using MBExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Utilities
{
    public static class BookmarkExtensionMethod
    {
        public static List<Folder> GetRootFolders(this List<Bookmark> folders)
        {
            var root = folders.Where(f => f.ParentId == null).OfType<Folder>().ToList();

            if (root == null)
            {
                throw new ArgumentException("Collection doesnt contain a parent");
            }

            return root;
        }

        public static List<Bookmark> GetParentFolders(this List<Bookmark> tree)
        {
            return tree.Where(f => f.ParentId != null).ToList();
        }

        public static List<Bookmark> ToBookmarks(this List<Folder> folders)
        {
            return folders.Cast<Bookmark>().ToList();
        }
    }
}
