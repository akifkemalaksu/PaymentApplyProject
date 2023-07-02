let formEl = $('#kt_form');

let validator = formEl.validate({
    ignore: ":hidden",
    rules: {
        tutar: {
            required: true
        },
    },

    invalidHandler: function (event, validator) {
        KTUtil.scrollTop();
    },

    submitHandler: function (form) {

    }
});

let wizard = new KTWizard('kt_wizard_v1', {
    startStep: 1,
    clickableSteps: false
});

let btnNext = $('button[data-ktwizard-type="action-next"]')
wizard.on('beforeNext', function (wizardObj) {
    if (validator.form() !== true) {
        wizardObj.stop();
        return;
    }
    if (wizardObj.currentStep === 1) {
        btnNext.removeClass("d-none");
        tutarDefines()
    }
    else if (wizardObj.currentStep === 2) {
        btnNext.html("Ödeme Yaptım");
    }
});

wizard.on('change', function (wizard) {
    setTimeout(function () {
        KTUtil.scrollTop();
    }, 500);
});


let bankaIdInput = $("#bankaId");
let tutarInput = $("#tutar");
$(".bank").on('click', (btn) => {
    let bankaId = btn.currentTarget.dataset.bankaid;
    $("#bankaId").val(bankaId);
    btnNext.click();
})

$.validator.messages.required = "Bu alan zorunludur."
let tutarDefines = () => {
    tutarInput.maskMoney({ thousands: '.', decimal: ',', allowZero: false });
}


let getBankaHesabi = async (data) => {
    let result = await fetchHelper.send("paymentframe/getaccountinfo", "POST", data)
    if (!result.isSuccessful) {
        tutarAlert.style.display = "block";
        tutarAlert.innerHTML = "Lütfen belirlenen değerler arasında bir tutar giriniz.";
        return;
    }
    return resultJson
}