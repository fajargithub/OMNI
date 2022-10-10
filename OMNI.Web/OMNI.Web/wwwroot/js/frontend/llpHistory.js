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
                    result = "<b style='color:darkyellow;'>RENTAL</b>";
                } else if (iDisplayIndex.status == "MAINTENANCE") {
                    result = "<b style='color:darkorange;'>MAINTENANCE</b>";
                }

                return result;
            }
        },
        { "data": "startDate" },
        { "data": "endDate" },
        {
            "targets": -1,
            "data": null,
            "render": function (row, data, iDisplayIndex) {
                var result = "";

                if (iDisplayIndex.latihan != "Total Persentase") {
                    result += "<a data-toggle='modal' data-target='#modal-add-edit' href='/Home/AddEditLatihanTrx?id=" + iDisplayIndex.id + "&port=" + port.replace(" ", "%20") + "&year=" + selectedYear + "' style='color:orange;' title='Edit'><i class='fa fa-pencil'></i></a> &nbsp;" +
                        " <a href='javascript:void(0)' onclick='deleteLatihanTrx(" + iDisplayIndex.id + ")' class='btn-delete' title='Delete' style='color:red;'><i class='fa fa-trash'></i></a>";
                }
                return result;
            }
        },
    ],
    createdRow: function (row, data, dataIndex) {
        
    },
    "order": [[1, 'asc']],
    rowCallback: function (row, data, iDisplayIndex) {
    },
    "initComplete": function (row, data, iDisplayIndex) { }
});

