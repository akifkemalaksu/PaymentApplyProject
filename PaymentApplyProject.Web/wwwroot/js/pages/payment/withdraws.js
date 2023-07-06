let filtreleButton = $('#kt_search');
let resetButton = $('#kt_reset');

let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#firmaId").serverSelect2({ url: "Firmalar", extraOptions: extraOptions });
let musteriSelect = $("#musteriId").serverSelect2({ url: "Musteriler", extraOptions: extraOptions });
let durumSelect = $("#durumId").select2();

firmaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "firmaId",
        value: this.value
    });
    musteriSelect.val(0).trigger("change");
    musteriSelect.serverSelect2({ url: "Musteriler", extraOptions: extraOptions, extraData: extraData });
});

filtreleButton.on("click", () => datatableHelper.dtTable.draw());
resetButton.on("click", () => {
    $('.kt-input').val(0).trigger("change");
    filtreleButton.click();
});

datatableHelper.datatableOptions.ajax = {
    url: "/payment/LoadWithdraws",
    type: "POST",
    data: function (d) {
        d.firmaId = firmaSelect.val()
        d.musteriId = musteriSelect.val()
        d.durumId = durumSelect.val()
    }
};
datatableHelper.datatableOptions.columnDefs = [
    { "className": "dt-center", "targets": [10] }
]
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "firma" },
    { data: "musteriKullaniciAd" },
    { data: "musteriAdSoyad" },
    { data: "bankaHesapNo" },
    {
        data: (row) => {
            if (row.durumId == "4")
                return `<span class="kt-badge kt-badge--inline kt-badge--warning">${row.durum}</span>`
            else if (row.durumId == "5")
                return `<span class="kt-badge kt-badge--inline kt-badge--danger">${row.durum}</span>`
            else if (row.durumId == "6")
                return `<span class="kt-badge kt-badge--inline kt-badge--success">${row.durum}</span>`
        }
    },
    {
        data: "talepTarihi",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "islemTarihi",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "tutar",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: "onaylananTutar",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: function (data, type, full, meta) {
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
    let resultHtml = await fetchHelper.sendText(`/payment/ViewWithdrawPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");

    modalHeader.html(`${id} - ${musteriAdSoyad} - Para Çekme İşlemi`);
    modalBody.html(resultHtml);

    goruntuleDefines()

    ktModal.modal('show');
}

let goruntuleDefines = () => {
    let onaylaButton = $("#onayla")
    let reddetButton = $("#reddet")
    let onaylanacakTutarInput = $("#onaylanacakTutar").maskMoney({ thousands: '', precision: false, allowZero: false });
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
                    let result = await fetchHelper.send("/payment/Approvewithdraw", httpMethods.post, data)

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
                let result = await fetchHelper.send("/payment/Rejectwithdraw", httpMethods.post, data)

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
