function onDeleteBookmark() {
    setPath();
    openModal("#deleteModal");
}


function deleteBookmark() {
    $("#js-delete").on("click", function () {
        var path = decodeURIComponent($(".js-from-parent").val());

        $.getJSON("/service/delete?folder=" + path, function (success) {
            if (success) {
                closeModal("#deleteModal");
                resetModal("#deleteModal");
                getBookmarks(refreshTree);
            } else {
                alert("Something went wrong..");
            }
        });
    });
}