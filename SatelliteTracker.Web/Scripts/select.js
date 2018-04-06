$("#selectForm").submit(function (e) {
    e.preventDefault();

    if ($("#fileSelector").val() === null) {
        swal("No file selected!", "", "error");
        return;
    }
    var formData = new FormData($(this)[0]);

    $.ajax({
        type: 'POST',
        url: "/Open",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (model) {
            $("#selectCollapseButton").click();
            drawOnMap(model);
        },
        error: function () {
            swal("", "Unexpected error occured.", "error");
        }
    });
});