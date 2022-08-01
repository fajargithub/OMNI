
$('#table-llp thead tr:eq(1) th').each(function (i) {
    $('select', this).on('change', function () {
        if (table.column(i).search() !== this.value) {
            table
                .column(i)
                .search(this.value)
                .draw();
        }
    });

    $('input', this).on('keyup change', function () {
        if (table.column(i).search() !== this.value) {
            table
                .column(i)
                .search(this.value)
                .draw();s
        }
    });
});

var table = $('#table-llp').DataTable(
    {
        orderCellsTop: true,
        fixedHeader: true,
        responsive: true,
        ordering: false,
        filter: true,
        order: [],
        columnDefs: [
            { "orderable": false, "targets": 14 }
        ],
        dom:
            "<'row mb-3'<'col-sm-12 col-md-6 d-flex align-items-center justify-content-start'l><'col-sm-12 col-md-6 d-flex align-items-center justify-content-end'B>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
        //buttons: [
        //    {
        //        text: '<i class="fal fa-filter"></i> Filter',
        //        className: 'btn btn-sm btn-outline-primary',
        //        action: function (e, dt, node, config) {
        //            $('#table-llp-filter').toggle();
        //        }
        //    },
        //],
        processing: true,
        //serverSide: true,
        "ajax": {
            "url": base_api + 'Home/GetAllLLPTrx?port=' + port,
            "type": 'GET'
            //"data": { searchData: result }
        },
        "columns": [
            { "data": null },
            { "data": "peralatanOSR" },
            { "data": "jenis" },
            { "data": "satuanJenis" },
            { "data": "detailExisting" },
            { "data": "qrCode" },
            { "data": "kondisi" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/RekomendasiLatihan/AddEdit?id=" + iDisplayIndex.id + "&port=" + port + "' style='color:blue;' title='Gambar'><b><i>Upload Link</i></b></a>";
                }
            },
            { "data": "totalExistingJenis" },
            { "data": "totalExistingKeseluruhan" },
            { "data": "rekomendasiHubla" },
            { "data": "totalKebutuhanHubla" },
            { "data": "selisihHubla" },
            { "data": "kesesuaianMP58" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/RekomendasiLatihan/AddEdit?id=" + iDisplayIndex.id + "&port=" + port + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deleteAction(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
                }
            }
        ],
        "order": [[1, 'asc']],
        rowCallback: function (row, data, iDisplayIndex) {
        },
        "initComplete": function (settings, json) {
        },
        "destroy": true
    }).ajax.reload();

table.on('order.dt search.dt', function () {
    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
        cell.innerHTML = i + 1;
    });
}).draw();