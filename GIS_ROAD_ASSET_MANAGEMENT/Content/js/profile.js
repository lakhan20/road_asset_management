/*document.getElementById('updateButton').addEventListener('click', function (event) {
    event.preventDefault(); // Prevent form submission

    var formFields = document.querySelectorAll('input, select');
    formFields.forEach(function (field) {
        field.removeAttribute('readonly');
        field.removeAttribute('disabled');
    });

    // Change button to Save
    var saveButton = document.createElement('button');
    saveButton.setAttribute('type', 'submit');
    saveButton.setAttribute('class', 'btn btn-primary float-end mr-2');
    saveButton.setAttribute('id', 'saveButton');
    saveButton.innerText = 'Save';

    var updateButton = document.getElementById('updateButton');
    updateButton.parentNode.replaceChild(saveButton, updateButton);
});
*/


$(document).ready(function () {
   /* $('#changePasswordButton').click(function () {
        $('#passwordFields').toggleClass('d-none');
    });*/

    $('#updateButton').click(function (event) {
        event.preventDefault(); // Prevent form submission

        // Remove readonly attribute from all input fields
        $('input:not(#pid)').removeAttr('readonly').removeAttr('disabled'); 

        $('select').removeAttr('readonly').removeAttr('disabled'); 
        var $saveButton = $('<button type="button" class="btn btn-primary float-end mr-2" id="saveButton">Save</button>');
        $(this).replaceWith($saveButton);

        // Define function to handle AJAX call
        $saveButton.click(function () {
            // Perform AJAX call
            var fromData = {
                id: $('#pid').val(),
                name: $('#pname').val(),
                email_id: $('#pemailid').val(),
                contact_no: $('#pcontactno').val(),
                ward_id: $('#pward').val(),
                password: $('#ppassword').val(),
            };
            $.ajax({
                type: 'POST',
                url: '/Admin/ProfileView',
                data: fromData,
                success: function (response) {
                    alert('succes');
                    // Once saved, change button back to Update Profile
                    var $updateButton = $('<button type="submit" class="btn btn-primary float-end mr-2" id="updateButton">Update Profile</button>');
                    $saveButton.replaceWith($updateButton);
                    // Disable form fields
                    $('input, select').attr('readonly', true).attr('disabled', true);
                    console.log(response);
                },
                error: function (xhr, status, error) {
                    // Error handling
                    console.error(error);
                }
            });
            });
    });
});


