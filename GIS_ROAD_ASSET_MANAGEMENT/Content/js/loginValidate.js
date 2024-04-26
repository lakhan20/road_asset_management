console.log("loginvalidate");

$(document).ready(function () {
    $('#LoginForm').submit(function (e) {

        e.preventDefault();

        var Lemail = $('#Lusername').val();
        var Lpassword = $('#Lpassword').val();

        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;

        $("#LemailError").html("");
        $("#LpwdError").html("");

        if (Lemail == "" || regEmail.test(Lemail) != true) {
            console.log("validate name");
            $("#LemailError").html("Please enter valid email.");
            return false;
        }

        if (Lpassword == "" || Lpassword.length < 6) {
            $("#LpwdError").html("Please enter valid password (minimum 6 characters).");
            return false;
        }

        if (Lemail !== " " && Lpassword !== "" && Lpassword.length >= 6) {
            // If email and password are valid, you can proceed with further actions here
            e.preventDefault(); // Prevent the default form submission

            //Collect form data
            var formData = {
                username: $('#Lusername').val(),
                password: $('#Lpassword').val()
            };
            console.log(formData);



            //Send AJAX request to the controller
            $.ajax({
                url: 'Login/Login', // URL to your controller action
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.Success) {
                        console.log("success");

                        window.location.href = "/Admin/HomeView";
                    } else {

                        $('#errorMessage').text(response.message);


                    }
                },
                error: function (xhr, status, error) {
                    console.log("error");
                }
            });
        }
    });

    //for logout

});


$('#Logoutbtn11').click(function () {
    console.log("logout.............");
    $.ajax({
        url: '/Login/Logout', // URL to your controller action
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