let filtreleButton = $('#kt_search');
let resetButton = $('#kt_reset');

let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#firmaId").select2(GetSelectOption({ url: "Firmalar", extraOptions: extraOptions }));
let musteriSelect = $("#musteriId").select2(GetSelectOption({ url: "Musteriler", extraOptions: extraOptions }));
let bankaSelect = $("#bankaId").select2(GetSelectOption({ url: "Bankalar", extraOptions: extraOptions }));
let bankaHesapSelect = $("#bankaHesapId").select2(GetSelectOption({ url: "BankaHesaplar", extraOptions: extraOptions }));
let durumSelect = $("#durumId").select2();

firmaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "firmaId",
        value: this.value
    });
    musteriSelect.val(0).trigger("change");
    musteriSelect.select2(GetSelectOption({ url: "Musteriler", extraOptions: extraOptions, extraData: extraData }));
});
bankaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "bankaId",
        value: this.value
    });
    bankaHesapSelect.val(0).trigger("change");
    bankaHesapSelect.select2(GetSelectOption({ url: "BankaHesaplar", extraOptions: extraOptions, extraData: extraData }));
});

filtreleButton.on("click", () => datatableHelper.dtTable.draw());
resetButton.on("click", () => {
    $('.kt-input').val(0).trigger("change");
    filtreleButton.click();
});

datatableHelper.datatableOptions.ajax = {
    url: "/payment/LoadDeposits",
    type: "POST",
    data: function (d) {
        d.firmaId = firmaSelect.val()
        d.musteriId = musteriSelect.val()
        d.bankaId = bankaSelect.val()
        d.bankaHesapId = bankaHesapSelect.val()
        d.durumId = durumSelect.val()
    }
};
datatableHelper.datatableOptions.columnDefs = [
    { "className": "dt-center", "targets": [12] }
]
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "firma" },
    { data: "musteriKullaniciAd" },
    { data: "musteriAdSoyad" },
    { data: "bankaHesapSahibi" },
    { data: "bankaHesapNo" },
    { data: "banka" },
    {
        data: (row) => {
            if (row.durumId == "1")
                return `<span class="kt-badge kt-badge--inline kt-badge--warning">${row.durum}</span>`
            else if (row.durumId == "2")
                return `<span class="kt-badge kt-badge--inline kt-badge--danger">${row.durum}</span>`
            else if (row.durumId == "3")
                return `<span class="kt-badge kt-badge--inline kt-badge--success">${row.durum}</span>`
        }
    },
    { data: "talepTarihi" },
    { data: "islemTarihi" },
    {
        data: "tutar",
        render: (data) => formatter.toMoney.format(data)
    },
    {
        data: "onaylananTutar",
        render: (data) => formatter.toMoney.format(data)
    },
    {
        data: function (data) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="goruntule(${data.id}, '${data.musteriAdSoyad}')" title="Görüntüle">
                <i class="la la-eye"></i>
            </button>
            `;
        },
    }
];
datatableHelper.initialize($("#kt_table_1"));

let goruntule = async (id, musteriAdSoyad) => {
    let resultHtml = await fetchHelper.sendText(`/payment/ViewDepositPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");

    modalHeader.html(`${id} - ${musteriAdSoyad} - Para Yatırma İşlemi`);
    modalBody.html(resultHtml);

    goruntuleDefines()

    ktModal.modal('show');
}

let goruntuleDefines = () => {
    let onaylaButton = $("#onayla")
    let reddetButton = $("#reddet")
    let onaylanacakTutarInput = $("#onaylanacakTutar").maskMoney({ thousands: '', precision:false, allowZero: false });
    let idInput = $("#id")

    onaylaButton.on("click", async () => {
        let onaylanacakTutar = parser.moneyToFloat(onaylanacakTutarInput.val())

        if (!onaylanacakTutar || isNaN(onaylanacakTutar)) {
            onaylanacakTutarInput.addClass("is-invalid");
            return;
        }

        swal.basicWithTwoButtonFunc("Uyarı", "Talebi onaylamak istediğinize emin misiniz?", icons.warning,
            async (result) => {
                if (result.value) {
                    let data = {
                        id: idInput.val(),
                        tutar: onaylanacakTutar
                    }
                    let result = await fetchHelper.send("/payment/ApproveDeposit", httpMethods.post, data)

                    if (!result.isSuccessful) {
                        swal.basic("Hata", result.message, icons.error)
                        return;
                    }

                    swal.basicWithOneButtonFunc("Başarılı", result.message, icons.success, () => {
                        $("#kt_modal").modal('hide')
                        datatableHelper.dtTable.draw()
                    })
                }
            })
    })

    reddetButton.on("click", () => swal.basicWithTwoButtonFunc("Uyarı", "Talebi reddetmek istediğinize emin misiniz?", icons.warning,
        async (result) => {
            if (result.value) {
                let data = {
                    id: idInput.val()
                }
                let result = await fetchHelper.send("/payment/RejectDeposit", httpMethods.post, data)

                if (!result.isSuccessful) {
                    swal.basic("Hata", result.message, icons.error)
                    return;
                }
                swal.basicWithOneButtonFunc("Başarılı", result.message, icons.success, () => {
                    $("#kt_modal").modal('hide')
                    datatableHelper.dtTable.draw()
                })
            }
        }
    ))
}