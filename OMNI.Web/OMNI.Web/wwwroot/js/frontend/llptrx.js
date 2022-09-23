$('#table_llp_trx').DataTable().destroy();

var totalPersentaseHubla = 0;
var totalPersentaseOSCP = 0;
/* COLUMN FILTER  */
var dt = $('#table_llp_trx').DataTable({
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            text: '<i class="fal fa-download"></i> Export Excel',
            titleAttr: 'Generate Excel',
            className: 'btn btn-sm btn-outline-primary',
            title: 'Data LLP ' + formattedToday,
            exportOptions: {
                columns: [1, 2, 3, 4, 6, 8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19]
            }
        }
    ],
    "ordering": false,
    "scrollX": true,
    "scrollY": '400px',
    "scrollCollapse": true,
    "paging": false,
    "fixedColumns": {
        leftColumns: 7,
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
                    result = "<a data-toggle='modal' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_LLP' style='color:blue;' title='Gambar'><b><i>File Gambar</i></b></a>"
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

                if (iDisplayIndex.kesesuaianOSCP == "TERPENUHI") {
                    result = "<b style='color:green;'>TERPENUHI</b>";
                } else if (iDisplayIndex.kesesuaianOSCP == "KURANG") {
                    result = "<b style='color:red;'>KURANG</b>";
                }

                return result;
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
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {

                result = "";

                if (iDisplayIndex.peralatanOSR != "Total Persentase") {
                    result += "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLLPTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deleteLLPTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
                }

                return result;
            }
        }
    ],
    createdRow: function (row, data, dataIndex) {
        console.log(dataIndex);
        console.log(row);
        console.log(data);

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
        }
    },
    rowsGroup: [
        'peralatanOSR:name',
        'satuanJenis:name',
        'totalExistingJenis:name',
        'totalExistingKeseluruhan:name',
        'rekomendasiHubla:name',
        'totalKebutuhanHubla:name',
        'selisihHubla:name',
        'kesesuaianPM58:name',
        'persentaseHubla:name',
        'rekomendasiOSCP:name',
        'totalKebutuhanOSCP:name',
        'selisihOSCP:name',
        'kesesuaianOSCP:name',
        'persentaseOSCP:name',
    ],
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": function (settings, json) {
        var lastJenis = "";
        var totalPersentaseHubla = 0;
        var totalPersentaseOSCP = 0;
        var countRekomendasiHubla = 0;
        var countRekomendasiOSCP = 0;
        //count total persentase hubla
        $.ajax({
            url: base_api + 'Home/GetAllLLPTrx?port=' + port + "&year=" + selectedYear,
            method: "GET",
            success: function (result) {
                if (result.data !== null && result.data !== undefined) {
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
                html: '<a href="javascript:void(0)" class="openimage" data-base64="' + result.data.qrCode + '">' + result.data.qrCodeText + '</a> '
            })
        }
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
            });
        } else if (result.isDenied) {

        }
    })
}
