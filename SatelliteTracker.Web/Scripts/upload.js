$(document).on('change', ':file', function () {
    document.getElementById("showFileName").value = this.files[0].name;
});

$("#uploadForm").submit(function (e) {
    // Prevent the default behavior
    e.preventDefault();

    // Serialize your form
    var formData = new FormData($(this)[0]);

    // Make your POST
    $.ajax({
        type: 'POST',
        url: "/Upload",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (view) {
            swal("Upload completed successfully!", "", "success")
        },
    });
});