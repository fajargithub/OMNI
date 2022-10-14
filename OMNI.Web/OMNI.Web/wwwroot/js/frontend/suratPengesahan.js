﻿var table2 = $('#table_surat_pengesahan').DataTable(
    {
        orderCellsTop: true,
        fixedHeader: true,
        responsive: true,
        ordering: false,
        order: [],
        columnDefs: [
            { "orderable": false, "targets": 5 }
        ],
        buttons: [
            {
                text: '<i class="fal fa-filter"></i> Filter',
                className: 'btn btn-sm btn-outline-primary',
                action: function (e, dt, node, config) {
                    $('#table_surat_pengesahan-filter').toggle();
                }
            },
        ],
        processing: true,
        "ajax": {
            "url": base_api + 'Lampiran/GetAllSuratPengesahan?port=' + port,
            "type": 'GET'
        },
        "columns": [
            { "data": null },
            { "data": "name" },
            { "data": "startDate" },
            { "data": "createdBy" },
            { "data": "createDate" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {

                    var result = "";
                    if (editable == "True") {
                        result += " <a href='javascript:void(0)' onclick='showRemark(" + iDisplayIndex.id + ")' class='btn-delete' title='Remark'><b style='color:lightblue;'><i class='fa fa-info-circle'></i></b></a> " +
                            "<a data-toggle='modal' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OSMOSYS_PENGESAHAN' style='color:blue;' title='Files'><b><i class='fa fa-archive'></i></b></a> " +
                            "<a data-toggle='modal' data-target='#modal-add-edit' href='/Lampiran/AddEdit?id=" + iDisplayIndex.id + "&port=" + port + "&lampiranType=PENGESAHAN' title='Edit'><b style='color:orange;'><i class='fa fa-pencil'></i></b></a>" +
                            " <a href='javascript:void(0)' onclick='deleteSuratPengesahan(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete'><b style='color:red;'><i class='fa fa-trash'></i></b></a>";
                    }

                    return result;
                }
            }
        ],
        "order": [[1, 'asc']],
        rowCallback: function (row, data, iDisplayIndex) {
        },
        "initComplete": function (settings, json) {
        }
    });

table2.on('order.dt search.dt', function () {
    table2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
        cell.innerHTML = i + 1;
    });
}).draw();

//function showRemark(id) {
//    $.post(base_api + 'Lampiran/GetById?id=' + id, function (result) {
//        Swal.fire(
//            'Remark',
//            result.data.remark,
//            'info'
//        )
//    });
//}

function deleteSuratPengesahan(id) {
    Swal.fire({
        title: 'Do you want to delete?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete',
        denyButtonText: `Cancel`,
    }).then((result) => {
        if (result.value) {
            $.post(base_api + 'Lampiran/DeleteLampiran?id=' + id, function (result) {
                console.log(result);
                Swal.fire('Deleted!', '', 'success');
                $("#table_surat_pengesahan").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}