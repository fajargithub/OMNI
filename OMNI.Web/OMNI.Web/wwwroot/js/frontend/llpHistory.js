const today = new Date();
const yyyy = today.getFullYear();
let mm = today.getMonth() + 1; // Months start at 0!
let dd = today.getDate();

if (dd < 10) dd = '0' + dd;
if (mm < 10) mm = '0' + mm;

const formattedToday = dd + '/' + mm + '/' + yyyy;

$('#table_history_llp_status').DataTable().destroy();

/* COLUMN FILTER  */
var table_history_llp_status = $('#table_history_llp_status').DataTable({
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            text: '<i class="fal fa-download"></i> Export Excel',
            titleAttr: 'Generate Excel',
            className: 'btn btn-sm btn-outline-primary',
            title: 'Data LLP History Status ' + formattedToday,
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
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
        "url": base_api + 'LLPHistoryStatus/GetAll?port=' + port + "&year=" + selectedYear,
        "type": 'GET'
    },
    "columns": [
        { "data": "peralatanOSR" },
        { "data": "jenis" },
        { "data": "satuan" },
        { "data": "detailExisting" },
        { "data": "noAsset" },
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
        { "data": "portFrom" },
        { "data": "portTo" },
        { "data": "startDate" },
        { "data": "endDate" },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                var result = "";

                if (iDisplayIndex.latihan != "Total Persentase") {
                    result += "<a class='dropdown-item' href='javascript:void(0)' onclick='llpHistoryRemark(" + iDisplayIndex.id + ")' title='Remark'><b style='color:cornflowerblue;'><i class='fa fa-info-circle'></i> Remark</b></a>";
                }
                return result;
            }
        }
    ],
    createdRow: function (row, data, dataIndex) {
        
    },
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": function (row, data, iDisplayIndex) { }
});

function llpHistoryRemark(id) {
    $.get(base_api + 'LLPHistoryStatus/GetById?id=' + id, function (result) {
        Swal.fire(
            'Remark',
            result.data.remark,
            'info'
        )
    });
}

