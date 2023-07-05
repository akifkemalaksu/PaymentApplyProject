let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let bankaSelect = $("#bankaId").select2(GetSelectOption({ url: "Bankalar", extraOptions: extraOptions }));
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

datatableHelper.datatableOptions.columnDefs = [
    { "className": "dt-center", "targets": [6, 8] }
]
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "banka" },
    { data: "hesapNumarasi" },
    { data: "adSoyad" },
    {
        data: "altLimit",
        render: (data) => formatter.toMoney.format(data)
    },
    {
        data: "ustLimit",
        render: (data) => formatter.toMoney.format(data)
    },
    {
        data: "aktifMi",
        render: (data) => data ?
            `<span class="kt-badge kt-badge--inline kt-badge--success">Aktif</span>`
            : `<span class="kt-badge kt-badge--inline kt-badge--danger">Pasif</span>`
    },
    { data: "eklemeTarihi" },
    {
        data: function (data) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Düzenle">
                <i class="flaticon-edit"></i>
            </button>
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="sil(${data.id})" title="Sil">
                <i class="flaticon-delete"></i>
            </button>
            `;
        },
    }
];

datatableHelper.initialize($("#kt_table_1"));

$("#ekle").on('click', async () => {
    let resultHtml = await fetchHelper.sendText(`/bank/ViewAddBankAccountPartial`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");

    modalHeader.html("Banka Hesabý Ekleme");
    modalBody.html(resultHtml);

    addBankaAccountDefines()

    ktModal.modal('show');
})

let addBankaAccountDefines = () => {

}

let sil = (id) => swal.basicWithTwoButtonFunc("Uyarý", "Silmek istediðinize emin misiniz?", icons.warning,
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