$(document).ready(function () {
    getMakes();     
});

$("#MakeId").click(function (event) {
    getModels();
})

function getMakes() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:56259/api/MakeApi',
        success: function (data, status) {
            $.each(data, function (index, item) {
                var option = '<option value="' + item.MakeId + '">' + item.Name + '</option>';
                $("#MakeId").append(option);             
            }); 
            
            $('.selDiv option:contains("' + $("#savedMake").text() + '")').attr('selected', 'selected');
            
            getModels();   
            setTimeout(function () { popModels() }, 50);
        },
        error: function () {
            alert('it broke tho');
        }
    });
}

function getModels() {
    $("#ModelId").text("");
    $.ajax({
        type: 'GET',
        url: 'http://localhost:56259/api/ModelApi/' + $("#MakeId").val(),
        success: function (data, status) {
            $.each(data, function (index, item) {
                var option = '<option value="' + item.ModelId + '">' + item.ModelName + '</option>';
                $('#ModelId').append(option);
            });            
        },
        error: function () {

        }
    });
}


function popModels() {  
    $('.selDive option:contains("' + $("#savedModel").text() + '")').attr('selected', 'selected');
}