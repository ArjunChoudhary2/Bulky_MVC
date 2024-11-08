﻿let DataTable;


$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) loadDataTable("inprocess");
    else if (url.includes("completed")) loadDataTable("completed");
    else if (url.includes("pending")) loadDataTable("pending");
    else if (url.includes("approved")) loadDataTable("approved");
    else loadDataTable("all");
    loadDataTable();
})

function loadDataTable(status) {

    DataTable = $('#myTable').DataTable({
        "ajax":
        {
            url: '/admin/order/getall?status=' + status
        },
        "columns": [
            { data: 'orderId', "width": '10%' },
            { data: 'name', "width": '15%' },
            { data: 'phoneNumber', "width": '15%' },
            { data: 'applicationUser.email', "width": '40%' },
            {
                data: 'orderStatus', width: '15%'
            }, {
                data: 'orderTotal', width: '15%'
            },
            {
                data: 'orderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
                     </div>` }
                , "width": '20%'
            }

        ]
    });
}
