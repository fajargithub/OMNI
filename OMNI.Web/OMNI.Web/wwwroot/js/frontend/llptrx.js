$('#table_llp_trx').DataTable().destroy();

var totalPersentaseHubla = 0;
var totalPersentaseOSCP = 0;

function countTotalPercentageLLPTrx() {
    var lastJenis = "";
    var totalPersentaseHubla = 0;
    var totalPersentaseOSCP = 0;
    var countRekomendasiHubla = 0;
    var countRekomendasiOSCP = 0;

    $.ajax({
        url: base_api + 'Home/GetAllLLPTrx?port=' + port + "&year=" + selectedYear,
        method: "GET",
        success: function (result) {
            if (result.data !== null && result.data !== undefined) {
                if (result.data.length > 0) {
                    $("#copyData").hide();
                } else {
                    $("#copyData").show();
                }
                
                for (var i = 0; i < result.data.length; i++) {
                    if (lastJenis == "") {
                        lastJenis = result.data[i].jenis;
                        if (result.data[i].rekomendasiHubla > 0) {
                            totalPersentaseHubla += result.data[i].persentaseHubla;
                            countRekomendasiHubla += 1;
                        }

                        if (result.data[i].rekomendasiOSCP > 0) {
                            totalPersentaseOSCP += result.data[i].persentaseOSCP;
                            countRekomendasiOSCP += 1;
                        }

                    } else if (lastJenis != result.data[i].jenis) {
                        lastJenis = result.data[i].jenis;
                        if (result.data[i].rekomendasiHubla > 0) {
                            totalPersentaseHubla += result.data[i].persentaseHubla;
                            countRekomendasiHubla += 1;
                        }

                        if (result.data[i].rekomendasiOSCP > 0) {
                            totalPersentaseOSCP += result.data[i].persentaseOSCP;
                            countRekomendasiOSCP += 1;
                        }
                    }
                }
            }
        }, complete: function () {
            var resultPersentaseHubla = totalPersentaseHubla / (countRekomendasiHubla * 100) * 100;
            if (Number.isNaN(resultPersentaseHubla)) {
                resultPersentaseHubla = 0;
            }

            var resultPersentaseOSCP = totalPersentaseOSCP / (countRekomendasiOSCP * 100) * 100;
            if (Number.isNaN(resultPersentaseOSCP)) {
                resultPersentaseOSCP = 0;
            }

            $("#totalPersentaseHubla").text(resultPersentaseHubla.toFixed(2) + "%");
            $("#totalPersentaseOSCP").text(resultPersentaseOSCP.toFixed(2) + "%");
        }
    })
}

