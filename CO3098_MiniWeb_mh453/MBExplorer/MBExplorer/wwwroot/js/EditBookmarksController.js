function onEdit() {

    var bookmark = getSelectedBookmark();

    // When folder is set to readonly dont allow user to edit new item - link, textfile, location
    if (bookmark.type === "ItemLink" || bookmark.type === "ItemTextFile" || bookmark.type === "ItemLocation") {

        if (isParentFolderReadOnly()) {
            // when readonly dont allow adding of items
            openModal("#js-readonlyModal");
            return;
        }
    }

    setPath();

    switch (bookmark.type) {
        case "Folder":
            getFolderAndPopulateEditModal(bookmark.id);
            break;
        case "ItemLink":
            getLinkItemAndPopulateEditModal(bookmark.id);
            break;
        case "ItemLocation":
            getLocationItemAndPopulateEditModal(bookmark.id);
            break;
        case "ItemTextFile":
            getTextFileItemAndPopulateEditModal(bookmark.id);
            break;
    }
}

function getFolderAndPopulateEditModal(id) {
    $.ajax({
        url: "/service/" + id,
        dataType: "JSON",
        beforeSend: function () {
            openModal("#js-edit-folderModal");
            $("#js-edit-folder-modal-content").hide();
            $("#js-edit-folder-modal-spinner").spin();
        },
        success: function (data) {
            $("#js-edit-folder-modal-spinner").fadeOut(function () {
                var view = $("#js-edit-folder-modal-content");
                $(view).find("#js-name").val(data.name);
                $(view).show();
            });
        }
    });
}

function onFolderEdit() {
    $("#js-edit-folderModal form").on("submit", function (e) {
        e.preventDefault();

        var queryString = decodeURIComponent($(this).serialize());
        var folder = getSelectedBookmark();

        $.ajax({
            url: "/service/edit/" + folder.id + "?" + queryString,
            dataType: "JSON",
            method: "POST",
            success: function () {
                closeModal("#js-edit-folderModal");
                resetModal("#js-edit-folderModal");
                getBookmarks(refreshTree);
            },
            error: function () {
                alert("Cant edit folder since its name might not be unique!");
            }
        });
    });
}

function getLinkItemAndPopulateEditModal(id) {
    $.ajax({
        url: "/service/link/" + id,
        dataType: "JSON",
        beforeSend: function () {
            openModal("#js-edit-linkModal");
            $("#js-edit-link-modal-content").hide();
            $("#js-edit-link-modal-spinner").spin();
        },
        success: function (data) {
            console.log(data);
            $("#js-edit-link-modal-spinner").fadeOut(function () {
                var view = $("#js-edit-link-modal-content");
                $(view).find("#js-name").val(data.name);
                $(view).find("#js-URL").val(data.url);
                $(view).show();
            });
        },
        error: function () {
            alert("Cant edit folder since its name might not be unique!");
        }
    });
}

function onLinkEdit() {
    $("#js-edit-linkModal form").on("submit", function (e) {
        e.preventDefault();

        var queryString = decodeURIComponent($(this).serialize());
        var link = getSelectedBookmark();

        $.ajax({
            url: "/service/link/edit/" + link.id + "?" + queryString,
            dataType: "JSON",
            method: "POST",
            success: function () {
                closeModal("#js-edit-linkModal");
                resetModal("#js-edit-linkModal");
                getBookmarks(refreshTree);
            },
            error: function () {
                alert("Cant edit folder since its name might not be unique!");
            }
        });
    });
}

function getTextFileItemAndPopulateEditModal(id) {
    $.ajax({
        url: "/service/textfile/" + id,
        dataType: "JSON",
        beforeSend: function (xhr) {
            openModal("#js-edit-textFileModal");
            $("#js-edit-textFile-modal-content").hide();
            $("#js-edit-textFile-modal-spinner").spin();
        },
        success: function (data) {
            $("#js-edit-textFile-modal-spinner").fadeOut(function () {
                var view = $("#js-edit-textFile-modal-content");
                $(view).find("#js-title").val(data.name);
                $(view).find("#js-content").val(data.fileContent);
                $(view).find(".js-from-parent").val(getSelectedBookmarkPath());
                $(view).show();
            });
        },
        error: function () {
            alert("Cant edit folder since its name might not be unique!");
        }
    });
}

function onTextFileEdit() {
    $("#js-edit-textFileModal form").on("submit", function (e) {
        e.preventDefault();

        var queryString = decodeURIComponent($(this).serialize());
        var link = getSelectedBookmark();

        $.ajax({
            url: "/service/textfile/edit/" + link.id + "?" + queryString,
            dataType: "JSON",
            method: "POST",
            success: function () {
                closeModal("#js-edit-textFileModal");
                resetModal("#js-edit-textFileModal");
                getBookmarks(refreshTree);
            },
            error: function () {
                alert("Cant edit folder since its name might not be unique!");
            }
        });
    });
}

function getLocationItemAndPopulateEditModal(id) {
    $.ajax({
        url: "/service/location/" + id,
        dataType: "JSON",
        beforeSend: function () {
            openModal("#js-edit-locationModal");
            $("#js-edit-location-modal-content").hide();
            $("#js-edit-location-modal-spinner").spin();
        },
        success: function (data) {
            $("#js-edit-location-modal-spinner").fadeOut(function () {
                console.log(data);
                var view = $("#js-edit-location-modal-content");
                $(view).find("#js-name").val(data.name);
                $(view).find("#js-latitude").val(data.latitude);
                $(view).find("#js-longitude").val(data.longitude);
                $(view).find(".js-from-parent").val(getSelectedBookmarkPath());
                $(view).show();
            });
        }
    });
}

function onLocationEdit() {
    $("#js-edit-locationModal form").on("submit", function (e) {
        e.preventDefault();

        var queryString = decodeURIComponent($(this).serialize());
        var location = getSelectedBookmark();

        $.ajax({
            url: "/service/location/edit/" + location.id + "?" + queryString,
            dataType: "JSON",
            method: "POST",
            success: function () {
                closeModal("#js-edit-locationModal");
                resetModal("#js-edit-locationModal");
                getBookmarks(refreshTree);
            },
            error: function () {
                alert("Cant edit folder since its name might not be unique!");
            }
        });
    });
}