function setPath() {
    var path = getSelectedBookmarkPath();

    if (path) {
        $(".js-from-parent").val(path);

        var currentFolder = path.split("|");

        if (!currentFolder) {
            $("#js-current-folder").text("Root");
        } else {
            $("#js-current-folder").text(currentFolder[currentFolder.length - 1]);
        }
    }
}

function resetPath() {
    $(".js-from-parent").val("");
}

function closeModal(modal) {
    $(modal + " .close").click();
}

function resetModal(modal) {
    $(modal + " input").val("");
}

function openModal(modal) {
    $(modal).modal("toggle");
}

