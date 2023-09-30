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

    var handleResetPasswordFormSubmit = function () {
        $('#kt_login_resetpassword_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                url: '',
                rules: {
                    Password: {
                        required: true,
                    },
                    PasswordAgain: {
                        required: true,
                        equalTo: '[name="Password"]'
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
                        window.location.href = `/account/login?message=${response.message}&isSuccessful=${response.isSuccessful}`;
                    else {
                        btn.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false);
                        showErrorMsg(form, 'danger', response.message);
                    }
                }
            });
        });
    }

    // Public Functions
    return {
        // public functions
        init: function () {
            handleResetPasswordFormSubmit();
        }
    };
}();

// Class Initialization
jQuery(document).ready(function () {
    KTLoginGeneral.init();
});
