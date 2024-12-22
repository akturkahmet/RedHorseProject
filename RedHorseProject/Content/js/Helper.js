$(document).ready(function () {

    var today = new Date().toISOString().split("T")[0];

    //$('input[type="date"]').attr("min", today);

    //$('input[type="date"]').on("change", function () {
    //    var selectedDate = $(this).val();
    //    if (selectedDate < today) {
    //        $(this).val(""); 
    //    }
    //});

    //Loading Ekranı
    $(document).ajaxStart(function () {
        $("#loading").fadeIn();
    });

    $(document).ajaxStop(function () {
        $("#loading").fadeOut();
    });
    //Loading Ekranı
    function closeModal() {
        $('#myModal').hide();
    }


  
});



function Destroy(id) {
    const tableElement = $(`#${id}`);
    if ($.fn.DataTable.isDataTable(tableElement)) {
        tableElement.DataTable().clear().destroy();
    }
}



$("#reservationDay1").prop("disabled", true);
$("#reservationHour1").prop("disabled", true);

$("#TourType1").change(function () {
    var tourType = $(this).val();
    if (tourType !== "seçiniz") {
        $("#reservationDay1").prop("disabled", false);
    } else {
        $("#reservationDay1").prop("disabled", true);
        $("#reservationHour1").prop("disabled", true);
        $('#reservationHour1').empty();
        $('#reservationHour1').append(new Option('Seç', ''));
    }
});