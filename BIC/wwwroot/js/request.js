jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        var url = window.location.search;

        if (url.includes("submitted")) {
            loadDataTable("GetAllSubmittedRequest");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("GetAllPendingRequest");
            }
            else {
                if (url.includes("workCompleted")) {
                    loadDataTable("GetAllCompletedRequest");
                }
                else {
                    if (url.includes("invoiceSubmitted")) {
                        loadDataTable("GetAllInvoiceSubmittedRequest");
                    }
                    else {
                        if (url.includes("paymentRecieved")) {
                            loadDataTable("GetAllPaymentRequest");
                        }
                        else {
                            loadDataTable("GetAllRequest");
                        }
                    }
                }
            }
        }
    })
})

var dataTable;

function loadDataTable(url) {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "Request/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "firstName", "width": "20%" },
            { "data": "companyName", "width": "20%" },
            { "data": "branch", "width": "20%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class = "text-center">
                                <a href = "/Admin/Request/Detail/${data}" class = "btn btn-primary border rounded" style = "cursor:pointer; width:40px;">
                                    <i class = "fas fa-eye" style = "color:white"></i>
                                </a>
                                &nbsp;
                                <a onclick = Delete('/Admin/Request/Delete/${data}') class = "btn btn-danger border rounded" style = "cursor:pointer; width: 40px;">
                                    <i class = "fas fa-trash-alt" style = "color: white"></i>
                                </a>
                            </div>`
                },
                "width": "15%"
            }
        ],
        "language": {
            "emptyTable": "No Record Found!"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "ARE YOU SURE YOU WANT TO DELETE?",
        text: "YOU WILL NOT BE ABLE TO RESTORE IT!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#BD6B55",
        confirmButtonText: "Yes, Delete it!",
        closeOnConfirm: true
    },
        function () {
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
    );
}