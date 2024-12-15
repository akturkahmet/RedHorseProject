﻿$(document).ready(function () {

    createEditReservationTable()
});


//function openCreateReservationModal() {
//    var reservationDay = $("#reservationDay1").val();
//    var reservationHour = $("#reservationHour1").val();
//    var tourType = $("#TourType1").val();
//    var CustomerCount = $("#CustomerCount1").val();

//    if (tourType == null || reservationDay == null || reservationHour == null) {
//        Swal.fire({
//            icon: 'error',
//            title: 'Hata',
//            text: "Lütfen tüm alanları doldurunuz!",
//            showConfirmButton: true,
//            confirmButtonText: 'Tamam'
//        });
//        return;
//    }

//    $.ajax({
//        url: '/Customer/ControlReservationDate',
//        method: 'POST',
//        data: {
//            ReservationDate: reservationDay + " " + reservationHour,
//            TourTypeId: tourType,
//            Hour: reservationHour,
//            CustomerCount: CustomerCount

//        },
//        success: function (response) {
//            if (response.success) {
//                fetch('/Customer/frmCreateReservation')
//                    .then(response => {
//                        if (!response.ok) {
//                            throw new Error('Network response was not ok ' + response.statusText);
//                        }
//                        return response.text();
//                    })
//                    .then(html => {
//                        document.getElementById('Body').innerHTML = html;

//                        $('#myModal').modal('show'); const modal = document.getElementById('myModal');
//                        modal.style.display = 'block';
//                        modal.classList.add('show');

//                        $("#TourType").val(tourType);
//                        $("#ReservationDay").val(reservationDay);
//                        $("#ReservationHour").val(reservationHour);
//                        $("#CustomerCount").val(CustomerCount);
//                    })
//                    .catch(error => {
//                        console.error('Error loading modal content:', error);
//                        Swal.fire({
//                            icon: 'error',
//                            title: 'Hata',
//                            text: 'Modal içeriği yüklenirken bir hata oluştu.',
//                            showConfirmButton: true,
//                            confirmButtonText: 'Tamam'
//                        });
//                    });

//            } else {
//                Swal.fire({
//                    icon: 'error',
//                    title: 'Hata',
//                    text: response.message,
//                    showConfirmButton: true,
//                    confirmButtonText: 'Tamam'
//                });
//            }
//        },
//        error: function (error) {
//            console.error("Hata oluştu:", error);
//            Swal.fire({
//                icon: 'error',
//                title: 'Hata',
//                text: 'Tarih kontrolü yapılırken bir hata oluştu.',
//                showConfirmButton: true,
//                confirmButtonText: 'Tamam'
//            });
//        }
//    });
//}

function openCreateReservationModal() {
    var reservationDay = $("#reservationDay1").val();
    var reservationHour = $("#reservationHour1").val();
    var tourType = $("#TourType1").val();
    var CustomerCount = $("#CustomerCount1").val();

    if (validateForm("reservationForm")) {
        $.ajax({
            url: '/Customer/ControlReservationDate',
            method: 'POST',
            data: {
                ReservationDate: reservationDay + " " + reservationHour,
                TourTypeId: tourType,
                Hour: reservationHour,
                CustomerCount: CustomerCount
            },
            success: function (response) {
                if (response.success) {
                    // Eğer tarih uygunsa modal içeriğini yükle
                    fetch('/Customer/frmCreateReservation')
                        .then(response => {
                            if (!response.ok) {
                                throw new Error('Network response was not ok ' + response.statusText);
                            }
                            return response.text();
                        })
                        .then(html => {
                            // Modal başlığını ayarla ve içeriği yükle
                            document.getElementById('Body').innerHTML = html;
                            document.getElementById('modalTitle').textContent = 'Rezervasyon Oluştur';

                            // Modal'ı göster
                            const modal = document.getElementById('myModal');
                            modal.style.display = 'block';
                            modal.classList.add('show');
                            document.body.classList.add('modal-open');

                            // Form alanlarını doldur
                            $("#TourType").val(tourType);
                            $("#ReservationDay").val(reservationDay);
                            $("#ReservationHour").val(reservationHour);
                            $("#CustomerCount").val(CustomerCount);
                        })
                        .catch(error => {
                            console.error('Modal içeriği yüklenirken bir hata oluştu:', error);
                            Swal.fire({
                                icon: 'error',
                                title: 'Hata',
                                text: 'Modal içeriği yüklenirken bir hata oluştu.',
                                showConfirmButton: true,
                                confirmButtonText: 'Tamam'
                            });
                        });

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata',
                        text: response.message,
                        showConfirmButton: true,
                        confirmButtonText: 'Tamam'
                    });
                }
            },
            error: function (error) {
                Alert(4)

            }
        });
    }
    else {
        Alert(3)
    }

}

