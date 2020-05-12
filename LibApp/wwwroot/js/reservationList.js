var dataTable;

$(document).ready(function () {
    loadReservations();
});

function loadReservations() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            'url': '/reservations/getactive',
            'method': 'GET',
            'datatype': 'json'
        },
        "columns": [
            { "data": "id", "width": "1%" },
            { "data": "client.name", "width": "25%" },
            { "data": "book.name", "width": "25%" },
            { "data": "targetDate", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;"
                                 onclick=Delete('/reservations/delete?id='+${data})>
                                 Terminate
                                </a>
                            </div>`
                }
            }
        ],
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        buttons: true,
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}