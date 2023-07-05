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

let bankButtonClickEventDefine = () => $(".bank").on('click', (btn) => {
    let bankaId = btn.currentTarget.dataset.bankaid;
    bankaIdInput.val(bankaId);
    btnNext.click();
})
bankButtonClickEventDefine()

let btnNext = $('button[data-ktwizard-type="action-next"]')
let btnToMainPage = $('#back-to-main-page')
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


let bankaIdInput = $("#bankaId");
let bankaHesapIdInput = $("#bankaHesapId");
let musteriIdInput = $("#musteriId");
let tutarInput = $("#tutar");

$.validator.messages.required = "Bu alan zorunludur."
let tutarDefines = () => {
    tutarInput.maskMoney({ thousands: '', precision: false, allowZero: false });
}


let getBankaHesabiBilgisi = async () => {
    let data = {
        bankaId: bankaIdInput.val(),
        tutar: parser.moneyToFloat(tutarInput.val())
    }
    let result = await fetchHelper.send("/paymentframe/getaccountinfo", httpMethods.post, data)
    if (!result.isSuccessful) {
        swal.basic("Uyarı", result.message, icons.warning)
        return false
    }
    bankaHesapIdInput.val(result.data.bankaHesapId);

    fillBankaHesapBilgileriArea(result.data)

    btnNext.html("Ödeme Yaptım");

    return true
}

let fillBankaHesapBilgileriArea = (data) => {

    $("#iban").html(data.hesapNumarasi)
    let hesapSahibi = `${data.ad} ${data.soyad}`
    $("#hesapSahibi").html(hesapSahibi)

    let ibanCopySpan = $("#ibanCopy")
    let hesapSahibiCopySpan = $("#hesapSahibiCopy")

    ibanCopySpan.on("click", () => {
        navigator.clipboard.writeText(data.hesapNumarasi)
        ibanCopySpan.popover('show')
        setTimeout(() => ibanCopySpan.popover('hide'), 3000)
    })
    hesapSahibiCopySpan.on("click", () => {
        navigator.clipboard.writeText(hesapSahibi)
        hesapSahibiCopySpan.popover('show')
        setTimeout(() => hesapSahibiCopySpan.popover('hide'), 3000)
    })

    counterFunc()
}

let counterFunc = () => {
    let counterSpan = $("#counter-text")
    let seconds = counterSpan.data('second')
    counterSpan.html(seconds);
    let sayac = parseInt(seconds)

    setInterval(() => {
        sayac--
        counterSpan.html(sayac)
        if (sayac === 0) {
            window.location.href = "https://grandpashabet1333.com"
        }
    }, 1000)
}

let resultDiv = $("#resultDiv")
let odemeYap = async () => {
    let tutar = parser.moneyToFloat(tutarInput.val())
    let data = {
        //musteriId: musteriIdInput.val(),
        musteriId: "1",
        bankaHesapId: bankaHesapIdInput.val(),
        tutar: tutar
    }

    let result = await fetchHelper.send("paymentframe/savepayment", httpMethods.post, data)

    if (!result.isSuccessful)
        resultDiv.html(`
            <h3>İşlemde bir hata oluştu, ${result.message}</h3>  
        `)
    else

        resultDiv.html(`
            <h2> 
                <i class="la la-try"></i> ${tutarInput.val()}
            </h2>
            <h3>Tutarında ödemeniz alınmıştır.</h3>  
        `)

    btnNext.hide();
    btnToMainPage.show()
}