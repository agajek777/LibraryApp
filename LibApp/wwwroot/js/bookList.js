var dataTable;

$(document).ready(function () {
    console.log("x");
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/books/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "10%" },
            { "data": "isbn", "width": "1%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/reservations/new?id=${data}" class="btn btn-info text-white" style="cursor:pointer; width:100px;">
                            Rent
                        </a>
                        &nbsp;
                        <a href="/books/Edit?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                            Edit
                        </a>
                        &nbsp;
                        <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;"
                            onclick=Delete('/api/books/'+${data})>
                            Delete
                        </a>
                        </div>`
                }, "width": "18%"
            }
        ],
        "language": {
            "emptyTable": "No data found"
        },
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