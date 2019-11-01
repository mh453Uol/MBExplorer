function buildTree(data) {
    $("#js-directory").jstree({
        core: {
            data: data,
            check_callback: function (operation, node, parent, position, more) {
                if (operation == "move_node") {
                    // Only move if the parent has changed
                    if (parent.id === node.parent) {
                        return false;
                    }

                    // When creating a new root node has to be a folder
                    if (parent.id === "#" && node.original.type === "Folder") {
                        console.log("Creating a new root folder");
                        return true;
                    }

                    // You can only move a folder or item to a folder only.
                    if (parent.original.type === "Folder") {
                        return true;
                    }

                    return false;
                }
            }
        },
            theme: { "stripes": true },
            plugins: [
                "contextmenu", "dnd", "search", "state", "types", "wholerow"
            ],
            contextmenu: { items: customContextMenu }
        });
    deselectAll();

    if (data.length) {
        hideRootButton();
    } else {
        showRootButton();
    }
    $("#js-loading-content").hide();
}

function showRootButton() {
    $("#js-root-create-button").removeClass("hidden");
}

function hideRootButton() {
    $("#js-root-create-button").addClass("hidden");
}

function refreshTree(data) {
    $('#js-directory').jstree(true).settings.core.data = data;
    $('#js-directory').jstree(true).refresh();
    deselectAll();
    if (data.length) {
        hideRootButton();
    } else {
        showRootButton();
    }
}

function setIconAndState(bookmark) {
    switch (bookmark.type) {
        case "ItemLink":
            bookmark.icon = "glyphicon glyphicon-link";
            break;
        case "ItemLocation":
            bookmark.icon = "glyphicon glyphicon-map-marker";
            break;
        case "ItemTextFile":
            bookmark.icon = "glyphicon glyphicon-text-background";
            break;
    }

    bookmark.state = { opened: false, selected: false };

    return bookmark;
}

function getSelectedBookmarkPath() {
    var tree = $("#js-directory").jstree(true);
    var selected = tree.get_path(tree.get_selected(), "|");
    return selected;
}

function getSelectedBookmarkType() {
    var tree = $("#js-directory").jstree(true);
    var bookmarks = tree.settings.core.data;
    var selected = tree.get_selected();
    var selectedBookmark = bookmarks.find(b => b.id == selected);

    return selectedBookmark.type;
}

function getSelectedBookmark() {
    var tree = $("#js-directory").jstree(true);
    var bookmarks = tree.settings.core.data;
    var selected = tree.get_selected();
    var selectedBookmark = bookmarks.find(b => b.id == selected);

    return selectedBookmark;
}

function getTypeOfBookmark(e, data) {

    var bookmark = data.node.original;

    if (bookmark) {
        return bookmark.type;
    }
}

function deselectAll() {
    $("#js-directory").jstree("deselect_all");
}

function customContextMenu(node) {
    // The default set of all items
    var items = {
        view: {
            label: "View",
            action: onViewBookmark
        },
        create: {
            label: "Create",
            action: false,
            submenu: {
                folder: {
                    label: "Folder",
                    action: false,
                    submenu: {
                        root: {
                            label: "Root Folder",
                            action: function () {
                                onAddBookmark("RootFolder");
                            }
                        },
                        current: {
                            label: "In Current Folder",
                            action: function () {
                                onAddBookmark("Folder");
                            }
                        }
                    }
                },
                items: {
                    label: "Item",
                    action: false,
                    submenu: {
                        link: {
                            label: "Link",
                            action: function () {
                                onAddBookmark("Link");
                            }
                        },
                        textFile: {
                            label: "Text File",
                            action: function () {
                                onAddBookmark("TextFile");
                            }
                        },
                        location: {
                            label: "Location",
                            action: function () {
                                onAddBookmark("Location");
                            }
                        }
                    }
                }
            }
        },
        link: {
            label: "Open Link In New Tab",
            action: onLinkView
        },
        edit: {
            label: "Edit",
            action: onEdit
        },
        delete: {
            label: "Delete",
            action: onDeleteBookmark
        },
        properties: {
            label: "Properties",
            action: onViewFolderProperties
        }
    };

    var node = $(node);

    var type = node[0].original.type;

    console.log(type);

    if (type == "Folder") {
        delete items.view;
        delete items.link;
    } else {
        delete items.create;
        delete items.properties;

        if (type === "ItemLink") {
            delete items.view;
        } else {
            delete items.link;
        }
    }
    return items;
}

function getNodeById(id) {
    return $('#js-directory').jstree(true).get_node(id);
}

function onSearch() {
    $("#js-search").keyup(function () {
        var search = $(this).val();
        $("#js-directory").jstree("search", search);
    });
}
