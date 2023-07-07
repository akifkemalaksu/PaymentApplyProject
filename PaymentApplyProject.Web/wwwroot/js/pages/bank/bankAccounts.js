let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let bankaSelect = $("#bankaId").serverSelect2({ url: "Bankalar", extraOptions: extraOptions });
let durumSelect = $("#aktifMi").select2();
let tutarInput = $("#tutar").maskMoney({ thousands: '', precision: false, allowZero: false });
let filtreleButton = $('#kt_search');
let resetButton = $('#kt_reset');

filtreleButton.on("click", () => datatableHelper.dtTable.draw());
resetButton.on("click", () => {
    $('.kt-input').val(0).trigger("change");
    filtreleButton.click();
});

datatableHelper.datatableOptions.ajax = {
    url: "/bank/LoadBankAccounts",
    type: "POST",
    data: function (d) {
        let tutar = parser.moneyToFloat(tutarInput.val())
        let durum = durumSelect.val()
        durum = durum === 0 ? null : durum

        d.bankaId = bankaSelect.val()
        d.tutar = tutar
        d.aktifMi = durum
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [7, 8] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "banka" },
    { data: "hesapNumarasi" },
    { data: "adSoyad" },
    {
        data: "altLimit",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: "ustLimit",
        render: (data) => formatter.toMoney(data)
    },
    {
        data: "eklemeTarihi",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "aktifMi",
        render: (data) => data ?
            `<span class="kt-badge kt-badge--inline kt-badge--success">Aktif</span>`
            : `<span class="kt-badge kt-badge--inline kt-badge--danger">Pasif</span>`
    },
    {
        data: function (data) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="edit(${data.id})" title="D�zenle">
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

    modalHeader.html("Banka Hesab� Ekleme");
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
            BankaId: {
                required: true
            },
            HesapNumarasi: {
                required: true
            },
            Ad: {
                required: true
            },
            Soyad: {
                required: true
            },
            AltLimit: {
                required: true
            },
            UstLimit: {
                required: true
            }
        },
        submitHandler: function (form) {
            swal.basicWithTwoButtonFunc("Uyar�", "Kaydetmek istedi�inize emin misiniz?", icons.warning, (result) => {
                if (result.value)
                    save(form)
            })
        }
    });

    formEl.find('[name="BankaId"]').serverSelect2({ url: "Bankalar" })
    formEl.find('.money').maskMoney({ thousands: '', allowZero: true, precision: false })
}

let save = (form) => {
    saveButton.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

    $(form).ajaxSubmit({
        success: function (response, status, xhr, $form) {
            saveButton.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false);

            if (!response.isSuccessful) {
                swal.basic("Hata", response.message, icons.error)
                return;
            }

            swal.basicWithOneButtonFunc("Ba�ar�l�", response.message, icons.success, () => {
                $("#kt_modal").modal('hide')
                datatableHelper.dtTable.draw()
            })
        }
    });
}

let edit = async (id) => {
    let resultHtml = await fetchHelper.sendText(`/bank/ViewEditBankAccountPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");
    let modalFooter = ktModal.find(".modal-footer");

    modalHeader.html("Banka Hesab� D�zenleme");
    modalBody.html(resultHtml);

    addBankAccountDefines()

    modalFooter.empty()
    let closeButton = $('<button type="button" class="btn btn-secondary btn-hover-brand" data-dismiss="modal">Kapat</button>')
    modalFooter.append(saveButton)
    modalFooter.append(closeButton)

    ktModal.modal('show');
}

let deleteRecord = (id) => swal.basicWithTwoButtonFunc("Uyar�", "Silmek istedi�inize emin misiniz?", icons.warning,
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
            swal.basicWithOneButtonFunc("Ba�ar�l�", result.message, icons.success, () => datatableHelper.dtTable.draw())
        }
    }
)