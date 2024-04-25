

$(document).ready(function ()
{
    
    $("#log_btn").click(function () {
        $("#loginModal").modal('show');
        console.log("log  btn click");
    });
    $("#log_link").click(function () {
        $("#loginModal").modal('show');
        console.log("log link click");
    });
    $("#signup_btn").click(function () {
        $("#signupModal").modal('show');
        console.log("sign btn click");
    });
    $("#signup_link").click(function () {
        $("#signupModal").modal('show');
        console.log("signup link click");
    });

});



    
