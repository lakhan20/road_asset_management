$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        var email = $('#usrname2').val();
        var password = $('#psw2').val();

        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{10}$/;
        var regName = /^[a-zA-Z]+$/;

        if (email == "" || !regEmail.test(email)) {
            $("#checkname").html("Please enter a valid email.");
        } else {
            $("#checkname").html(""); // Hide the error message
        }

        if (password == "" || password.length < 6) {
            $("#checkpsw").html("Please enter a valid password (minimum 6 characters).");
        } else {
            $("#checkpsw").html(""); // Hide the error message
        }

        if (email !== "" && regEmail.test(email) && password !== "" && password.length >= 6) {
            // If email and password are valid, you can proceed with further actions here
            // For example, you can submit the form using AJAX
            // You can also redirect the user to another page
            // Add your additional logic here
        }

        return false; // Prevent form submission
    });
});