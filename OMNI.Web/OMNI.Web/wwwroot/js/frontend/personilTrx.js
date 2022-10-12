function countPercentagePersonilTrx() {
    console.log('on count percentage personil trx!');
    var countRekomendasiHublaPersonil = 0;
    var totalPersentasePersonil = 0;
    var lastPersonil = "";
    $.ajax({
        url: base_api + 'Home/GetAllPersonilTrx?port=' + port + "&year=" + selectedYear,
        method: "GET",
        success: function (result) {
            if (result.data.length > 0) {
                for (var i = 0; i < result.data.length; i++) {
                    if (lastPersonil == "") {
                        lastPersonil = result.data[i].personil;
                        if (result.data[i].rekomendasiHubla > 0) {
                            countRekomendasiHublaPersonil += 1;
                            totalPersentasePersonil += result.data[i].persentasePersonil;
                        }
                    } else if (lastPersonil != result.data[i].personil) {
                        lastPersonil = result.data[i].personil;
                        if (result.data[i].rekomendasiHubla > 0) {
                            countRekomendasiHublaPersonil += 1;
                            totalPersentasePersonil += result.data[i].persentasePersonil;
                        }
                    }

                }
            }
        }, complete: function () {
            var resultPersentaseHublaPersonil = totalPersentasePersonil / (countRekomendasiHublaPersonil * 100) * 100;
            if (Number.isNaN(resultPersentaseHublaPersonil)) {
                resultPersentaseHublaPersonil = 0;
            }

            $("#totalPersentaseHublaPersonil").text(resultPersentaseHublaPersonil.toFixed(2) + "%");
        }
    });
}

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
        "url": base_api + 'Home/GetAllPersonilTrx?port=' + port + "&year=" + selectedYear,
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
                return "<a data-toggle='modal' data-backdrop='static' data-keyboard='false' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_PERSONIL' style='color:blue;' title='Gambar'><b><i>File Sertifikat</i></b></a>";
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
            data: 'persentasePersonil',
            render: function (row, data, iDisplayIndex) {
                var result = "";

                if (iDisplayIndex.personil == "Total Persentase") {
                    result = "<b><i id='totalPersentaseHublaPersonil'></i></b>";
                } else {
                    if (iDisplayIndex.rekomendasiHubla > 0) {
                        result = iDisplayIndex.persentasePersonil + "%";
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

                if (isManagement !== "True") {
                    if (iDisplayIndex.personil != "Total Persentase") {
                        result += "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditPersonilTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                            " <a href='javascript:void(0)' onclick='deletePersonilTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
                    }
                }
             
                return result;
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
    createdRow: function (row, data, dataIndex) {
        if (data.personil === 'Total Persentase') {
            // Add COLSPAN attribute
            $('td:eq(0)', row).attr('colspan', 3);

            // Center horizontally
            $('td:eq(0)', row).attr('align', 'center');

            // Hide required number of columns
            // next to the cell with COLSPAN attribute
            $('td:eq(2)', row).css('display', 'none');
            $('td:eq(3)', row).css('display', 'none');
            $('td:eq(4)', row).css('display', 'none');
            $('td:eq(5)', row).css('display', 'none');

            // Update cell data
            this.api().cell($('td:eq(0)', row)).data('<b>Total Persentase Hubla</b>');
            this.api().cell($('td:eq(3)', row)).data('');
            this.api().cell($('td:eq(7)', row)).data('');
            this.api().cell($('td:eq(8)', row)).data('');
            this.api().cell($('td:eq(9)', row)).data('');
        }
    },
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": countPercentagePersonilTrx
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
                console.log('on delete personil trx!');
                $("#table_personil_trx").DataTable().ajax.reload(null, false);
                countPercentagePersonilTrx();
            });
        } else if (result.isDenied) {

        }
    })
}
