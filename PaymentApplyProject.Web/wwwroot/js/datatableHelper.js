const datatableHelper = {
    dtTable: null,
    datatableOptions: {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/tr.json',
        },
        autoWidth: true,
        fixedHeader: true,
        stateSave: false,
        processing: true,
        serverSide: true,
        searchDelay: 500,
        paging: true,
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
        pageLength: 10,
        pagingType: "full_numbers",
        responsive: true,
        columnDefs: [
            { targets: "no-visible", visible: false },
            { targets: "no-sort", orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-tr" }
        ]
    },
    initialize: (table) => {
        datatableHelper.dtTable = table.DataTable(datatableHelper.datatableOptions);
    },
    draw: () => datatableHelper.dtTable.draw(),
    strtrunc: (str, num) => {
        if (str.length > num) {
            return str.slice(0, num) + "...";
        }
        else {
            return str;
        }
    }
}