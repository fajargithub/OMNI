var pageFunction_3 = function () {
    var responsiveHelper_dt_3_basic = undefined;
    var responsiveHelper_table_latihan = undefined;
    var responsiveHelper_datatable_col_reorder = undefined;
    var responsiveHelper_datatable_tabletools = undefined;

    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    $('#dt_3_basic').dataTable({
        "sDom": "<'Usert-tooViewBaRoleConstantg'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt_3-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "autoWidt_3h": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_dt_3_basic) {
                responsiveHelper_dt_3_basic =
                    new ResponsiveDatatablesHelper($('#dt_3_basic'), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_dt_3_basic.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_dt_3_basic.respond();
        }
    });

    function format(d) {
        var result = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;margin: 5px; text-align:left;"><tr>' +
            '<td><b>Persentase OSCP</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.latihan + '</td>' +
            '</tr>' +
            '</table>';

        return result;
    }


    /* COLUMN FILTER  */
    var dt_3 = $('#table_latihan_trx').DataTable({
        "sDom": "<'dt_3-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt_3-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "processing": true,
        "serverSide": false,
        "ordering": false,
        "responsive": false,
        "ajax": {
            "url": base_api + 'Home/GetAllLatihanTrx?port=' + port,
            "type": 'GET'
        },
        "columns": [
            { "data": null },
            { "data": "latihan" },
            { "data": "satuan" },
            { "data": "tanggalPelaksanaan" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-file' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_LATIHAN' style='color:blue;' title='Gambar'><b><i>File Dokumen</i></b></a>";
                }
            },
            { "data": "rekomendasiHubla" },
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
            { "data": "persentaseLatihan" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLatihanTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deleteLatihanTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
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

    dt_3.on('order.dt_3 search.dt_3', function () {
        dt_3.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $("#table_latihan_trx thead th input[type=text]").on('keyup change',
        function () {

            dt_3
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });

    // Apply the filter
    $("#table_latihan_trx thead th input[type=text]").on('keyup change',
        function () {

            dt_3
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });
}

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

pageFunction_3();