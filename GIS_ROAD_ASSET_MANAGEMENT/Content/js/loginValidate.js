console.log("login validate");

$(document).ready(function () {

    var captchaValue = $('#captchaValue').val();
    $('#captchaShow').val(captchaValue);

    $('#LoginForm').submit(function (e) {

        e.preventDefault();
      
        var Lemail = $('#Lusername').val();
        var Lpassword = $('#Lpassword').val();
        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;

        $("#LemailError").html("");
        $("#LpwdError").html("");

        if (Lemail == "" || regEmail.test(Lemail) != true) {
            $("#LemailError").html("Please enter valid email.");
            $('#Lusername').val(''); // Clear username field
            $('#Lpassword').val(''); // Clear password field
            return false;
        }

        if (Lpassword == "" || Lpassword.length < 6) {
            $("#LpwdError").html("Please enter valid password (minimum 6 characters).");
            $('#Lpassword').val(''); // Clear password field
            return false;
        }

        if ($('#rememberMeCheckbox').is(':checked')) {
            localStorage.setItem('Lusername', $('#Lusername').val());
            localStorage.setItem('Lpassword', $('#Lpassword').val());
        } else {
            // If "Remember me" is not checked, remove stored values from local storage
            localStorage.removeItem('Lusername');
            localStorage.removeItem('Lpassword');
        }
        $('#loginModal').on('shown.bs.modal', function () {
            var storedUsername = localStorage.getItem('Lusername');
            var storedPassword = localStorage.getItem('Lpassword');

            // Populate form fields with stored values
            if (storedUsername) $('#Lusername').val(storedUsername);
            if (storedPassword) $('#Lpassword').val(storedPassword);
        });
        // Function to generate a random CAPTCHA code
        

        // Event listener for refreshing CAPTCHA
       

        // If email and password are valid, proceed with the AJAX request
        var formData = {
            username: $('#Lusername').val(),
            password: $('#Lpassword').val(),
            captchaCode: $('#captchaInput').val()
        };

        $.ajax({
            url: '/Login/Login',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.Success) {
                    console.log("success");
                    window.location.href = "/Admin/HomeView"; // Redirect on successful login
                } else {
                    $('#errorMessage').text(response.message);
                    $('#captchaError').text(response.Message);
                    $('#Lpassword').val(''); // Clear password field

                    refreshCaptcha(); // Refresh CAPTCHA
                }
            },
            error: function (xhr, status, error) {
                console.log("error");
            }
        });
    });

    $.ajax({
        url: '/Login/GenerateCaptchaImage',
        type: 'GET',
        success: function (captchaCode) {
            // Set the received CAPTCHA code in the text box
            $('#captchashow').val(captchaCode);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching CAPTCHA code:', error);
        }
    });

    refreshCaptcha();

    // Event listener for refreshing CAPTCHA when the button is clicked
    $('#refreshCaptcha').click(function () {
        refreshCaptcha();
    });
});
function generateRandomCaptchaCode(length) {
    var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var randomCaptcha = '';
    for (var i = 0; i < length; i++) {
        randomCaptcha += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return randomCaptcha;
}

// Function to refresh the CAPTCHA
function refreshCaptcha() {
    $.ajax({
        url: '/Login/GenerateCaptchaImage',
        type: 'GET',
        success: function (captchaCode) {
            // Set the received CAPTCHA code in the text box
            $('#captchashow').val(captchaCode);

            // Update the session with the new CAPTCHA code
            $.ajax({
                url: '/Login/UpdateCaptchaSession',
                type: 'POST',
                data: { captchaCode: captchaCode },
                success: function (response) {
                    console.log("Session updated with new captcha code");
                },
                error: function (xhr, status, error) {
                    console.error('Error updating session with new CAPTCHA code:', error);
                }
            });
        },
        error: function (xhr, status, error) {
            console.error('Error fetching CAPTCHA code:', error);
        }
    });
}

$(document).on('click', '#logoutbtn', function () {
    console.log("hi");
    $.ajax({
        url: '/Login/Logout',
        type: 'POST',
        success: function (response) {
            if (response.Success) {
                console.log("logout");
                window.location.href = "/home/index";
            } else {
                $('#errorMessage').text(response.message);
                console.log("not logout")
            }
        },
        error: function (xhr, status, error) {
            console.log("error");
        }
    });
});
