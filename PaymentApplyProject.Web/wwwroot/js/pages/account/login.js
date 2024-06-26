"use strict";

// Class Definition
var KTLoginGeneral = function () {

    var login = $('#kt_login');

    var showErrorMsg = function (form, type, msg) {
        var alert = $('<div class="alert alert-' + type + ' alert-dismissible" role="alert">\
			<div class="alert-text">'+ msg + '</div>\
			<div class="alert-close">\
                <i class="flaticon2-cross kt-icon-sm" data-dismiss="alert"></i>\
            </div>\
		</div>');

        form.find('.alert').remove();
        alert.prependTo(form);
        //alert.animateClass('fadeIn animated');
        KTUtil.animateClass(alert[0], 'fadeIn animated');
        alert.find('span').html(msg);
    }

    // Private Functions
    var displaySignInForm = function () {
        login.removeClass('kt-login--forgot');
        login.removeClass('kt-login--signup');

        login.addClass('kt-login--signin');
        KTUtil.animateClass(login.find('.kt-login__signin')[0], 'flipInX animated');
        //login.find('.kt-login__signin').animateClass('flipInX animated');
    }

    var displayForgotForm = function () {
        login.removeClass('kt-login--signin');
        login.removeClass('kt-login--signup');

        login.addClass('kt-login--forgot');
        //login.find('.kt-login--forgot').animateClass('flipInX animated');
        KTUtil.animateClass(login.find('.kt-login__forgot')[0], 'flipInX animated');

    }

    var handleFormSwitch = function () {
        $('#kt_login_forgot').click(function (e) {
            e.preventDefault();
            displayForgotForm();
        });

        $('#kt_login_forgot_cancel').click(function (e) {
            e.preventDefault();
            displaySignInForm();
        });
    }

    var handleSignInFormSubmit = function () {
        $('#kt_login_signin_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    EmailUsername: {
                        required: true,
                    },
                    Password: {
                        required: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

            form.ajaxSubmit({
                success: function (response, status, xhr, $form) {
                    // similate 2s delay
                    if (response.isSuccessful)
                        window.location.href = "/";
                    else {
                        btn.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false);
                        showErrorMsg(form, 'danger', response.message);
                    }
                }
            });
        });
    }

    var handleForgotFormSubmit = function () {
        $('#kt_login_forgot_submit').click(function (e) {
            e.preventDefault();

            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    email: {
                        required: true,
                        email: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

            form.ajaxSubmit({
                success: function (response, status, xhr, $form) {
                    btn.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false); // remove
                    form.clearForm(); // clear form
                    form.validate().resetForm(); // reset validation states

                    // display signup form
                    displaySignInForm();
                    var signInForm = login.find('.kt-login__signin form');
                    signInForm.clearForm();
                    signInForm.validate().resetForm();

                    if (response.isSuccessful)
                        showErrorMsg(signInForm, 'success', '�ifre kurtarma talimat� e-postan�za g�nderildi.');
                    else
                        showErrorMsg(signInForm, 'danger', response.message);
                }
            });
        });
    }

    var handleMessageFromQueryString = function () {
        const queryString = window.location.search;
        const urlParams = new URLSearchParams(queryString);
        const message = urlParams.get('message');
        const isSuccessful = urlParams.get('isSuccessful');
        console.log(isSuccessful);
        if (message) {
            var signInForm = login.find('.kt-login__signin form');
            let type = (isSuccessful === 'true') ? 'success' : 'danger'
            showErrorMsg(signInForm, type, message);
        }
    }

    // Public Functions
    return {
        // public functions
        init: function () {
            handleFormSwitch();
            handleSignInFormSubmit();
            handleForgotFormSubmit();
            handleMessageFromQueryString();
        }
    };
}();

// Class Initialization
jQuery(document).ready(function () {
    KTLoginGeneral.init();
});
