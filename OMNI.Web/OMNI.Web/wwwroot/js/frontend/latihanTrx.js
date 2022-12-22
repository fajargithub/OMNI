function countPercentageLatihanTrx() {
    console.log('on count percentage latihan trx!');
    var countRekomendasiHubla = 0;
    var totalPersentaseHubla = 0;
    var lastLatihan = "";
    $.ajax({
        url: base_api + 'Home/GetAllLatihanTrx?port=' + port + "&year=" + selectedYear,
        method: "GET",
        success: function (result) {
            if (result.data.length > 0) {
                for (var i = 0; i < result.data.length; i++) {
                    if (lastLatihan == "") {
                        lastLatihan = result.data[i].latihan;
                        if (result.data[i].rekomendasiHubla > 0) {
                            countRekomendasiHubla += 1;
                            totalPersentaseHubla += result.data[i].persentaseLatihan;
                        }
                    } else if (lastLatihan != result.data[i].latihan) {
                        lastLatihan = result.data[i].latihan;
                        if (result.data[i].rekomendasiHubla > 0) {
                            countRekomendasiHubla += 1;
                            totalPersentaseHubla += result.data[i].persentaseLatihan;
                        }
                    }

                }
            }
        }, complete: function () {
            var resultPersentaseHublalatihan = totalPersentaseHubla / (countRekomendasiHubla * 100) * 100;
            if (Number.isNaN(resultPersentaseHublalatihan)) {
                resultPersentaseHublalatihan = 0;
            }

            $("#totalPersentaseHublaLatihan").text(resultPersentaseHublalatihan.toFixed(2) + "%");
        }
    });
}

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
                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
            }
        }
    ],
    "columnDefs": [
        { "text-align": "left", "targets": 1 },
    ],
    "iDisplayLength": -1,
    "ordering": false,
    "scrollX": true,
    "ajax": {
        "url": base_api + 'Home/GetAllLatihanTrx?port=' + port + "&year=" + selectedYear,
        "type": 'GET'
    },
    "columns": [
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
            name: 'totalTanggalPelaksanaan',
            title: 'Jumlah Pelaksanaan',
            data: 'totalTanggalPelaksanaan'
        },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                return "<a data-toggle='modal' data-backdrop='static' data-keyboard='false' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_LATIHAN' style='color:blue;' title='Gambar'><b><i>File Dokumen</i></b></a>";
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
            data: 'persentaseLatihan',
            render: function (row, data, iDisplayIndex) {
                var result = "";
                if (iDisplayIndex.latihan == "Total Persentase") {
                    result = "<b><i id='totalPersentaseHublaLatihan'></i></b>";
                } else {
                    if (iDisplayIndex.rekomendasiHubla > 0) {
                        result = iDisplayIndex.persentaseLatihan + "%";
                    } else {
                        result = "-";
                    }
                }
              
                return result;
            }
        },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                var result = "";

                if (editable == "True") {
                    if (iDisplayIndex.latihan != "Total Persentase") {
                        //result += "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLatihanTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        //    " <a href='javascript:void(0)' onclick='deleteLatihanTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
                        result += "<div class='btn-group' role='group'>" +
                            "<button id='btnGroupVerticalDrop1' type='button' class='btn btn-primary btn-xs dropdown-toggle waves-effect waves-themed' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>Action</button>" +
                            "<div class='dropdown-menu' aria-labelledby='btnGroupVerticalDrop1'>" +
                            "<a class='dropdown-item' data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLatihanTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i> Edit</a>" +
                            "<a class='dropdown-item' data-toggle='modal' data-target='#modal-history' href='/HistoryTrx/HistoryLatihanTrx?trxId=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' title='Edit'><b style='color:teal;'><i class='fa fa-history'></i> History</b></a>" +
                            " <a class='dropdown-item' href='javascript:void(0)' onclick='deleteLatihanTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i> Delete</a>";

                        result += "</div>" +
                            "</div>";
                    }
                }
                
                return result;
            }
        },
    ],
    rowsGroup: [
        'latihan:name',
        'satuan:name',
        'totalTanggalPelaksanaan:name',
        'rekomendasiHubla:name',
        'selisihHubla:name',
        'kesesuaianPM58:name',
        'persentaseLatihan:name'
    ],
    createdRow: function (row, data, dataIndex) {
        if (data.latihan === 'Total Persentase') {
            // Add COLSPAN attribute
            $('td:eq(0)', row).attr('colspan', 3);

            // Center horizontally
            $('td:eq(0)', row).attr('align', 'center');

            // Hide required number of columns
            // next to the cell with COLSPAN attribute
            $('td:eq(2)', row).css('display', 'none');
            $('td:eq(4)', row).css('display', 'none');
            $('td:eq(5)', row).css('display', 'none');
            $('td:eq(6)', row).css('display', 'none');

            // Update cell data
            this.api().cell($('td:eq(0)', row)).data('<b>Total Persentase Hubla</b>');
            this.api().cell($('td:eq(3)', row)).data('');
            this.api().cell($('td:eq(5)', row)).data('');
            this.api().cell($('td:eq(6)', row)).data('');
        }
    },
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": countPercentageLatihanTrx
});

table_latihan_trx.on('draw', function () {
    countPercentageLatihanTrx();
});

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
                $("#table_latihan_trx").DataTable().ajax.reload(function () {
                    countPercentageLatihanTrx();
                });
            });
        } else if (result.isDenied) {

        }
    })
}
