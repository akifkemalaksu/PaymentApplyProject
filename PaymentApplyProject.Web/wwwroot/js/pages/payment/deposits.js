connection.on('displayNotification', (data) => {
    datatableHelper.dtTable.draw()
});

let filtreleButton = $('#kt_search');

let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#companyId").serverSelect2({ url: "Companies", extraOptions: extraOptions });
let musteriSelect = $("#customerId").serverSelect2({ url: "Customers", extraOptions: extraOptions });
let bankaSelect = $("#bankId").serverSelect2({ url: "Banks", extraOptions: extraOptions });
let bankaHesapSelect = $("#bankAccountId").serverSelect2({ url: "BankAccounts", extraOptions: extraOptions });
let durumSelect = $("#statusId").select2();
let startDateInput = $("#startDate");
let endDateInput = $("#endDate");

const startDate = moment().startOf('month')
const endDate = moment().add(1, 'days').subtract(1, 'seconds')

startDateInput.val(startDate.format("DD.MM.YYYY"));
endDateInput.val(endDate.format("DD.MM.YYYY"));

dateRangePickerOptions.startDate = startDate
dateRangePickerOptions.endDate = endDate
let tarihInput = $('#kt_daterangepicker').daterangepicker(dateRangePickerOptions, (start, end, label) => {
    startDateInput.val(start.format("DD.MM.YYYY"));
    endDateInput.val(end.format("DD.MM.YYYY"));
});

firmaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "companyId",
        value: this.value
    });
    musteriSelect.val(0).trigger("change");
    musteriSelect.serverSelect2({ url: "Customers", extraOptions: extraOptions, extraData: extraData });
});
bankaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "bankId",
        value: this.value
    });
    bankaHesapSelect.val(0).trigger("change");
    bankaHesapSelect.serverSelect2({ url: "BankAccounts", extraOptions: extraOptions, extraData: extraData });
});

filtreleButton.on("click", () => datatableHelper.dtTable.draw());

datatableHelper.datatableOptions.ajax = {
    url: "/payment/LoadDeposits",
    type: "POST",
    data: function (d) {
        d.companyId = firmaSelect.val()
        d.customerId = musteriSelect.val()
        d.bankId = bankaSelect.val()
        d.bankAccountId = bankaHesapSelect.val()
        d.statusId = durumSelect.val()
        d.startDate = startDateInput.val()
        d.endDate = endDateInput.val()
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [12] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "company" },
    { data: "customerUsername" },
    { data: "customerNameSurname" },
    { data: "bankAccountOwner" },
    { data: "bankAccountNumber" },
    { data: "bank" },
    {
        data: (row) => {
            if (row.statusId == "1")
                return `<span class="kt-badge kt-badge--inline kt-badge--success">${row.status}</span>`
            else if (row.statusId == "2")
                return `<span class="kt-badge kt-badge--inline kt-badge--danger">${row.status}</span>`
            else if (row.statusId == "3")
                return `<span class="kt-badge kt-badge--inline kt-badge--warning">${row.status}</span>`
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
        data: function (data) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="goruntule(${data.id}, '${data.customerNameSurname}')" title="Görüntüle">
                <i class="la la-eye"></i>
            </button>
            `;
        },
    }
];
datatableHelper.initialize($("#kt_table_1"));

let goruntule = async (id, customerNameSurname) => {
    let resultHtml = await fetchHelper.sendText(`/payment/ViewDepositPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");

    modalHeader.html(`${id} - ${customerNameSurname} - Para Yatırma İşlemi`);
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