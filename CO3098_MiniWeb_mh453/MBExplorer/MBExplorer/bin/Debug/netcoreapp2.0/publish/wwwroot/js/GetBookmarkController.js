
function getBookmarks(onSuccess) {
    $.get("/api/bookmark", function (result) {
        onSuccess(result.map(setIconAndState));
    });
}

function getTextFileAndPopulateModal(id) {
    $.ajax({
        url: "/service/textfile/" + id,
        dataType: "JSON",
        beforeSend: function (xhr) {
            openModal("#js-view-textFileModal"); 
            $("#js-view-textfile-modal-content").hide();
            $("#js-textfile-modal-spinner").spin();
        },
        success: function (data) {
            $("#js-textfile-modal-spinner").fadeOut(function () {
                var view = $("#js-view-textfile-modal-content");
                $(view).find("#js-title").val(data.name);
                $(view).find("#js-content").val(data.fileContent);
                $(view).find(".js-from-parent").val(getSelectedBookmarkPath());
                $(view).show();
            });
        }
    });
}

function getLocationAndPopulateModal(id) {
    $.ajax({
        url: "/service/location/" + id,
        dataType: "JSON",
        beforeSend: function () {
            openModal("#js-view-locationModal");
            $("#js-view-location-modal-content").hide();
            $("#js-location-modal-spinner").spin();
        },
        success: function (data) {
            $("#js-location-modal-spinner").fadeOut(function () {
                console.log(data);
                addMarker(data.latitude, data.longitude);
                google.maps.event.trigger(map, "resize");
                var view = $("#js-view-location-modal-content");
                $(view).find("#js-name").val(data.name);
                $(view).find(".js-from-parent").val(getSelectedBookmarkPath());
                $(view).show();
            });
        }
    });
}