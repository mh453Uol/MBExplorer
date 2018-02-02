function onAddBookmark(type) {
    // When folder is set to readonly dont allow user to add new item - link, textfile, location
    if (type === "Link" || type === "TextFile"|| type === "Location") {

        if (isParentFolderReadOnly()) {
            // when readonly dont allow adding of items
            openModal("#js-readonlyModal");
            return;
        }
    }

    switch (type) {
        case "RootFolder":
            resetPath();
            openModal("#folderModal");
            break;
        case "Folder":
            setPath();
            openModal("#folderModal");
            break;
        case "Link":
            setPath();
            openModal("#linkModal");
            break;
        case "TextFile":
            isParentFolderReadOnly();
            setPath();
            openModal("#textFileModal");
            break;
        case "Location":
            isParentFolderReadOnly();
            setPath();
            openModal("#locationModal");
            break;
    }
}

function onFolderAdd() {
    $("#folderModal form").on("submit", function (e) {
        e.preventDefault();
        var queryString = decodeURIComponent($(this).serialize());

        $.getJSON("/service/create?" + queryString, function (success) {

            if (success) {
                closeModal("#folderModal");
                resetModal("#folderModal");
                getBookmarks(refreshTree);
            } else {
                alert("Cant add folder since its name might not be unique!");
            }

        });
    });
}

function onItemLinkAdd() {

    $("#linkModal form").on("submit", function (e) {
        e.preventDefault();
        var queryString = decodeURIComponent($(this).serialize());

        $.getJSON("/service/link/create?" + queryString, function (success) {

            if (success) {
                closeModal("#linkModal");
                resetModal("#linkModal");
                getBookmarks(refreshTree);
            } else {
                alert("Cant add folder since its name might not be unique!");
            }

        });
    });
}

function onItemTextFileAdd() {

    $("#textFileModal form").on("submit", function (e) {
        e.preventDefault();
        var queryString = decodeURIComponent($(this).serialize());

        $.getJSON("/service/textfile/create?" + queryString, function (success) {

            if (success) {
                closeModal("#textFileModal");
                resetModal("#textFileModal");
                getBookmarks(refreshTree);
            } else {
                alert("Cant add folder since its name might not be unique!");
            }

        });
    });
}

function onItemLocationAdd() {

    $("#locationModal form").on("submit", function (e) {
        e.preventDefault();
        var queryString = decodeURIComponent($(this).serialize());

        $.getJSON("/service/location/create?" + queryString, function (success) {

            if (success) {
                closeModal("#locationModal");
                resetModal("#locationModal");
                getBookmarks(refreshTree);
            } else {
                alert("Cant add folder since its name might not be unique!");
            }

        });
    });
}

function isParentFolderReadOnly() {
    var selected = getSelectedBookmark();
    return selected.isReadOnly;
}