function createEditReservationTable() {
    editReservationTable = $('#EditReservation').DataTable({
        ajax: {
            url: '/Customer/GetRezervation',
            dataSrc: ''
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Excel Olarak Dışa Aktar',
                filename: 'Rezervasyonlar',
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF Olarak Dışa Aktar',
                filename: 'Rezervasyonlar',
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            }
        ],
        pageLength: 10,
        lengthMenu: [5, 10, 25, 50, 100],
        columns: [
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'Phone' },
            { data: 'HotelName' },
            { data: 'PassportNo' },
            { data: 'HotelRoomNo' },
            { data: 'CustomerCount' },
            { data: 'CreatedDate' },
            { data: 'ReservationDate' },
            {
                data: null,
                render: function (data, type, row) {
                    return `
                        <p class="${row.Status ? 'color-green bold' : 'color-red bold'}" 
                        id="btn-edit">
                        ${row.Status ? 'Aktif' : 'Pasif'}
                        </p>`;
                }

            },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="openEditModal(${row.Id})" class="btn btn-primary" data-id="${row.Id}" id="btn-edit">Düzenle</button>`;
                }
            }
        ]
    });
}





function createReservation() {
    var formData = $("#createReservationForm").serializeArray();
    var reservationDay = $("#ReservationDay").val();
    var reservationHour = $("#ReservationHour").val();

    formData.push({
        name: "ReservationDate",
        value: reservationDay + " " + reservationHour
    });
    if (validateForm("createReservationForm")) {
        $.ajax({
            url: '/Customer/CreateReservation',
            method: 'POST',
            data: formData,
            success: function (response) {
                Alert(1, "Başarılı", "Rezervasyon başarıyla oluşturuldu.")
                $('#myModal').hide();
            },
            error: function (error) {
                console.log("error:" + error);
            }
        });
    }
    else {
        Alert(3)

    }
}


function updateReservation() {
    var formData = $("#EditReservationForm").serialize();
    if (validateForm("EditReservationForm")) {
        $.ajax({
            url: '/Customer/UpdateReservation',
            method: 'POST',
            data: formData,
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarıyla Güncellendi!',
                    text: 'Rezervasyon başarıyla güncellendi.',
                    showConfirmButton: true,
                    confirmButtonText: 'Tamam'
                }).then((result) => {
                    if (result.isConfirmed) {
                        refreshTable()
                        $("#myModal").hide()
                    }
                });

            },
            error: function (error) {
                console.log("error:" + error);
            }
        });
    }
    else {
        Alert(3)

    }
}
function updateAgencyInformation() {
    var formData = $("#EditCustomerInformationForm").serialize();
    $.ajax({
        url: '/Customer/UpdateAgencyInformation',
        method: 'POST',
        data: formData,
        success: function (response) {
            Swal.fire({
                icon: 'success',
                title: 'Başarıyla Güncellendi!',
                text: 'Bilgilerin başarıyla güncellendi.',
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload()
                }
            });

        },
        error: function (error) {
            console.log("error:" + error);
        }
    });
}


$('#showPassword').change(function () {
    var passwordField = $('.togglePassword');

    if ($(this).is(':checked')) {
        passwordField.attr('type', 'text');
    } else {
        passwordField.attr('type', 'password'); // Şifreyi gizle
    }
});


function changePassword() {
    var formData = $("#changePasswordForm").serialize();
    $.ajax({
        url: '/Customer/ChangeCustomerPassword',
        method: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarıyla Güncellendi!',
                    text: response.message,
                    showConfirmButton: true,
                    confirmButtonText: 'Tamam'
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata',
                    text: response.message,
                    showConfirmButton: true,
                    confirmButtonText: 'Tamam'
                });
            }
        },

        error: function (xhr) {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: xhr.responseJSON?.message || 'Bir hata oluştu. Lütfen tekrar deneyin.',
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            });
        }
    });
}







function ControlHours(TourTypeId) {
    $.ajax({
        url: '/Customer/ControlHours',
        method: 'Get',
        data: {
            TourTypeId: TourTypeId
        },
        success: function (response) {
            $('#reservationHour1').empty();
            $('#reservationHour1').append(new Option('Seç', ''));
            response.forEach(function (hour) {
                var option = new Option(hour.Hour);
                option.value = hour.Hour;
                if (hour.Status == 0) {
                    option.disabled = true;
                    option.style.color = "red";
                    option.style.opacity = 0.5;
                }

                $('#reservationHour1').append(option);
            });
        },

        error: function (xhr) {

        }
    });
}

function controlHourCapacity() {
    var TourTypeId = $("#TourType1").val()
    var Hour = $("#reservationHour1").val()
    var ReservationDate = $("#reservationDay1").val()
    if (TourTypeId && Hour && ReservationDate) {
        $.ajax({
            url: '/Customer/ControlHourCapacity',
            method: 'GET',
            data: {
                TourTypeId: TourTypeId,
                Hour: Hour,
                ReservationDate: ReservationDate
            },
            success: function (response) {

                $('#remainingCapacity').text(response);
            },

            error: function (xhr) {

            }
        });
    }
    else {
        Alert(3,"Hata","Lütfen Tur tipi,tarihi ve saati doldurunuz.")
    }

}






//grafikler
const data = [
    { "date": "2024-12-01", "sales": 10 },
    { "date": "2024-12-02", "sales": 15 },
    { "date": "2024-12-03", "sales": 8 },
    { "date": "2024-12-04", "sales": 12 },
    { "date": "2024-12-05", "sales": 20 }
];
$(document).ready(function () {
    // Tarih ve satış verilerini ayır
    const dates = data.map(item => item.date);
    const sales = data.map(item => item.sales);

    // Chart.js bar grafiği
    const ctx = document.getElementById('salesChart').getContext('2d');
    const salesChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: dates,
            datasets: [{
                label: 'Satış Adetleri',
                data: sales,
                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'Tarihe Göre Satış Adetleri'
                }
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Tarih'
                    }
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Satış Adetleri'
                    }
                }
            }
        }
    });
});