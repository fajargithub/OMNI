var pagefunction_2 = function () {
    var responsiveHelper_dt_2_basic = undefined;
    var responsiveHelper_table_personil = undefined;
    var responsiveHelper_datatable_col_reorder = undefined;
    var responsiveHelper_datatable_tabletools = undefined;

    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    $('#dt_2_basic').dataTable({
        "sDom": "<'Usert-tooViewBaRoleConstantg'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt_2-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "autoWidt_2h": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_dt_2_basic) {
                responsiveHelper_dt_2_basic =
                    new ResponsiveDatatablesHelper($('#dt_2_basic'), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_dt_2_basic.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_dt_2_basic.respond();
        }
    });

    function format(d) {
        var result = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;margin: 5px; text-align:left;"><tr>' +
            '<td><b>Persentase OSCP</b></td>' +
            '<td style="padding-right: 10px;">:</td>' +
            '<td>' + d.name + '</td>' +
            '</tr>' +
            '</table>';

        return result;
    }


    /* COLUMN FILTER  */
    var dt_2 = $('#table_personil_trx').DataTable({
        "sDom": "<'dt_2-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt_2-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "processing": true,
        "serverSide": false,
        "ordering": false,
        "responsive": false,
        "ajax": {
            "url": base_api + 'Home/GetAllPersonilTrx?port=' + port,
            "type": 'GET'
        },
        "columns": [
            //{
            //    "class": "details-control",
            //    "orderable": false,
            //    "data": null,
            //    "defaultContent": "",
            //    "render": function (row, data, iDisplayIndex) {
            //        var result = "<a href='javascript:void(0)' style='color:green;'><i class='fa fa-chevron-circle-down'></i></a>";
            //        return result;
            //    }
            //},
            { "data": null },
            { "data": "personil" },
            { "data": "name" },
            { "data": "satuan" },
            { "data": "totalDetailExisting" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/IndexFile?trxId=" + iDisplayIndex.id + "&flag=OMNI_PERSONIL' style='color:blue;' title='Gambar'><b><i>File Sertifikat</i></b></a>";
                }
            },
            { "data": "tanggalPelatihan" },
            { "data": "tanggalExpired" },
            { "data": "sisaMasaBerlaku" },
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
            { "data": "persentasePersonil" },
            {
                "targets": -1,
                "data": null,
                "render": function (row, data, iDisplayIndex) {
                    return "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditPersonilTrx?id=" + iDisplayIndex.id + "&port=" + port + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deletePersonilTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
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

    dt_2.on('order.dt_2 search.dt_2', function () {
        dt_2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $("#table_personil_trx thead th input[type=text]").on('keyup change',
        function () {

            dt_2
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });
    /* END COLUMN FILTER */

    /* COLUMN SHOW - HIDE */
    //$('#datatable_col_reorder').dataTable({
    //    "sDom": "<'dt_2-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'C>r>" +
    //        "t" +
    //        "<'dt_2-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
    //    "oLanguage": {
    //        "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
    //    },
    //    "autoWidt_2h": true,
    //    "preDrawCallback": function () {
    //        // Initialize the responsive datatables helper once.
    //        if (!responsiveHelper_datatable_col_reorder) {
    //            responsiveHelper_datatable_col_reorder =
    //                new ResponsiveDatatablesHelper($('#datatable_col_reorder'), breakpointDefinition);
    //        }
    //    },
    //    "rowCallback": function (nRow) {
    //        responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
    //    },
    //    "drawCallback": function (oSettings) {
    //        responsiveHelper_datatable_col_reorder.respond();
    //    }
    //});

    //$('#table_personil_trx tbody').on('click', 'td.details-control', function () {
    //    var tr = $(this).closest('tr');
    //    var row = dt_2.row(tr);

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

    //dt_2.on('draw', function () {
    //    $.each(detailRows, function (i, id) {
    //        $('#' + id + ' td.details-control').trigger('click');
    //    });
    //});
    // Apply the filter
    $("#table_personil_trx thead th input[type=text]").on('keyup change',
        function () {

            dt_2
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });
    /* END COLUMN FILTER */

    /* COLUMN SHOW - HIDE */
    //$('#datatable_col_reorder').dataTable({
    //    "sDom": "<'dt_2-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'C>r>" +
    //        "t" +
    //        "<'dt_2-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
    //    "oLanguage": {
    //        "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
    //    },
    //    "autoWidt_2h": true,
    //    "preDrawCallback": function () {
    //        // Initialize the responsive datatables helper once.
    //        if (!responsiveHelper_datatable_col_reorder) {
    //            responsiveHelper_datatable_col_reorder =
    //                new ResponsiveDatatablesHelper($('#datatable_col_reorder'), breakpointDefinition);
    //        }
    //    },
    //    "rowCallback": function (nRow) {
    //        responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
    //    },
    //    "drawCallback": function (oSettings) {
    //        responsiveHelper_datatable_col_reorder.respond();
    //    }
    //});
}

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
                $("#table_personil_trx").DataTable().ajax.reload(null, false);
            });
        } else if (result.isDenied) {

        }
    })
}

pagefunction_2();