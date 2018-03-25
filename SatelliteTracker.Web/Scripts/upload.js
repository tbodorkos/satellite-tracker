$(document).on('change', ':file', function () {
    document.getElementById("showFileName").value = this.files[0].name;
});

$("#uploadForm").submit(function (e) {
    e.preventDefault();
    var formData = new FormData($(this)[0]);

    $.ajax({
        type: 'POST',
        url: "/Upload",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (view) {
            swal("Upload completed successfully!", "", "success");
            $("#uploadCollapseButton").click();
        }
    });
});
