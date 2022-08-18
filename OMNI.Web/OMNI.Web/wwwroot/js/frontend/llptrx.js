var pagefunction = function () {

    $('#table_llp_trx').DataTable().destroy();

    /* COLUMN FILTER  */
    var dt = $('#table_llp_trx').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fal fa-download"></i> Export Excel',
                titleAttr: 'Generate Excel',
                className: 'btn btn-sm btn-outline-primary',
                exportOptions: {
                    columns: [1,2,3,4,5,7,9,10,11,12,13,14,15,16,17,18,19]
                }
            }
        ],
        "scrollX": true,
        "fixedColumns": {
            leftColumns: 7,
        },
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
                    return "<a data-toggle='modal' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_LLP' style='color:blue;' title='Gambar'><b><i>File Gambar</i></b></a>";
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
            { "data": "persentaseHubla" },
            { "data": "rekomendasiOSCP" },
            { "data": "totalKebutuhanOSCP" },
            { "data": "selisihOSCP" },
            { "data": "kesesuaianOSCP" },
            { "data": "persentaseOSCP" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLLPTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deleteLLPTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
                }
            }
        ],
        "order": [[1, 'asc']],
        rowCallback: function (row, data, iDisplayIndex) {
        },
        "initComplete": function (settings, json) {
            console.log('complete load table');
        }
    });

    dt.on('order.dt search.dt', function () {
        dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $("#table_llp_trx thead th input[type=text]").on('keyup change',
        function () {

            dt
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });
    /* END COLUMN FILTER */
}

function qrcodeClick(id) {
    $.ajax({
        url: base_api + "Home/GetLLPTrxById?id=" + id,
        method: 'GET',
        success: function (result) {
            console.log(result.data);
            Swal.fire({
                imageUrl: result.data.qrCode,
                imageAlt: 'QR Code',
                text: result.data.qrCodeText,
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
                $("#table_llp_trx").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}

pagefunction();