/* COLUMN FILTER  */
var dt = $('#table_llp_trx').DataTable({
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            text: '<i class="fal fa-download"></i> Export Excel',
            titleAttr: 'Generate Excel',
            className: 'btn btn-sm btn-outline-primary',
            title: 'Data LLP - ' + portName + ", " + selectedYear,
            exportOptions: {
                columns: [0, 1, 2, 3, 26, 27, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21],
                rows: ':not(:last)'
            }
        }
    ],
    "ordering": false,
    "scrollX": true,
    "scrollY": '400px',
    "scrollCollapse": true,
    "paging": false,
    "fixedColumns": {
        leftColumns: 7
       // rightColumns: 1
    },
    "ajax": {
        "url": base_api + 'Home/GetAllLLPTrx?port=' + port +"&year=" + selectedYear,
        "type": 'GET'
    },
    "columns": [
        /*{ "data": null },*/
        {
            name: 'peralatanOSR',
            title: 'Peralatan OSR',
            data: 'peralatanOSR',
            render : function (row, data, iDisplayIndex) {
                var result = iDisplayIndex.peralatanOSR;
                if (result == "Total Persentase") {
                    result = "<b>Total Persentase</b>";
                }
                return result;
            }
        },
        { "data": "jenis" },
        {
            name: 'satuanJenis',
            title: 'Satuan',
            data: 'satuanJenis'
        },
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
            name: 'kondisi',
            title: 'Kondisi',
            target: -1,
            data: 'kondisi',
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
                var result = "";
                if (iDisplayIndex.peralatanOSR != "Total Persentase") {
                    result = "<a data-toggle='modal' data-backdrop='static' data-keyboard='false' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_LLP' style='color:blue;' title='View Files'><b><i class='fa fa-archive' style='font-size:13px; color:teal;'></i></b></a>";
                }
                return result;
            }
        },
        {
            name: 'totalExistingJenis',
            title: 'Total Existing di Lokasi (Jenis)',
            data: 'totalExistingJenis'
        },
        {
            name: 'totalExistingKeseluruhan',
            title: 'Total Existing di Lokasi (Keseluruhan)',
            data: 'totalExistingKeseluruhan'
        },
        {
            name: 'rekomendasiHubla',
            title: 'Rekomendasi Hubla',
            data: 'rekomendasiHubla'
        },
        {
            name: 'totalKebutuhanHubla',
            title: 'Total Kebutuhan sesuai Hubla',
            data: 'totalKebutuhanHubla'
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
            name: 'persentaseHubla',
            title: 'Persentase Hubla',
            data: 'persentaseHubla',
            render: function (row, data, iDisplayIndex) {
                result = "";
                if (iDisplayIndex.peralatanOSR == "Total Persentase") {
                    result = "<b><i id='totalPersentaseHubla'></i></b>";
                } else {
                    if (iDisplayIndex.persentaseHubla !== null) {
                        if (iDisplayIndex.rekomendasiHubla > 0) {
                            result = iDisplayIndex.persentaseHubla + "%";
                            totalPersentaseHubla += parseFloat(iDisplayIndex.persentaseHubla);
                        } else {
                            result = "-";
                        }

                    }
                }

                return result;
            }
        },
        {
            name: 'rekomendasiOSCP',
            title: 'Rekomendasi OSCP',
            data: 'rekomendasiOSCP'
        },
        {
            name: 'totalKebutuhanOSCP',
            title: 'Total Kebutuhan sesuai OSCP',
            data: 'totalKebutuhanOSCP'
        },
        {
            name: 'selisihOSCP',
            title: 'Selisih OSCP',
            data: 'selisihOSCP'
        },
        {
            name: 'kesesuaianOSCP',
            title: 'Kesesuaian OSCP',
            target: -1,
            data: 'kesesuaianOSCP',
            render: function (row, data, iDisplayIndex) {
                var result = "";

                if (iDisplayIndex.kesesuaianPM58 == "TERPENUHI") {
                    result = "<b style='color:green;'>TERPENUHI</b>";
                } else if (iDisplayIndex.kesesuaianPM58 == "KURANG") {
                    result = "<b style='color:red;'>KURANG</b>";
                } else if (iDisplayIndex.kesesuaianPM58 == "TIDAK TERPENUHI") {
                    result = "<b style='color:darkorange;'>TIDAK TERPENUHI</b>";
                }

                return iDisplayIndex.kesesuaianPM58 + ": " + result;
            }
        },
        {
            name: 'persentaseOSCP',
            title: 'Persentase OSCP',
            data: 'persentaseOSCP',
            render: function (row, data, iDisplayIndex) {
                result = "";
                if (iDisplayIndex.peralatanOSR == "Total Persentase") {
                    result = "<b><i id='totalPersentaseOSCP'></i></b>";
                } else {
                    if (iDisplayIndex.persentaseOSCP !== null) {
                        if (iDisplayIndex.persentaseOSCP > 0) {
                            result = iDisplayIndex.persentaseOSCP + "%";
                            totalPersentaseOSCP += parseFloat(iDisplayIndex.persentaseOSCP);
                        } else {
                            result = "-";
                        }

                    }
                }
                

                return result;
            }
        },
        {
            name: 'brand',
            title: 'Brand',
            data: 'brand'
        },
        {
            name: 'serialNumber',
            title: 'Serial Number',
            data: 'serialNumber'
        },
        {
            name: 'noAsset',
            title: 'No Asset',
            data: 'noAsset'
        },
        {
            name: 'status',
            title: 'Status',
            target: -1,
            data: 'status',
            render: function (row, data, iDisplayIndex) {
                var result = "";

                if (iDisplayIndex.status == "STAND BY") {
                    result = "<b style='color:darkgrey;'>STAND BY</b>";
                } else if (iDisplayIndex.status == "RENTAL") {
                    result = "<b style='color:darkorange;'>RENTAL</b>";
                } else if (iDisplayIndex.status == "MAINTENANCE") {
                    result = "<b style='color:darkred;'>MAINTENANCE</b>";
                }

                return result;
            }
        },
        {
            name: 'createdBy',
            title: 'Created By',
            data: 'createdBy'
        },
        {
            name: 'createDate',
            title: 'Created At',
            data: 'createDate'
        },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {

                result = "";

                if (iDisplayIndex.peralatanOSR != "Total Persentase") {

                    result += "<div class='btn-group' role='group'>" +
                        "<button id='btnGroupVerticalDrop1' type='button' class='btn btn-primary btn-xs dropdown-toggle waves-effect waves-themed' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>Action</button>" +
                        "<div class='dropdown-menu' aria-labelledby='btnGroupVerticalDrop1'>" +
                        "<a class='dropdown-item' href='javascript:void(0)' onclick='llpTrxRemark(" + iDisplayIndex.id + ")' title='Remark'><b style='color:cornflowerblue;'><i class='fa fa-info-circle'></i> Remark</b></a>";

                    if (editable == "True") {
                        result += "<a class='dropdown-item' data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLLPHistoryStatus?llpTrxId=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "' title='Rental / Maintenance'><b style='color:teal;'><i class='fa fa-exchange'></i> Rental/Maintenance</b></a>" +
                            "<a class='dropdown-item' data-toggle='modal' data-backdrop='static' data-keyboard='false' data-target='#modal-add-edit' href='/Home/AddEditLLPTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' title='Edit'><b style='color:darkorange;'><i class='fa fa-pencil'></i> Edit</b></a>" +
                            "<a class='dropdown-item' data-toggle='modal' data-target='#modal-history' href='/HistoryTrx/HistoryLLPTrx?trxId=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' title='Edit'><b style='color:teal;'><i class='fa fa-history'></i> History</b></a>" +
                            "<a class='dropdown-item btn-delete' href='javascript:void(0)' onclick='deleteLLPTrx(" + iDisplayIndex.id + ")' title='Delete'><b style='color:red;'><i class='fa fa-trash'></i> Delete</b></a>";
                    }
                    
                    result += "</div>" +
                        "</div>";
                }

                return result;
            }
        },
        {
            "targets": -1,
            "data": null,
            "visible": false,
            "render": function (row, data, iDisplayIndex) {
                result = iDisplayIndex.qrCodeText;
                return result;
            }
        },
        {
            "targets": -1,
            "data": null,
            "visible": false,
            "render": function (row, data, iDisplayIndex) {
                result = base_api + "Public/Index?id=" + iDisplayIndex.id + "&port=" + port + "&year=" + selectedYear;
                return result;
            }
        }
    ],
    createdRow: function (row, data, dataIndex) {
        //if ((dataIndex % 2) == 0) {
        //    $(row).css("background-color", "#dde2ea1c");
        //}

        if (data.peralatanOSR === 'Total Persentase') {
            // Add COLSPAN attribute
            $('td:eq(0)', row).attr('colspan', 3);

            // Center horizontally
            $('td:eq(0)', row).attr('align', 'center');

            // Hide required number of columns
            // next to the cell with COLSPAN attribute
            $('td:eq(3)', row).css('display', 'none');
            $('td:eq(4)', row).css('display', 'none');

            // Update cell data
            this.api().cell($('td:eq(0)', row)).data('<b>Total Persentase Hubla & OSCP</b>');
            this.api().cell($('td:eq(7)', row)).data('');
            this.api().cell($('td:eq(8)', row)).data('');
            this.api().cell($('td:eq(9)', row)).data('');
            this.api().cell($('td:eq(10)', row)).data('');
            this.api().cell($('td:eq(11)', row)).data('');
            this.api().cell($('td:eq(14)', row)).data('');
            this.api().cell($('td:eq(15)', row)).data('');
            this.api().cell($('td:eq(16)', row)).data('');
            this.api().cell($('td:eq(17)', row)).data('');
        }
    },
    rowsGroup: [
        'totalExistingKeseluruhan:name',
        10,
        11,
        14,
        15,
        16,
        0,
        2,
        7,
        9
    ],
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": countTotalPercentageLLPTrx
});

dt.on('draw', function () {
    countTotalPercentageLLPTrx();
});


$("#table_llp_trx thead th input[type=text]").on('keyup change',
    function () {

        dt
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();

    });

function qrcodeClick(id) {
    $.ajax({
        url: base_api + "Home/GetLLPTrxById?id=" + id,
        method: 'GET',
        success: function (result) {
            Swal.fire({
                imageUrl: result.data.qrCode,
                imageAlt: 'QR Code',
                html: 'Kode: <a href="javascript:void(0)" class="openimage" data-base64="' + result.data.qrCode + '">' + result.data.qrCodeText + '</a> '
            })
        }
    });
}

function llpTrxRemark(id) {
    $.get(base_api + 'Home/GetLLPTrxById?id=' + id, function (result) {
        Swal.fire(
            'Remark',
            result.data.remark,
            'info'
        )
    });
}

$(document).on('click', '.openimage', function () {
    var base64 = $(this).attr("data-base64");
    var image = new Image();
    image.src = base64;
    var w = window.open("");
    w.document.write(image.outerHTML);
});

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
                countTotalPercentageLLPTrx();
            });
        } else if (result.isDenied) {

        }
    })
}
