

var table_personil_trx = $('#table_personil_trx').DataTable({
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            text: '<i class="fal fa-download"></i> Export Excel',
            titleAttr: 'Generate Excel',
            className: 'btn btn-sm btn-outline-primary',
            title: 'Data Personil ' + formattedToday,
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
            }
        }
    ],
    "columnDefs": [
        { "text-align": "left", "targets": 1 },
    ],
    "ordering": false,
    "scrollX": true,
    "ajax": {
        "url": base_api + 'Home/GetAllPersonilTrx?port=' + port,
        "type": 'GET'
    },
    "columns": [
        /*{ "data": null },*/
        {
            name: 'personil',
            title: 'Personil',
            data: 'personil'
        },
        { "data": "name" },
        {
            name: 'satuan',
            title: 'Satuan',
            data: 'satuan'
        },
        {
            name: 'totalDetailExisting',
            title: 'Total Detail Existing',
            data: 'totalDetailExisting'
        },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                return "<a data-toggle='modal' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_PERSONIL' style='color:blue;' title='Gambar'><b><i>File Sertifikat</i></b></a>";
            }
        },
        { "data": "tanggalPelatihan" },
        { "data": "tanggalExpired" },
        { "data": "sisaMasaBerlaku" },
        {
            name: 'rekomendasiHubla',
            title: 'Rekomendasi Hubla',
            data: 'rekomendasiHubla'
        },
        {
            name: 'selisihHubla',
            title: 'Selisih Hubla',
            data: 'selisihHubla'
        },
        {
            name: 'kesesuaianPM58',
            title: 'Kesesuaian PM.58',
            target: -1,
            data: 'kesesuaianPM58',
            render: function (row, data, iDisplayIndex) {
                var result = "";

                if (iDisplayIndex.kesesuaianPM58 == "TERPENUHI") {
                    result = "<b style='color:green;'>TERPENUHI</b>";
                } else if (iDisplayIndex.kesesuaianPM58 == "KURANG") {
                    result = "<b style='color:red;'>KURANG</b>";
                }

                return result;
            }
        },
        {
            name: 'persentasePersonil',
            title: 'Persentase Personil',
            data: 'persentasePersonil'
        },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditPersonilTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                    " <a href='javascript:void(0)' onclick='deletePersonilTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
            }
        }
    ],
    rowsGroup: [
        'personil:name',
        'satuan:name',
        'totalDetailExisting:name',
        'rekomendasiHubla:name',
        'selisihHubla:name',
        'kesesuaianPM58:name',
        'persentasePersonil:name'
    ],
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": function (settings, json) {
        console.log('complete load table');
    }
});

//table_personil_trx.on('order.table_personil_trx search.table_personil_trx', function () {
//    table_personil_trx.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
//        cell.innerHTML = i + 1;
//    });
//}).draw();

function deletePersonilTrx(id) {
    Swal.fire({
        title: 'Do you want to delete?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete',
        denyButtonText: `Cancel`,
    }).then((result) => {
        if (result.value) {
            $.post(base_api + 'Home/DeletePersonilTrx?id=' + id, function (result) {
                Swal.fire('Deleted!', '', 'success');
                $("#table_personil_trx").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}
