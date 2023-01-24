$("#copyDataPersonil").hide();

$("#deleteAllPersonil").click(function () {
    var targetYear = parseInt($("#year-lokasi").val());

    Swal.fire({
        title: 'Do you want to delete All of Personil data?',
        icon: 'warning',
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {

            Swal.fire({
                title: '<i class="fa fa-cog fa-spin fa-3x fa-fw" aria-hidden="true"></i><span class="sr-only"> Delete Data</span>',
                text: 'Deleting, please wait',
                allowOutsideClick: false,
                showConfirmButton: false
            });

            $.ajax({
                url: base_api + "Home/DeleteAllPersonilTrx?port=" + port + "&year=" + targetYear,
                method: "GET",
                success: function (result) {
                    if (result.result == "SUCCESS") {
                        Swal.fire(
                            'Success!',
                            'Data is deleted',
                            'success'
                        )

                        setTimeout(function () {
                            Swal.close();
                            $('#modal-copy-data').modal('hide');
                            $("#table_personil_trx").DataTable().ajax.reload(function () {
                                console.log('on reload datatable!');
                                countPercentagePersonilTrx();
                            });
                        }, 1000)
                    }
                }
            })
        } else if (result.isDenied) {

        }
    })
});

$("#copyDataPersonil").click(function () {
    $.ajax({
        url: base_api + "Home/GetCopyDataYearPersonilTrx?port=" + port,
        method: "GET",
        success: function (result) {
            yearList = result.data;
            console.log(yearList);
        },
        complete: function () {
            $("#yearDataPersonilTrx").empty();
            var $dropdown = $("#yearDataPersonilTrx");
            $.each(yearList, function () {
                console.log(this);
                $dropdown.append($("<option />").val(this).text(this));
            });

            $('#modal-copy-data-personiltrx').modal('show');
        }
    });
});

$("#submitCopyDataPersonilTrx").click(function () {
    console.log('submit copy data personil trx!');
    idCopies = null;
    copyIndex = 0;
    var yearData = parseInt($("#yearDataPersonilTrx").val());
    var targetYear = parseInt($("#year-lokasi").val());
    if (yearData > 0) {
        Swal.fire({
            title: 'Do you want to copy data personil ' + portName + " from year " + yearData + "?",
            icon: 'info',
            showDenyButton: false,
            showCancelButton: true,
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.value) {

                Swal.fire({
                    title: '<i class="fa fa-cog fa-spin fa-3x fa-fw" aria-hidden="true"></i><span class="sr-only"> Copying Data</span>',
                    text: 'Saving, please wait',
                    allowOutsideClick: false,
                    showConfirmButton: false
                });

                $.ajax({
                    url: base_api + "Home/CopyDataPersonilTrx?port=" + port + "&year=" + yearData + "&targetYear=" + targetYear,
                    method: "GET",
                    success: function (result) {
                        setTimeout(function () {
                            Swal.close();
                            $('#modal-copy-data-personiltrx').modal('hide');
                            $("#table_personil_trx").DataTable().ajax.reload(function () {
                                countPercentagePersonilTrx();
                            });
                        }, 1000)
                    }
                })
            } else if (result.isDenied) {

            }
        })
    }

});

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
                $("#copyDataPersonil").hide();

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
            } else {
                $("#copyDataPersonil").show();
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
                } else if (iDisplayIndex.kesesuaianPM58 == "TIDAK TERPENUHI") {
                    result = "<b style='color:darkorange;'>TIDAK TERPENUHI</b>";
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

                if (editable == "True") {
                    if (iDisplayIndex.personil != "Total Persentase") {

                        result += "<div class='btn-group' role='group'>" +
                            "<button id='btnGroupVerticalDrop1' type='button' class='btn btn-primary btn-xs dropdown-toggle waves-effect waves-themed' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>Action</button>" +
                            "<div class='dropdown-menu' aria-labelledby='btnGroupVerticalDrop1'>" +
                            "<a class='dropdown-item' data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditPersonilTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i> Edit</a>" +
                            "<a class='dropdown-item' data-toggle='modal' data-target='#modal-history' href='/HistoryTrx/HistoryPersonilTrx?trxId=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' title='Edit'><b style='color:teal;'><i class='fa fa-history'></i> History</b></a>" +
                            " <a class='dropdown-item' href='javascript:void(0)' onclick='deletePersonilTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i> Delete</a>";

                        result += "</div>" +
                            "</div>";
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
    //fnDrawCallback: function (settings) {
    //    alert('on Change Page!');
    //},
    "initComplete": countPercentagePersonilTrx
});

table_personil_trx.on('draw', function () {
    countPercentagePersonilTrx();
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
