console.log('Hello')

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    DataTable = $('#myTable').DataTable({
        "ajax":
        { url:'/admin/product/getall'
        }, "columns" : [
            { data: 'title', width : '15%' },
            { data: 'isbn', width: '15%' },
            { data: 'price', width: '15%' },
            { data: 'author', width: '15%' },
            {
                data: 'category.name', width: '15%'
            },
            {  width: '15%' }

        ]
    });
}