let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#companyId").serverSelect2({ url: "Companies", extraOptions: extraOptions });
let durumSelect = $("#active").select2();
let filtreleButton = $('#kt_search');

filtreleButton.on("click", () => datatableHelper.dtTable.draw());

datatableHelper.datatableOptions.ajax = {
    url: "/company/LoadCustomers",
    type: "POST",
    data: function (d) {
        let durum = durumSelect.val()
        durum = durum == 0 ? null : durum

        d.companyId = firmaSelect.val()
        d.active = durum
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [5, 6] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "company" },
    { data: "username" },
    { data: "nameSurname" },
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
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="activeOrPassive(${data.id},${data.active})" title="Durum deðiþtir">
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