let durumSelect = $("#active").select2();
let filtreleButton = $('#kt_search');

filtreleButton.on("click", () => datatableHelper.draw());

datatableHelper.datatableOptions.ajax = {
    url: "/company/LoadCompanies",
    type: "POST",
    data: function (d) {
        let durum = durumSelect.val()
        durum = durum == 0 ? null : durum

        d.active = durum
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [2, 3] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "name" },
    {
        data: "active",
        render: (data) => data ?
            `<span class="kt-badge kt-badge--inline kt-badge--success">Aktif</span>`
            : `<span class="kt-badge kt-badge--inline kt-badge--danger">Pasif</span>`
    },
    {
        data: function (data) {
            return `
            <button class="btn btn-sm btn-clean btn-icon btn-icon-md" onclick="activeOrPassive(${data.id},${data.active})" title="Durum de�i�tir">
                <i class="fa fa-exchange-alt"></i>
            </button>
            `;
        },
    }
];

datatableHelper.initialize($("#kt_table_1"));

let activeOrPassive = (id, aktif) => swal.basicWithTwoButtonFunc("Uyar�", `${(aktif ? 'Pasif' : 'Aktif')} etmek istedi�inize emin misiniz?`, icons.warning,
    async (result) => {
        if (result.value) {
            let result = await fetchHelper.send(`/company/changeCompanyStatus/${id}`, httpMethods.post)

            if (!result.isSuccessful) {
                swal.basic("Hata", result.message, icons.error)
                return;
            }
            swal.basicWithOneButtonFunc("Ba�ar�l�", result.message, icons.success, () => datatableHelper.draw())
        }
    }
)