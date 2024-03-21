//function to load comments

function loadComments(projectID) {
    $.ajax({
        url: '/ProjectManagement/ProjectComment/GetComments?projectID=' + projectID,
        method: 'GET',
        success: function (data) {
            var commentHtml = '';
            for (var i = 0; i < data.length; i++) {
                commentHtml += '<div class="comment"';
                commentHtml += '<p>' + data[i].content + '</p>';
                commentHtml += '<span>Posted on ' + new Date(data[i].datePosted).toLocaledString() + '</span>';
                commentHtml += '</div>';
            }
            $('#commentList').html(commentHtml);
        }
    })
}

$(document).ready(function () {
    var projectID = $('#projectComments input[name="ProjectID"]').val();
    loadComments(projectID);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault(); //prevents entire page from being refreshed on submit
        var formData = {
            projectID: projectID,
            Content: $('#projectComments textarea[name="Content"]').val()
        };

        $.ajax({
            url: '/ProjectManagement/ProjectComment/AddComment',
            method: 'Post',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#projectComments textarea[name="Content"]').val(''); //clears text area
                    loadComments(projectID); //reloads comments after adding a new one
                } else {
                    alert(response.message);
                }
            },

            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });
});
