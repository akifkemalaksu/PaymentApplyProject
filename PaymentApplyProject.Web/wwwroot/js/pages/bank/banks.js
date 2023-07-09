datatableHelper.datatableOptions.ajax = {
    url: "/bank/LoadBanks",
    type: "POST"
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [2] })
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "ad" },
    { data: null, defaultContent: "" }
];

datatableHelper.initialize($("#kt_table_1"));