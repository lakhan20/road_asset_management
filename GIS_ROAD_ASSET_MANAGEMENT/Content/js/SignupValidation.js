
console.log("Signupvalidateupdate");
$(document).ready(function () {
    var citizenRadio = $('input[value="Citizen"]');
    var contractorRadio = $('input[value="Contractor"]');
    var citizenForm = $('#contentCitizen');
    var contractorForm = $('#contentContractor');

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
    console.log("inside fun signup");
    // Validation logic for citizen
    $('#CitizenForm').submit(function (e) {
        e.preventDefault();
        console.log("inside citixzen signup");
        var Crole_id = 4;
        var Cname = $('#Cname').val();
        var Cemail = $('#CEmail').val();
        var Ccontact = $('#Ccontact').val();
        var Cpassword = $('#Cpsw').val();
        var CconfirmPsw = $('#Ccnf-psw').val();
        var Cward = $('#Cward').val();
        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{9}$/;

        // Reset error messages
        $('#CnameError').html('');
        $('#CemailError').html('');
        $('#CnumberError').html('');
        $('#CpswError').html('');
        $('#CCnf-pswError').html('');
        $('#CwardError').html('');

        if (Cname == "") {
            console.log("validate name");
            $('#CnameError').html('Please enter your name.');
            return false;
        }
        else if (Cemail == "" || regEmail.test(Cemail) != true) {
            $('#CemailError').html('Please enter a valid email.');
            return false;
        }

        else if (Ccontact == "" || !regPhone.test(Ccontact) != true) {
            $('#CnumberError').html('Please enter a valid contact number.');
            return false;
        }

        else if (Cward == "") {
            $('#CwardError').html('Please enter the ward').show();
            return false;
        }

        else if (Cpassword == "" || Cpassword.length < 6) {
            $('#CpswError').html('Please enter a valid password (minimum 6 characters).');
            return false;
        }

        else if (CconfirmPsw == "" || CconfirmPsw !== Cpassword) {
            $('#CCnf-pswError').html('Passwords do not match.');
            return false;
        }
        else {
            var formData = {
                Cname, Cemail, Ccontact, Cward, Cpassword, Crole_id
            };
            console.log(formData);

            //Ajax for signup citizen
            $.ajax({
                url: '/SignUp/Csignup', // URL to your controller action
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.Success) {
                        console.log("signup success");
                       window.location.href = "/Home/Index";
                    } else {
                        console.log("Authentication failed");
                        //$('#loginModal').modal('show'); // Assuming your login modal has id "loginModal"
                        //$('#errorMessage').text("hytfffdusdvvh");
                    }
                },
                error: function (xhr, status, error) {
                    console.log("error");
                }
            });

        }
        // Additional actions on successful form submission can be added here
    });

    //Validation logic for contrator 
    $('#ContractorForm').submit(function (e) {
        e.preventDefault();
        console.log("inside Contractor Signup");
        var Con_role_id = 3;
        var Con_name = $('#Con_name').val();
        var Con_email = $('#Con_Email').val();
        var Con_contact = $('#Con_contact').val();
        var Con_company = $('#Con_Company').val();
        var Con_file = $('#Con_identity').val();
        var Con_password = $('#Con_psw').val();
        var Con_confirmPsw = $('#Con_cnf-psw').val();
        var Con_ward = $('#Con_ward').val();



        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{10}$/;

        // Reset error messages
        $('#Con_usrnameError').text('');
        $('#Con_emailError').text('');
        $('#Con_numberError').text('');
        $('#Con_companyError').text('');
        $('#Con_proofError').text('');
        $('#Con_pswError').text('');
        $('#Con_conf-pswError').text('');
        $('#Con_wardError').text('');

        if (Con_name == "") {
            $('#Con_usrnameError').html('Please enter your name.');
            return false;
        }

        else if (Con_email == "" || !regEmail.test(Con_email)) {
            $('#Con_emailError').html('Please enter a valid email.');
            return false;
        }

        else if (Con_contact == "" || !regPhone.test(Con_contact)) {
            $('#Con_numberError').html('Please enter a valid contact number.');
            return false;
        }

        else if (Con_company == "") {
            $('#Con_companyError').html('Please enter your company name.');
            return false;
        }

        else if (Con_file == "") {
            $('#Con_proofError').html('Please upload proof of identity.');
            return false;
        }
        else if (Con_ward == "") {
            $('#Con_wardError').html('Please enter the ward').show();
            return false;
        }

        else if (Con_password == "" || Con_password.length  < 6) {
            $('#Con_pswError').html('Please enter a valid password (minimum 6 characters).');
            return false;
        }

        else if (Con_confirmPsw == "" || Con_confirmPsw !== Con_password) {
            $('#Con_conf-pswError').html('Passwords do not match.');
            return false;
        }
        else {
            var formData = {
                Con_name, Con_email, Con_contact, Con_company, Con_file, Con_ward, Con_password, Con_role_id
            };
            console.log(formData);
        }


        $.ajax({
            url: '/SignUp/Con_signup', // URL to your controller action
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.Success) {
                    console.log("signup success");
                    window.location.href = "/Home/Index";
                } else {
                    console.log("Authentication failed");
                    //$('#loginModal').modal('show'); // Assuming your login modal has id "loginModal"
                    //$('#errorMessage').text("hytfffdusdvvh");
                }
            },
            error: function (xhr, status, error) {
                console.log("error");
            }
        });

    });
    //Ward officer sign up form 
    $('#WardOfficerForm').submit(function (e) {
        e.preventDefault();
        console.log("inside wardoffficer signup");
        var Wrole_id = 2;
        var Wname = $('#Wname').val();
        var Wemail = $('#WEmail').val();
        var Wcontact = $('#Wcontact').val();
        var Wpassword = $('#Wpsw').val();
        var WconfirmPsw = $('#Wcnf-psw').val();
        var Wward = $('#Wward').val();
        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{9}$/;

        // Reset error messages
        $('#WnameError').html('');
        $('#WemailError').html('');
        $('#WnumberError').html('');
        $('#WpswError').html('');
        $('#WCnf-pswError').html('');
        $('#WwardError').html('');

        if (Wname == "") {
            console.log("validate name");
            $('#WnameError').html('Please enter your name.');
            return false;
        }
        else if (Wemail == "" || regEmail.test(Wemail) != true) {
            $('#WemailError').html('Please enter a valid email.');
            return false;
        }

        else if (Wcontact == "" || !regPhone.test(Wcontact) != true) {
            $('#WnumberError').html('Please enter a valid contact number.');
            return false;
        }

        else if (Wward == "") {
            $('#WwardError').html('Please enter the ward').show();
            return false;
        }

        else if (Wpassword == "" || Wpassword.length < 6) {
            $('#WpswError').html('Please enter a valid password (minimum 6 characters).');
            return false;
        }

        else if (WconfirmPsw == "" || WconfirmPsw !== Wpassword) {
            $('#WCnf-pswError').html('Passwords do not match.');
            return false;
        }
        else {
            var formData = {
                Wname, Wemail, Wcontact, Wward, Wpassword, Wrole_id
            };
            console.log(formData);

            //Ajax for signup citizen
            $.ajax({
                url: '/SignUp/Wsignup', // URL to your controller action
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.Success) {
                        console.log("signup success");
                        window.location.href = "/Admin/UserView";
                    } else {
                        console.log("Authentication failed");
                        //$('#loginModal').modal('show'); // Assuming your login modal has id "loginModal"
                        //$('#errorMessage').text("hytfffdusdvvh");
                    }
                },
                error: function (xhr, status, error) {
                    console.log("error");
                }
            });

        }
        // Additional actions on successful form submission can be added here
    });
    //admin modal data

    $('#AdminForm').submit(function (e) {
        e.preventDefault();
        console.log("inside Admin signup");
        var Arole_id = 1;
        var Aname = $('#Aname').val();
        var Aemail = $('#AEmail').val();
        var Acontact = $('#Acontact').val();
        var Apassword = $('#Apsw').val();
        var AconfirmPsw = $('#Acnf-psw').val();
        var Award = $('#Award').val();
        var regEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
        var regPhone = /^\d{9}$/;

        // Reset error messages
        $('#AnameError').html('');
        $('#AemailError').html('');
        $('#AnumberError').html('');
        $('#ApswError').html('');
        $('#ACnf-pswError').html('');
        $('#AwardError').html('');

        if (Aname == "") {
            console.log("validate name");
            $('#AnameError').html('Please enter your name.');
            return false;
        }
        else if (Aemail == "" || regEmail.test(Aemail) != true) {
            $('#AemailError').html('Please enter a valid email.');
            return false;
        }

        else if (Acontact == "" || !regPhone.test(Acontact) != true) {
            $('#AnumberError').html('Please enter a valid contact number.');
            return false;
        }

        else if (Award == "") {
            $('#AwardError').html('Please enter the ward').show();
            return false;
        }

        else if (Apassword == "" || Apassword.length < 6) {
            $('#ApswError').html('Please enter a valid password (minimum 6 characters).');
            return false;
        }

        else if (AconfirmPsw == "" || AconfirmPsw !== Apassword) {
            $('#ACnf-pswError').html('Passwords do not match.');
            return false;
        }
        else {
            var formData = {
                Aname, Aemail, Acontact, Award, Apassword, Arole_id
            };
            console.log(formData);

            //Ajax for signup citizen
            $.ajax({
                url: '/SignUp/Asignup', // URL to your controller action
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.Success) {
                        console.log("signup success");
                        window.location.href = "/Admin/UserView";
                    } else {
                        console.log("Authentication failed");
                        //$('#loginModal').modal('show'); // Assuming your login modal has id "loginModal"
                        //$('#errorMessage').text("hytfffdusdvvh");
                    }
                },
                error: function (xhr, status, error) {
                    console.log("error");
                }
            });

        }
        // Additional actions on successful form submission can be added here
    });
});