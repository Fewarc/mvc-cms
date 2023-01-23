// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#newPostTitle').focusout(function() {
    $.ajax({
        url: '/ViewData/UpdateTitle',
        data: { "text" : $(this).val() },
        type: 'POST',
        success: function () {
            location.reload();
        }
    });
});

$('.section-text').focusout(function() {
    $.ajax({
        url: '/ViewData/UpdateSection',
        data: { "text" : $(this).val(), "id": $(this).attr("id")},
        type: 'POST',
        success: function () {
            location.reload();
        }
    });
});

$('.image-input').change(function () {
    var file = $(this).prop('files')[0];
    var formData = new FormData();

    formData.append('file', file);
    formData.append("id", $(this).attr("id"));
    
    $.ajax({
        url: '/ViewData/UpdateImageSection',
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function () {
            location.reload();
        }
    });
})