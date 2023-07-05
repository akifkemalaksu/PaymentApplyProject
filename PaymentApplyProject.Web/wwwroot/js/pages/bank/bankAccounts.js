let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let bankaSelect = $("#bankaId").select2(GetSelectOption({ url: "Bankalar", extraOptions: extraOptions }));
let durumSelect = $("#aktifMi").select2();
let tutarInput = $("#tutar").maskMoney({ thousands: '.', decimal: ',', allowZero: false });
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
        d.bankaId = bankaSelect.val()
        d.tutar = tutarInput.val()
        d.aktifMi = durumSelect.val()
    }
};

datatableHelper.datatableOptions.columnDefs = [
    //{ "className": "dt-center", "targets": [12] }
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
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="goruntule(${data.id}, '${data.musteriAdSoyad}')" title="Görüntüle">
                <i class="la la-eye"></i>
            </button>
            `;
        },
    }
];

datatableHelper.initialize($("#kt_table_1"));