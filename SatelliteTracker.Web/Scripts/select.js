﻿$("#selectForm").submit(function (e) {
    e.preventDefault();
    var formData = new FormData($(this)[0]);

    $.ajax({
        type: 'POST',
        url: "/Open",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (model) {
            drawOnMap(model);
        },
        error: function () {
            swal("", "Unexpected error occured.", "error");
        }
    });
});