using MBExplorer.Core.Models;
using MBExplorer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Core
{
    public class BookmarkManager
    {
        public List<Folder> BuildDirectory(List<Bookmark> bookmarks)
        {
            var bookmarksById = bookmarks.ToDictionary(f => f.Id);

            var childrenGroupedByParent = bookmarks
                .GetParentFolders()
                .GroupBy(f => f.ParentId);

            foreach (IGrouping<int?, Bookmark> group in childrenGroupedByParent)
            {
                int? parentId = group.Key;

                if (bookmarksById.ContainsKey(parentId.Value))
                {
                    var parent = bookmarksById[parentId.Value];
                    parent.Children = group.ToList();
                }
            }

            return bookmarks.GetRootFolders();
        }
        public Bookmark TraverseToFindBookmark(Bookmark node, string name)
        {
            Bookmark to_return = null;

            if (node == null) { return null; }

            if (node.Name == name) { return node; }

            var isFoundFolder = node.Children.SingleOrDefault(f => f.Name == name);

            if (isFoundFolder != null) { return isFoundFolder; }

            foreach (Bookmark subfolder in node.Children)
            {
                var x = TraverseToFindBookmark(subfolder, name);

                if (x != null)
                {
                    to_return = x;
                }
            }

            return to_return;
        }
        public Bookmark GetBookmarkByPath(List<Bookmark> folders, string path)
        {
            var tree = BuildDirectory(folders);

            var paths = path.Split("|");

            if (paths == null)
            {
                throw new ArgumentException("Path cant be null");
            }

            var root = (Bookmark)tree.SingleOrDefault(r => r.Name == paths[0]);

            if (root == null) { return null; }

            foreach (String pathSection in paths)
            {
                root = TraverseToFindBookmark(root, pathSection);
            }

            return root;
        }
        public List<Bookmark> GetAllFoldersAndItems(Folder folder)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();

            if (folder != null)
            {
                foreach (Bookmark bookmarksItems in folder.Children)
                {
                    List<Bookmark> insideFolders = GetSubFoldersAndItems(bookmarksItems);
                    bookmarks.AddRange(insideFolders);
                }

                bookmarks.Add(folder);
            }

            return bookmarks;
        }
        public List<Bookmark> GetSubFoldersAndItems(Bookmark folder)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();

            if (folder == null) { return bookmarks; }

            if (folder.Children == null)
            {
                bookmarks.Add(folder);
                return bookmarks;
            }

            foreach (Bookmark parent in folder.Children)
            {
                bookmarks.AddRange(GetSubFoldersAndItems(parent));
            }

            bookmarks.Add(folder);

            return bookmarks;
        }
        public Folder StringTreeParser(string tree)
        {
            var word = "";
            int index = -1;
            Folder current = null;

            foreach (char letter in tree)
            {
                switch (letter)
                {
                    case '[':
                        if (index > -1)
                        {
                            if (current == null)
                            {
                                current = new Folder() { Name = word };

                                current.Parent = current;
                            }
                            else
                            {
                                var node = new Folder()
                                {
                                    Name = word,
                                    Parent = current
                                };
                                current.Children.Add(node);
                                current = node;
                            }
                        }
                        word = "";
                        index++;
                        break;
                    case ']':
                        if (word != "")
                        {
                            current.Children.Add(new Folder() { Name = word, Parent = current });
                            current = (Folder)current.Parent;
                        }
                        index--;
                        word = "";
                        break;
                    case '|':
                        if (word != "")
                        {
                            current.Children.Add(new Folder() { Name = word, Parent = current });
                        }
                        word = "";
                        break;
                    default:
                        word += letter;
                        break;
                }

            }

            if (index == -1)
            {
                return current;
            }
            else
            {
                return null;
            }
        }
    }
}
