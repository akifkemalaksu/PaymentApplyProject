﻿let filtreleButton = $('#kt_search');
let resetButton = $('#kt_reset');

let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#companyId").serverSelect2({ url: "Companies", extraOptions: extraOptions });
let musteriSelect = $("#customerId").serverSelect2({ url: "Customers", extraOptions: extraOptions });
let durumSelect = $("#statusId").select2();

firmaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "companyId",
        value: this.value
    });
    musteriSelect.val(0).trigger("change");
    musteriSelect.serverSelect2({ url: "Customers", extraOptions: extraOptions, extraData: extraData });
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
        d.companyId = firmaSelect.val()
        d.customerId = musteriSelect.val()
        d.statusId = durumSelect.val()
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [10] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "company" },
    { data: "username" },
    { data: "nameSurname" },
    { data: "accountNumber" },
    {
        data: (row) => {
            if (row.statusId == "4")
                return `<span class="kt-badge kt-badge--inline kt-badge--warning">${row.status}</span>`
            else if (row.statusId == "5")
                return `<span class="kt-badge kt-badge--inline kt-badge--danger">${row.status}</span>`
            else if (row.statusId == "6")
                return `<span class="kt-badge kt-badge--inline kt-badge--success">${row.status}</span>`
        }
    },
    {
        data: "addDate",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "transactionDate",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "amount",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: "approvedAmount",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: function (data, type, full, meta) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="goruntule(${data.id}, '${data.nameSurname}')" title="Görüntüle">
                <i class="la la-eye"></i>
            </button>
            `;
        },
    }
];
datatableHelper.initialize($("#kt_table_1"));

let goruntule = async (id, nameSurname) => {
    let resultHtml = await fetchHelper.sendText(`/payment/ViewWithdrawPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");

    modalHeader.html(`${id} - ${nameSurname} - Para Çekme İşlemi`);
    modalBody.html(resultHtml);

    goruntuleDefines()

    ktModal.modal('show');
}

let goruntuleDefines = () => {
    let onaylaButton = $("#onayla")
    let reddetButton = $("#reddet")
    let onaylanacakTutarInput = $("#approvedAmount").maskMoney({ thousands: '', precision: false, allowZero: false });
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
                        amount: onaylanacakTutar
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
