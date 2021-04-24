$(document).ready(function () {

});

$("#search-button").click(function (event) {
    GetReports();
});


function GetReports() {
    $('#user-table').text("");
    var searchInput = {
        UserId: $("#select-user").val(),
        FromDate: $("#from-date").val(),
        ToDate: $("#to-date").val(),
    }

    $.ajax({
        url: 'http://localhost:56259/api/inventoryapi/GetSalesReport',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $.each(data, function (index, item) {
                
                var row = '<tr>'
                row += '<td>' + item.FullName + '</td>';
                row += '<td>' + item.TotalSalesAmount + '</td>';
                row += '<td>' + item.TotalVehiclesSold + '</td>'
                row += '</tr>';
                $('#user-table').append(row);
            });
        },
        data: JSON.stringify(searchInput)
    });
}