function onViewBookmark() {
    var bookmark = getSelectedBookmark();

    if (bookmark.type === "ItemTextFile") {
        getTextFileAndPopulateModal(bookmark.id);
    }

    if (bookmark.type === "ItemLocation") {
        getLocationAndPopulateModal(bookmark.id);
    }
}

function onViewFolderProperties() {
    var path = getSelectedBookmarkPath();
    $.ajax({
        url: "/service/count?folder="+path,
        dataType: "JSON",
        beforeSend: function () {
            openModal("#js-folder-propertiesModal");
            $("#js-folder-properties-modal-content").hide();
            $("#js-folder-properties-modal-spinner").spin();
        },
        success: function (data) {
            $("#js-folder-properties-modal-spinner").fadeOut(function () {
                var view = $("#js-folder-properties-modal-content");
                $(view).find("#js-direct-folders").val(data.direct);
                $(view).find("#js-indirect-folders").val(data.indirect);
                var isReadOnly = getSelectedBookmark().isReadOnly;
                $(view).find("#js-readonly-attribute").prop("checked", isReadOnly);
                $(view).show();
            });
        }
    });
}

function onLinkView() {
    var selectedBookmark = getSelectedBookmark();
    var id = selectedBookmark.id;
    $.getJSON("/service/link/" + id, function (data) {
        window.open(data.url);
    });
}


