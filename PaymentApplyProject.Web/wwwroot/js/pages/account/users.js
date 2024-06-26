﻿let durumSelect = $("#active").select2();
let filtreleButton = $('#kt_search');

filtreleButton.on("click", () => datatableHelper.draw());

datatableHelper.datatableOptions.ajax = {
    url: "/account/loadusers",
    type: "POST",
    data: function (d) {
        let durum = durumSelect.val()

        d.active = durum == 0 ? null : durum
    }
};
datatableHelper.datatableOptions.columnDefs.push({ "className": "dt-center", "targets": [7, 8] });
datatableHelper.datatableOptions.columns = [
    { data: "id" },
    { data: "username" },
    { data: "email" },
    { data: "name" },
    { data: "surname" },
    {
        data: "addDate",
        render: (date) => formatter.toGoodDate(date)
    },
    {
        data: "companies",
        render: (data) => data.split(',').map((ad) => `<p class="kt-font-primary">${ad}</p>`).join('')
    },
    { data: "role" },
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
    let resultHtml = await fetchHelper.sendText(`/account/ViewAddUserPartial`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");
    let modalFooter = ktModal.find(".modal-footer");

    modalHeader.html("Kullanıcı Ekleme");
    modalBody.html(resultHtml);

    addUserDefines()

    modalFooter.empty()
    let closeButton = $('<button type="button" class="btn btn-secondary btn-hover-brand" data-dismiss="modal">Kapat</button>')
    modalFooter.append(saveButton)
    modalFooter.append(closeButton)

    ktModal.modal('show');
})

let addUserDefines = () => {
    let formEl = $('#kt_form');
    let validator = formEl.validate({
        rules: {
            Username: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Name: {
                required: true
            },
            Surname: {
                required: true
            },
            Roles: {
                required: true
            }
        },
        submitHandler: function (form) {
            swal.basicWithTwoButtonFunc("Uyarı", "Kaydetmek istediğinize emin misiniz?", icons.warning, (result) => {
                if (result.value)
                    save(form)
            })
        }
    });

    formEl.find('[name="Companies"]').serverSelect2({ url: "Companies" })
    formEl.find('[name="RoleId"]').select2({ width : '100%' })
    formEl.find('[name="Username"]').on('input', function (e) {
        let inputValue = $(this).val();
        let sanitizedValue = inputValue.replace(/\s/g, ''); // Remove spaces
        $(this).val(sanitizedValue);
    });
}

let save = (form) => {
    saveButton.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true)

    $(form).ajaxSubmit({
        success: function (response, status, xhr, $form) {
            saveButton.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false)

            if (!response.isSuccessful) {
                swal.basic("Hata", response.message, icons.error)
                return;
            }

            swal.basicWithOneButtonFunc("Başarılı", response.message, icons.success, () => {
                $("#kt_modal").modal('hide')
                datatableHelper.draw()
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
    let resultHtml = await fetchHelper.sendText(`/account/ViewEditUserPartial/${id}`, httpMethods.get);

    let ktModal = $("#kt_modal")
    let modalHeader = ktModal.find(".modal-title");
    let modalBody = ktModal.find(".modal-body");
    let modalFooter = ktModal.find(".modal-footer");

    modalHeader.html("Kullanıcı Düzenleme");
    modalBody.html(resultHtml);

    addUserDefines()

    modalFooter.empty()
    let closeButton = $('<button type="button" class="btn btn-secondary btn-hover-brand" data-dismiss="modal">Kapat</button>')
    modalFooter.append(saveButton)
    modalFooter.append(closeButton)

    ktModal.modal('show');
}


let deleteRecord = (id) => swal.basicWithTwoButtonFunc("Uyarı", "Silmek istediğinize emin misiniz?", icons.warning,
    async (result) => {
        if (result.value) {
            let data = {
                id: id
            }
            let result = await fetchHelper.send("/account/deleteuser", httpMethods.post, data)

            if (!result.isSuccessful) {
                swal.basic("Hata", result.message, icons.error)
                return;
            }
            swal.basicWithOneButtonFunc("Başarılı", result.message, icons.success, () => datatableHelper.draw())
        }
    }
)