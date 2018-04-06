$(document).on('change', ':file', function () {
    document.getElementById("showFileName").value = this.files[0].name;
});

$("#uploadForm").submit(function (e) {
    e.preventDefault();

    if ($(":file")[0].files.length === 0) {
        swal("No file selected!", "", "error");
        return;
    }

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
            $("#showFileName").val("");
            $("#uploadCollapseButton").click();
            addFileToSelect();
        }
    });
});

function addFileToSelect() {
    var $select = $("#fileSelector"),
        $fileInput = $(":file"),
        hasValue = false,
        files,
        fileName,
        pointIndex,
        option;
        

    if (typeof $fileInput !== "undefined" && typeof $fileInput[0] !== "undefined") {
        files = $fileInput[0].files;
        if (typeof files !== "undefined" && files.length > 0) {
            fileName = files[0].name;
            pointIndex = fileName.lastIndexOf(".");
            if (pointIndex !== -1) {
                fileName = fileName.substring(0, pointIndex);

                $select.find("option").each(function () {
                    if (this.text === fileName) {
                        hasValue = true;
                    }
                });

                if (hasValue) {
                    return;
                }

                option = document.createElement('option');
                option.value = fileName;
                option.text = fileName;
                $select[0].appendChild(option);
            }
        }
    }
}