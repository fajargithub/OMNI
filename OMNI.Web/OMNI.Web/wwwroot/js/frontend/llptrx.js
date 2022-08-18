var pagefunction = function () {
    var responsiveHelper_dt_basic = undefined;
    var responsiveHelper_table_llp_trx = undefined;
    var responsiveHelper_datatable_col_reorder = undefined;
    var responsiveHelper_datatable_tabletools = undefined;

    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    $('#dt_basic').dataTable({
        "sDom": "<'Usert-tooViewBaRoleConstantg'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "autoWidth": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_dt_basic) {
                responsiveHelper_dt_basic =
                    new ResponsiveDatatablesHelper($('#dt_basic'), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_dt_basic.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_dt_basic.respond();
        }
    });

    function format(d) {

        var result = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;margin: 5px; text-align:left;">' +
            '<tr>' +
            '<td><b>Rekomendasi Hubla</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.rekomendasiHubla + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><b>Total Kebutuhan Sesuai Hubla</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.totalKebutuhanHubla + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><b>Selisih Hubla</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.selisihHubla + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><b>Kesesuaian PM.58</b></td>' +
            '<td style="padding-right: 10px;">:</td>';

            if (d.kesesuaianPM58 == "TERPENUHI") {
                result += "<td><b style='color:green;'>TERPENUHI</b></td>";
            } else if (d.kesesuaianPM58 == "KURANG") {
                result += "<td><b style='color:red;'>KURANG</b></td>";
            }

        result += '</tr>';
            result += '<tr>' +
            '<td><b>Persentase Hubla</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.persentaseHubla + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><b>Rekomendasi OSCP</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.rekomendasiOSCP + '</td>' +
            '</tr>' +
                '<tr>' +
                '<td><b>Total Kebutuhan Sesuai OSCP</b></td>' +
                '<td style="padding-right: 10px;">:</td>' +
                '<td>' + d.totalKebutuhanOSCP + '</td>' +
                '</tr>' +
                '<tr>' +
                '<td><b>Selisih OSCP</b></td>' +
                '<td style="padding-right: 10px;">:</td>' +
                '<td>' + d.selisihOSCP + '</td>' +
                '</tr>' +

            '<tr>' +
            '<td><b>Kesesuaian OSCP</b></td>' +
            '<td style="padding-right: 10px;">:</td>';

        if (d.kesesuaianOSCP == "TERPENUHI") {
            result += "<td><b style='color:green;'>TERPENUHI</b></td>";
        } else if (d.kesesuaianOSCP == "KURANG") {
            result += "<td><b style='color:red;'>KURANG</b></td>";
        }

        result += '</tr>';
        result += '<tr>' +
            '<td><b>Persentase OSCP</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.persentaseOSCP + '</td>' +
            '</tr>' +
            '</table>';

        return result;
    }

    $('#table_llp_trx').DataTable().destroy();

    /* COLUMN FILTER  */
    var dt = $('#table_llp_trx').DataTable({
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "processing": true,
        "serverSide": false,
        "ordering": false,
        "responsive": false,
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
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLLPTrx?id=" + iDisplayIndex.id + "&port=" + port + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
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

    /* COLUMN SHOW - HIDE */
    $('#datatable_col_reorder').dataTable({
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'C>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "autoWidth": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_col_reorder) {
                responsiveHelper_datatable_col_reorder =
                    new ResponsiveDatatablesHelper($('#datatable_col_reorder'), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_col_reorder.respond();
        }
    });

    //$('#table_llp_trx tbody').on('click', 'td.details-control', function () {
    //    var tr = $(this).closest('tr');
    //    var row = dt.row(tr);

    //    if (row.child.isShown()) {
    //        row.child.hide();
    //        tr.removeClass('shown');
    //    }
    //    else {
    //        // Open this row
    //        row.child(format(row.data())).show();
    //        tr.addClass('shown');
    //    }
    //});

    //var detailRows = [];

    //dt.on('draw', function () {
    //    $.each(detailRows, function (i, id) {
    //        $('#' + id + ' td.details-control').trigger('click');
    //    });
    //});

    $("#table_llp_trx thead th input[type=text]").on('keyup change',
        function () {

            dt
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });
    /* END COLUMN FILTER */

    /* COLUMN SHOW - HIDE */
    $('#datatable_col_reorder').dataTable({
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'C>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "autoWidth": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_col_reorder) {
                responsiveHelper_datatable_col_reorder =
                    new ResponsiveDatatablesHelper($('#datatable_col_reorder'), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_col_reorder.respond();
        }
    });
}

function qrcodeClick(id) {
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
                $("#table_llp_trx").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}

pagefunction();