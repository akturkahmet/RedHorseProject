﻿$(document).ready(function () {

    createEditReservationTable()
});


function openCreateReservationModal() {
    var reservationDay = $("#reservationDay1").val();
    var reservationHour = $("#reservationHour1").val();
    var tourType = $("#TourType1").val();
    var CustomerCount = $("#CustomerCount1").val();

    if (tourType == null || reservationDay == null || reservationHour == null) {
        Swal.fire({
            icon: 'error',
            title: 'Hata',
            text: "Lütfen tüm alanları doldurunuz!",
            showConfirmButton: true,
            confirmButtonText: 'Tamam'
        });
        return;
    }

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
                fetch('/Customer/frmCreateReservation')
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok ' + response.statusText);
                        }
                        return response.text();
                    })
                    .then(html => {
                        document.getElementById('Body').innerHTML = html;

                        $('#myModal').modal('show');

                        $("#TourType").val(tourType);
                        $("#ReservationDay").val(reservationDay);
                        $("#ReservationHour").val(reservationHour);
                        $("#CustomerCount").val(CustomerCount);
                    })
                    .catch(error => {
                        console.error('Error loading modal content:', error);
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
            console.error("Hata oluştu:", error);
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Tarih kontrolü yapılırken bir hata oluştu.',
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            });
        }
    });
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
    $.ajax({
        url: '/Customer/CreateReservation',
        method: 'POST',
        data: formData,
        success: function (response) {
            Swal.fire({
                icon: 'success',
                title: 'Başarıyla Oluşturuldu!',
                text: 'Rezervasyonun başarıyla oluşturuldu.',
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            })
            $('#myModal').hide();
        },
        error: function (error) {
            console.log("error:" + error);
        }
    });
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
