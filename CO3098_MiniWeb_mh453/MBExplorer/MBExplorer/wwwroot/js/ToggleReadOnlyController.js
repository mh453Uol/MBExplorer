function onReadOnlyToggle() {
    $("#js-readonly").click(function () {
        var isReadOnlyApplied = $("#js-readonly-attribute").is(":checked");
        var path = getSelectedBookmarkPath();
        $.ajax({
            url: "/service/readonly?isreadonly=" + isReadOnlyApplied + "&path=" + path,
            method: "POST",
            success: function (data) {
                if (data) {
                    closeModal("#js-folder-propertiesModal");
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