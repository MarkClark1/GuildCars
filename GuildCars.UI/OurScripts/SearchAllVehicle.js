$(document).ready(function () {

});

$("#search-button").click(function (event) {
    SearchVehicles();
});


function SearchVehicles() {
    $('#results').text("");
    var searchInput = {
        MinYear: $("#min-year-input").val(),
        MaxYear: $("#max-year-input").val(),
        MinPrice: $("#min-price-input").val(),
        MaxPrice: $("#max-price-input").val(),
        MakeModel: $("#make-model-input").val()
    }

    $.ajax({
        url: 'http://localhost:56259/api/inventoryapi/SearchAllVehicles',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $.each(data, function (index, item) {
                var row = '<div class="border row" style="margin-bottom: 15px;">';

                row += '<div class="col-md-3">';
                row += '<h6>' + item.Year + ' ' + item.Make + ' ' + item.Model + '</h6>';
                row += '<img src="../Images/Inventory-' + item.VehicleId + '.jpg" style="width: 200px; height: auto;">';
                row += '</div>';

                row += '<div class="col-md-3">';
                row += 'Body Style: ' + item.Style + '<br/>';
                row += 'Trans: ' + item.Transmission + '<br/>';
                row += 'Color: ' + item.Color + '<br/>';
                row += '</div>';


                row += '<div class="col-md-3">';
                row += 'Interior: ' + item.Interior + '<br/>';
                row += 'Mileage: ' + item.Mileage + '<br/>';
                row += 'VIN #: ' + item.Vin + '<br/>';
                row += '</div>';


                row += '<div class="col-md-3">';
                row += 'Sale Price: $' + item.SalePrice + '<br/>';
                row += 'MSRP: $' + item.MSRP + '<br/>';
                row += '<a href="http://localhost:56259/Sales/Purchase/' + item.VehicleId + '" class = "btn btn-dark">Purchase</a>';
                row += '</div>';
                row += '</div>';
                $('#results').append(row);
            });
        },
        data: JSON.stringify(searchInput)
    });
}