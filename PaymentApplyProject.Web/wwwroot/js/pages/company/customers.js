let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#firmaId").serverSelect2({ url: "Firmalar", extraOptions: extraOptions });
let durumSelect = $("#aktifMi").select2();
let filtreleButton = $('#kt_search');
let resetButton = $('#kt_reset');

filtreleButton.on("click", () => datatableHelper.dtTable.draw());
resetButton.on("click", () => {
    $('.kt-input').val(0).trigger("change");
    filtreleButton.click();
});

datatableHelper.datatableOptions.ajax = {
    url: "/company/LoadCustomers",
    type: "POST",
    data: function (d) {
        let durum = durumSelect.val()
        durum = durum == 0 ? null : durum

        d.firmaId = firmaSelect.val()
        d.aktifMi = durum
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [5, 6] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "firma" },
    { data: "kullaniciAdi" },
    { data: "adSoyad" },
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
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="activeOrPassive(${data.id},${data.aktifMi})" title="Durum deðiþtir">
                <i class="fa fa-exchange-alt"></i>
            </button>
            `;
        },
    }
];

datatableHelper.initialize($("#kt_table_1"));

let activeOrPassive = (id, aktif) => swal.basicWithTwoButtonFunc("Uyarý", `${(aktif ? 'Pasif' : 'Aktif')} etmek istediðinize emin misiniz?`, icons.warning,
    async (result) => {
        if (result.value) {
            let result = await fetchHelper.send(`/company/changeCustomerStatus/${id}`, httpMethods.post)

            if (!result.isSuccessful) {
                swal.basic("Hata", result.message, icons.error)
                return;
            }
            swal.basicWithOneButtonFunc("Baþarýlý", result.message, icons.success, () => datatableHelper.dtTable.draw())
        }
    }
)