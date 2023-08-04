let counterFunc = () => {
    let progressBar = $(".progress-bar")
    let counterSpan = $("#counter-text")
    let fullSeconds = parseInt(progressBar.attr("aria-valuemax"))
    let seconds = parseInt(counterSpan.data('second'))

    counterSpan.html(seconds);
    progressBar.css("width", ((100 * seconds) / fullSeconds) + "%");

    setInterval(() => {
        seconds--
        counterSpan.html(seconds)
        progressBar.css("width", ((100 * seconds) / fullSeconds) + "%");
        if (seconds === 0) {
            window.location.href = btnToMainPageFailed.prop("href")
        }
    }, 1000)
}

counterFunc()

let formEl = $('#kt_form');
let validator = formEl.validate({
    ignore: ":hidden",
    rules: {
        amount: {
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

let bankButtonClickEventDefine = () => $(".bank").on('click', (btn) => {
    let bankaId = btn.currentTarget.dataset.bankaid;
    bankaIdInput.val(bankaId);
    btnNext.click();
})
bankButtonClickEventDefine()

let btnNext = $('button[data-ktwizard-type="action-next"]')
let btnToMainPageSuccessful = $('#successful-back-to-main-page')
let btnToMainPageFailed = $('#failed-back-to-main-page')

wizard.on('beforeNext', async (wizardObj) => {
    if (validator.form() !== true) {
        wizardObj.stop();
        return;
    }
    if (wizardObj.currentStep === 1) {
        btnNext.removeClass("d-none");
        tutarDefines()
    }
    else if (wizardObj.currentStep === 2) {
        let tutar = parser.moneyToFloat(tutarInput.val())
        if (isNaN(tutar)) {
            wizardObj.stop();
            swal.basic("Uyarı", "Geçerli bir tutar giriniz.", icons.warning)
            return;
        }

        let result = await getBankaHesabiBilgisi()
        if (!result) {
            wizard.goTo(1)
            btnNext.addClass("d-none")
            bankButtonClickEventDefine()
        }
    }
    else if (wizard.currentStep === 3) {
        await odemeYap()
    }
});

wizard.on('change', function (wizard) {
    setTimeout(function () {
        KTUtil.scrollTop();
    }, 500);
});


let bankaIdInput = $("#bankId");
let bankaHesapIdInput = $("#bankAccountId");
let musteriIdInput = $("#customerId");
let tutarInput = $("#amount");
let depositRequestIdInput = $("#depositRequestId");

let tutarDefines = () => {
    tutarInput.maskMoney({ thousands: '', precision: false, allowZero: false });
}


let getBankaHesabiBilgisi = async () => {
    let data = {
        bankId: bankaIdInput.val(),
        amount: parser.moneyToFloat(tutarInput.val())
    }
    let result = await fetchHelper.send("/paymentframe/getaccountinfo", httpMethods.post, data)
    if (!result.isSuccessful) {
        swal.basic("Uyarı", result.message, icons.warning)
        return false
    }

    fillBankaHesapBilgileriArea(result.data)

    btnNext.html("Ödeme Yaptım");

    return true
}

let fillBankaHesapBilgileriArea = (data) => {
    $("#iban").html(data.accountNumber)
    let hesapSahibi = `${data.name} ${data.surname}`
    $("#hesapSahibi").html(hesapSahibi)

    bankaHesapIdInput.val(data.bankAccountId);

    let ibanCopySpan = $("#ibanCopy")
    let hesapSahibiCopySpan = $("#hesapSahibiCopy")

    ibanCopySpan.on("click", () => {
        navigator.clipboard.writeText(data.accountNumber)
        ibanCopySpan.popover('show')
        setTimeout(() => ibanCopySpan.popover('hide'), 3000)
    })
    hesapSahibiCopySpan.on("click", () => {
        navigator.clipboard.writeText(hesapSahibi)
        hesapSahibiCopySpan.popover('show')
        setTimeout(() => hesapSahibiCopySpan.popover('hide'), 3000)
    })
}

let resultDiv = $("#resultDiv")
let odemeYap = async () => {
    let tutar = parser.moneyToFloat(tutarInput.val())
    let data = {
        customerId: musteriIdInput.val(),
        bankAccountId: bankaHesapIdInput.val(),
        depositRequestId: depositRequestIdInput.val(),
        amount: tutar
    }

    let result = await fetchHelper.send("paymentframe/savepayment", httpMethods.post, data)

    if (!result.isSuccessful) {
        resultDiv.html(`
            <h3>İşlemde bir hata oluştu, ${result.message}</h3>  
        `)
        btnToMainPageFailed.show()
    }

    else {
        resultDiv.html(`
            <h2> 
                <i class="la la-try"></i> ${tutarInput.val()}
            </h2>
            <h3>Tutarında ödemeniz alınmıştır.</h3>  
        `)
        btnToMainPageSuccessful.show()
    }

    btnNext.hide();
}