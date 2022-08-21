$('#table_latihan_trx').DataTable().destroy();

/* COLUMN FILTER  */
var table_latihan_trx = $('#table_latihan_trx').DataTable({
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            text: '<i class="fal fa-download"></i> Export Excel',
            titleAttr: 'Generate Excel',
            className: 'btn btn-sm btn-outline-primary',
            title: 'Data Latihan ' + formattedToday,
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7, 8]
            }
        }
    ],
    "columnDefs": [
        { "text-align": "left", "targets": 1 },
    ],
    "ordering": false,
    "scrollX": true,
    "ajax": {
        "url": base_api + 'Home/GetAllLatihanTrx?port=' + port,
        "type": 'GET'
    },
    "columns": [
        //{
        //    name: 'no',
        //    title: 'No',
        //    data: null
        //},
        {
            name: 'latihan',
            title: 'Latihan',
            data: 'latihan'
        },
        {
            name: 'satuan',
            title: 'Satuan',
            data: 'satuan'
        },
        { "data": "tanggalPelaksanaan" },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                return "<a data-toggle='modal' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_LATIHAN' style='color:blue;' title='Gambar'><b><i>File Dokumen</i></b></a>";
            }
        },
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
            name: 'persentaseLatihan',
            title: 'Persentase Latihan',
            data: 'persentaseLatihan'
        },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLatihanTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                    " <a href='javascript:void(0)' onclick='deleteLatihanTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
            }
        },
    ],
    rowsGroup: [
        'latihan:name',
        'satuan:name',
        'rekomendasiHubla:name',
        'selisihHubla:name',
        'kesesuaianPM58:name',
        'persentaseLatihan:name'
    ],
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": function (settings, json) {
        console.log('complete load table');
    }
});

//table_latihan_trx.on('order.table_latihan_trx search.table_latihan_trx', function () {
//    table_latihan_trx.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
//        cell.innerHTML = i + 1;
//    });
//}).draw();

function deleteLatihanTrx(id) {
    Swal.fire({
        title: 'Do you want to delete?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete',
        denyButtonText: `Cancel`,
    }).then((result) => {
        if (result.value) {
            $.post(base_api + 'Home/DeleteLatihanTrx?id=' + id, function (result) {
                Swal.fire('Deleted!', '', 'success');
                $("#table_latihan_trx").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}
