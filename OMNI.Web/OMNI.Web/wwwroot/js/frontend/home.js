
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
        responsive: false,
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
        processing: true,
        "ajax": {
            "url": base_api + 'Home/GetAllLLPTrx?port=' + port,
            "type": 'GET'
        },
        "columns": [
            { "data": null },
            { "data": "peralatanOSR" },
            { "data": "jenis" },
            { "data": "satuanJenis" },
            { "data": "detailExisting" },
            {
              "targets": -1,
              "data": null,
                "render": function (row, data, iDisplayIndex) {
                    var image = "<a href='javascript:void(0)' onclick='qrcodeClick(" + iDisplayIndex.id + ")'><img src='" + iDisplayIndex.qrCode + "' alt='' width='35' id='qrcode-" + iDisplayIndex.id + "'></a>";
                    return image;
                 }
            },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    var kondisi = "";

                    if (iDisplayIndex.kondisi == "Baik") {
                        kondisi = "<b style='color:green;'>Baik</b>";
                    } else if (iDisplayIndex.kondisi == "Sedang") {
                        kondisi = "<b style='color:darkorange;'>Sedang</b>";
                    } else if (iDisplayIndex.kondisi == "Buruk") {
                        kondisi = "<b style='color:red;'>Buruk</b>";
                    }

                    return kondisi;
                }
            },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "' style='color:blue;' title='Gambar'><b><i>Upload Link</i></b></a>";
                }
            },
            { "data": "totalExistingJenis" },
            { "data": "totalExistingKeseluruhan" },
            { "data": "rekomendasiHubla" },
            { "data": "totalKebutuhanHubla" },
            { "data": "selisihHubla" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
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
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLLPTrx?id=" + iDisplayIndex.id + "&port=" + port + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deleteLLPTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
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


function qrcodeClick(id) {
    console.log(id);

    $.ajax({
        url: base_api + "Home/GetLLPTrxById?id=" + id,
        method: 'GET',
        success: function (result) {
            Swal.fire({
                imageUrl: result.data.qrCode,
                imageAlt: 'QR Code'
            })
        }
    });
}

function deleteLLPTrx(id) {
    Swal.fire({
        title: 'Do you want to delete?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete',
        denyButtonText: `Cancel`,
    }).then((result) => {
        if (result.value) {
            $.post(base_api + 'Home/DeleteLLPTrx?id=' + id, function (result) {
                Swal.fire('Deleted!', '', 'success');
                $("#table-llp").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}

function deleteFile(id) {
    console.log(id);
    Swal.fire({
        title: 'Do you want to delete?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete',
        denyButtonText: `Cancel`,
    }).then((result) => {
        if (result.value) {
            $.post(base_api + 'Home/DeleteFile?id=' + id, function (result) {
                Swal.fire('Deleted!', '', 'success');
                $("#table-files").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}
