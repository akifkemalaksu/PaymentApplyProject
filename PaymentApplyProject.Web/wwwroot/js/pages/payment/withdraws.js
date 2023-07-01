let extraOptions = [{ id: "0", text: "Hepsi", defaultSelected: true, selected: true }];
let firmaSelect = $("#firmaId").select2(GetSelectOption({ url: "Firmalar", extraOptions: extraOptions }));
let musteriSelect = $("#musteriId").select2(GetSelectOption({ url: "Musteriler", extraOptions: extraOptions }));
let durumSelect = $("#durumId").select2();
let filtreleButton = $('#kt_search');
let resetButton = $('#kt_reset');
firmaSelect.on('select2:select', function (e) {
    let extraData = [];
    extraData.push({
        name: "firmaId",
        value: this.value
    });
    musteriSelect.val(0).trigger("change");
    musteriSelect.select2(GetSelectOption({ url: "Musteriler", extraOptions: extraOptions, extraData: extraData }));
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

datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "firma" },
    { data: "musteriKullaniciAd" },
    { data: "musteriAdSoyad" },
    { data: "bankaHesapNo" },
    { data: "paraCekmeDurum" },
    { data: "onayRedTarihi" },
    {
        data: "tutar",
        render: (data) => moneyFormatter.format(data)
    },
    {
        data: "onaylananTutar",
        render: (data) => moneyFormatter.format(data)
    },
    {
        data: function (data, type, full, meta) {
            return `
            <span class="dropdown">
                <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">
                    <i class="la la-ellipsis-h"></i>
                </a>
                <div class="dropdown-menu dropdown-menu-right">
                    <a class="dropdown-item" href="#"><i class="la la-check"></i> Onayla </a>
                    <a class="dropdown-item" href="#"><i class="la-times"></i> Reddet </a>
                </div>
            </span>
            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="View">
                <i class="la la-eye"></i>
            </a>
            `;
        },
    }
];

datatableHelper.initialize($("#kt_table_1"));

