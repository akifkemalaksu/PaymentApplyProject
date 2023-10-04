let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let bankaSelect = $("#bankId").serverSelect2({ url: "Banks", extraOptions: extraOptions });
let durumSelect = $("#active").select2();
let tutarInput = $("#amount").maskMoney({ thousands: '', precision: false, allowZero: false });
let filtreleButton = $('#kt_search');

filtreleButton.on("click", () => datatableHelper.dtTable.draw());

datatableHelper.datatableOptions.ajax = {
    url: "/bank/LoadBankAccounts",
    type: "POST",
    data: function (d) {
        let tutar = parser.moneyToFloat(tutarInput.val())
        let durum = durumSelect.val()

        d.bankId = bankaSelect.val()
        d.amount = tutar
        d.active = durum == 0 ? null : durum
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [7, 8] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "bank" },
    { data: "accountNumber" },
    { data: "nameSurname" },
    {
        data: "lowerLimit",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: "upperLimit",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: "addDate",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "active",
        render: (data) => data ?
            `<span class="kt-badge kt-badge--inline kt-badge--success">Aktif</span>`
            : `<span class="kt-badge kt-badge--inline kt-badge--danger">Pasif</span>`
    },
    {
        data: function (data) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="edit(${data.id})" title="Düzenle">
                <i class="flaticon-edit"></i>
            </button>
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="deleteRecord(${data.id})" title="Sil">
                <i class="flaticon-delete"></i>
            </button>
            `;
        },
    }
];

datatableHelper.initialize($("#kt_table_1"));

let saveButton = $('<button type="submit" form="kt_form" class="btn btn-success">Kaydet</button>')
$("#ekle").on('click', async () => {
    let resultHtml = await fetchHelper.sendText(`/bank/ViewAddBankAccountPartial`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");
    let modalFooter = ktModal.find(".modal-footer");

    modalHeader.html("Banka Hesabý Ekleme");
    modalBody.html(resultHtml);

    addBankAccountDefines()

    modalFooter.empty()
    let closeButton = $('<button type="button" class="btn btn-secondary btn-hover-brand" data-dismiss="modal">Kapat</button>')
    modalFooter.append(saveButton)
    modalFooter.append(closeButton)

    ktModal.modal('show');
})

let addBankAccountDefines = () => {
    let formEl = $('#kt_form');
    let validator = formEl.validate({
        rules: {
            BankId: {
                required: true
            },
            AccountNumber: {
                required: true
            },
            Name: {
                required: true
            },
            Surname: {
                required: true
            },
            LowerLimit: {
                required: true
            },
            UpperLimit: {
                required: true
            }
        },
        submitHandler: function (form) {
            swal.basicWithTwoButtonFunc("Uyarý", "Kaydetmek istediðinize emin misiniz?", icons.warning, (result) => {
                if (result.value)
                    save(form)
            })
        }
    });

    formEl.find('[name="BankId"]').serverSelect2({ url: "Banks" })
    formEl.find('.money').maskMoney({ thousands: '', allowZero: true, precision: false })
}

let save = (form) => {
    saveButton.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

    $(form).ajaxSubmit({
        success: function (response, status, xhr, $form) {
            saveButton.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false)

            if (!response.isSuccessful) {
                swal.basic("Hata", response.message, icons.error)
                return;
            }

            swal.basicWithOneButtonFunc("Baþarýlý", response.message, icons.success, () => {
                $("#kt_modal").modal('hide')
                datatableHelper.dtTable.draw()
            })
        },
        error: function (response) {
            saveButton.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false)
            let result = response.responseJSON
            swal.basic("Hata", result.message, icons.error)
        }
    });
}

let edit = async (id) => {
    let resultHtml = await fetchHelper.sendText(`/bank/ViewEditBankAccountPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");
    let modalFooter = ktModal.find(".modal-footer");

    modalHeader.html("Banka Hesabý Düzenleme");
    modalBody.html(resultHtml);

    addBankAccountDefines()

    modalFooter.empty()
    let closeButton = $('<button type="button" class="btn btn-secondary btn-hover-brand" data-dismiss="modal">Kapat</button>')
    modalFooter.append(saveButton)
    modalFooter.append(closeButton)

    ktModal.modal('show');
}

let deleteRecord = (id) => swal.basicWithTwoButtonFunc("Uyarý", "Silmek istediðinize emin misiniz?", icons.warning,
    async (result) => {
        if (result.value) {
            let data = {
                id: id
            }
            let result = await fetchHelper.send("/bank/deleteBankAccount", httpMethods.post, data)

            if (!result.isSuccessful) {
                swal.basic("Hata", result.message, icons.error)
                return;
            }
            swal.basicWithOneButtonFunc("Baþarýlý", result.message, icons.success, () => datatableHelper.dtTable.draw())
        }
    }
)