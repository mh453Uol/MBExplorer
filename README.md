# MBExplorer
Online bookmark manager which has characteristics like a file system. Allows you organise bookmark into folders. Support bookmarking links, location and text files!

MyBookmark Explorer (MBExplorer) is an online bookmark manager that can be used to store your
favourite items. There are three types of bookmark items that can be added: (1) links (2) text files, and (3)
locations. A user can create folders and sub-folders and uses them to organize items.  

Hosted on a FREE tier server - http://mbexplorer.apphb.com/ 

# Features

## Folders

a) Displays the folder structure in a TreeView. 
b) Allows you to create, edit and delete folders from the TreeView.
c) TreeView is refreshed when the folder structure changes.

## Items

a) Allows you to create, view, edit and delete bookmark items inside a folder from the TreeView. Apart from the
operations with folders, the user is be able to create bookmark items. There are three kinds of
items: (1) Links, (2) Text files, and (3) Locations.
1. Link - Displays title and URL
2. Text file - Displays title and plain text content
3. Location Displays place name and its GPS coordinate and locations displayed on Google Map 

## Miscellaneous

a) User is a able to lock/unlock folder – recursively set/unset read-only attribute for all items and its sub-folders.
1. No item can be added to the folder when set to ready-only.
2. An item becomes not editable when it is read-only.
g) User is able to search folders/items, filter by item types and keywords (title and content)
h) Use is able to move folders/items.

## Implementation

1. Since a bookmark manager has a file system charectaristics, I had to use recursion to build up structure. 
2. Database uses Adjacency List so folders and bookmark items have a parent Id column which refers to ther parents. 

# Web Api

MBExplorer allows users to create and edit bookmark folders, as well as organise them into a hierarchy.
```
(a) GET /service/create?folder=Romance&parent=Documents|Books|Fiction
```
Add a new bookmark folder to an existing folder. This request returns true if the operation is successful,
otherwise false are returned (if a folder with the given name already exists or the parent folder does not
exist). If the parent folder is not explicitly specified then the folder to be added will become a top-level
folder. Consider the example above. This request will create a new folder called Romance inside
/Documents/Books/Fiction folder.

```
GET /service/create?folder=Documents
GET /service/create?folder=Books&parent=Documents
GET /service/create?folder=Holidays&parent=Documents
GET /service/create?folder=Fiction&parent=Documents|Books
GET /service/create?folder=NonFiction&parent=Documents|Books
GET /service/create?folder=Romance&parent=Documents|Books|Fiction
GET /service/create?folder=Horror&parent=Documents|Books|Fiction
GET /service/create?folder=Asia&parent=Documents|Holidays
GET /service/create?folder=Europe&parent=Documents|Holidays
```
The following folder structure will be created

```
Documents
 |
 + Books
 | |
 | + Fiction
 | | |
 | | + Romance
 | | |
 | | + Horror
 | |
 | + NonFiction
 |
 + Holidays
 |
 + Asia
 |
 + Europe
 ```
```
(b) GET /service/delete?folder=Documents|Books
```
Delete a bookmark folder from the directory structure. Any sub-folders or items kept in this folder (including
items in its sub-folders) will be deleted too. For example, the request above will delete the folder “Books”
and all its sub-folders including “Fiction”, “Romance”, “Horror” and “NonFiction”, and any items
inside them will also be deleted. The request returns true if the operation is successful, otherwise
false is returned. 
```
(c) GET /service/structure?folder=Documents|Books
```
Returns a hierarchical JSON string reflects the bookmark folder structure. For example, the request above
returns a JSON object in response:
```json
{
  "folder": "Books",
  "subfolders": [{
  "folder": "Fiction",
  "subfolders": [{
  "folder": "Romance"
  }, {
  "folder": "Horror"
  }]
  }, {
  "folder": "NonFiction"
  }]
}
```
```
(d) GET /service/count?folder=Documents|Books
```

Returns the number of direct and indirect sub-folders inside a given folder
```json
{"direct":"2","indirect":"4"}
```
```
(e) GET
/service/createStructure?tree=[Books[Fiction[Romance|Horror]|NonFiction]]&root=Documents
```
This service constructs a sub-folder from the given string. For example, the above GET request adds a subtree
hierarchy to the “Documents” folder. The service returns true if the operation is successful, otherwise
false is returned. Uses a a simple string parser using a stack.

