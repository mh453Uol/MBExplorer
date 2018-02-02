function onMoveBookmark() {
    $("#js-directory").bind("move_node.jstree", function (e, data) {

        var bookmark = data.node;
        var parent = getNodeById(data.node.parent);

        if (parent.id == "#") {
            parent.text = "Root"
        }

        var movingBookmark = $("#js-current-bookmark");
        var newParentId = $("#js-move-to-folder");

        $(movingBookmark).text(data.node.text);
        $(movingBookmark).attr("bookmark-id", bookmark.id);

        $(newParentId).text(parent.text);
        $(newParentId).attr("new-parent-id",parent.id);
    
        openModal("#js-moveModal");
    });
}

function moveBookmark() {
    $("#js-move").click(function () {

        var bookmarkId = $("#js-current-bookmark").attr("bookmark-id");
        var newParentId = $("#js-move-to-folder").attr("new-parent-id");

        $.ajax({
            url: "/service/move?bookmarkId=" + bookmarkId + "&newParentId=" + newParentId,
            method: "POST",
            success: function (data) {
                if (data) {
                    closeModal("#js-moveModal");
                    getBookmarks(refreshTree);
                } else {
                    alert("Error");
                }
            },
            error: function () {
                alert("Error");
            }
        });
    });
}

function onCancelMovingBooking() {
    refreshTree();
}

function getBookmarkType(type) {
    if (type === "Folder") {
        return type;
    }

    if (type === "ItemLink") {
        return "Link";
    }

    if (type === "ItemLocation") {
        return "Location";
    }

    if (type === "ItemTextFile") {
        return "Text File";
    }
}
