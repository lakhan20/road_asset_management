$(document).ready(function () {
    var citizenRadio = $('input[value="option1"]');
    var contractorRadio = $('input[value="option2"]');
    var citizenForm = $('#contentOption1');
    var contractorForm = $('#contentOption2');

    function showCitizenForm() {
        citizenForm.show();
        contractorForm.hide();
    }

    function showContractorForm() {
        contractorForm.show();
        citizenForm.hide();
    }

    contractorForm.hide();

    citizenRadio.change(showCitizenForm);
    contractorRadio.change(showContractorForm);

    // Bind form submission events
    $('#myForm1').submit(validation1);
    $('#myForm2').submit(validation2);
});

function validation1(e) {
    e.preventDefault();

    // Validation logic for Form 1
    $('#myForm1').submit(function (e) {
        e.preventDefault();

        var name = $('#usrname').val();
        var email = $('#Email').val();
        var contact = $('#contact').val();
        var password = $('#psw').val();
        var confirmPsw = $('#cnf-psw').val();
        var ward = $('#ward').val();


        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{10}$/;

        // Reset error messages
        $('#usrnameError').text('');
        $('#emailError').text('');
        $('#numberError').text('');
        $('#pswError').text('');
        $('#psw1Error').text('');
        $('#wardError').text('');

        if (name == "") {
            $('#usrnameError').text('Please enter your name.');
            return false;
        }

        if (email == "" || !regEmail.test(email)) {
            $('#emailError').text('Please enter a valid email.');
            return false;
        }

        if (contact == "" || !regPhone.test(contact)) {
            $('#numberError').text('Please enter a valid contact number.');
            return false;
        }

        if (ward == "") {
            $('#wardError').text('Please enter the ward').show();
            return false;
        }

        if (password == "" || password.length < 6) {
            $('#pswError').text('Please enter a valid password (minimum 6 characters).');
            return false;
        }

        if (confirmPsw == "" || confirmPsw !== password) {
            $('#psw1Error').text('Passwords do not match.');
            return false;
        }

        // Additional actions on successful form submission can be added here
    });
}

function validation2(e) {
    // Validation logic for Form 2
    $('#myForm2').submit(function (e) {
        e.preventDefault();

        var name = $('#name').val();
        var email = $('#Email1').val();
        var contact1 = $('#contact1').val();
        var company1 = $('#Company1').val();
        var file = $('#identity1').val();
        var password = $('#psw1').val();
        var confirmPsw = $('#cnf-psw1').val();
        var ward1 = $('#ward1').val();

        

        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{10}$/;

        // Reset error messages
        $('#usrnameError1').text('');
        $('#emailError1').text('');
        $('#numberError1').text('');
        $('#companyError1').text('');
        $('#proofError1').text('');
        $('#pswError1').text('');
        $('#psw1Error1').text('');
        $('#ward1Error').text('');

        if (name == "") {
            $('#usrnameError1').text('Please enter your name.');
            return false;
        }

        if (email == "" || !regEmail.test(email)) {
            $('#emailError1').text('Please enter a valid email.');
            return false;
        }

        if (contact1 == "" || !regPhone.test(contact1)) {
            $('#numberError1').text('Please enter a valid contact number.');
            return false;
        }

        if (company1 == "") {
            $('#companyError1').text('Please enter your company name.');
            return false;
        }

        if (file == "") {
            $('#proofError1').text('Please upload proof of identity.');
            return false;
        }
        if (ward1 == "") {
            $('#ward1Error').text('Please enter the ward').show();
            return false;
        }

        if (password == "" || password.length < 6) {
            $('#pswError1').text('Please enter a valid password (minimum 6 characters).');
            return false;
        }

        if (confirmPsw == "" || confirmPsw !== password) {
            $('#psw1Error1').text('Passwords do not match.');
            return false;
        }

        // Additional actions on successful form submission can be added here
    });
}