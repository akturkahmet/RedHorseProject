$(document).ready(function () {
    function closeModal() {
        $('#myModal').hide();

    }
    $(function () {
        $("#myModal .modal-dialog").draggable({
            containment: false,
        });

        $("#myModal").on('show.bs.modal', function () {
            $(".modal-dialog").css("position", "absolute");
            $(".modal-dialog").css("top", "100px");
            $(".modal-dialog").css("left", "100px");
        });
    });
    createEditReservationTable()
});



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
                    fetch('/Customer/frmCreateReservation')
                        .then(response => {
                            if (!response.ok) {
                                throw new Error('Network response was not ok ' + response.statusText);
                            }
                            return response.text();
                        })
                        .then(html => {
                            document.getElementById('Body').innerHTML = html;
                            document.getElementById('modalTitle').textContent = 'Rezervasyon Oluştur';

                            const modal = document.getElementById('myModal');
                            modal.style.display = 'block';
                            modal.classList.add('show');
                            document.body.classList.add('modal-open');

                            $("#TourType").val(tourType);
                            $("#ReservationDay").val(reservationDay);
                            $("#ReservationHour").val(reservationHour);
                            $("#CustomerCount").val(CustomerCount);
                            $("#remainingCapacity").text(response.remainingCapacity);


                            var passportLabel = $("#PassportNo").prev("label");
                            var passportInput = $("#PassportNo");

                            if (tourType === "ballon") {
                                passportLabel.text("Pasaport Numarası*");
                            }
                            else {
                                passportInput.addClass("notNecessary");
                                passportLabel.text("Pasaport Numarası");

                            }
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
        pageLength: 24,
        lengthMenu: [[24, 50, 100, -1], [24, 50, 100, "Tümü"]],
        dom: '<"top"lBf>rt<"bottom"ip><"clear">',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel"></i> Excel', 
                filename: 'Rezervasyonlar',
                titleAttr: 'Excel Olarak İndir', 
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf"></i> PDF',
                filename: 'Rezervasyonlar',
                titleAttr: 'PDF Olarak İndir', 
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10]
                }
            }
        ],

        columns: [
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'Name' },
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
                    if (row.IsFutureReservation) {
                        return `<button onclick="openEditModal(${row.Id})" class="btn btn-primary" data-id="${row.Id}" id="activeButton">Düzenle</button>`;
                    } else {
                        return `Düzenleme yapılamaz.`;
                    }
                }
            }
        ]
    });
}




function openEditModal(id) {
    fetch(`/Customer/frmEditRezervation?id=${id}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('modalTitle').textContent = 'Düzenle';
            $('#modalTitle').text('Rezervasyon Oluştur');


            document.getElementById('Body').innerHTML = html;

            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');
            document.body.classList.add('modal-open');
            $("#ReservationId").val(id)
            getReservationDetails()




        })
        .catch(error => console.error('Error loading modal content:', error));
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
                Alert(4)
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






function ControlHours() {
    var Date1 = $("#reservationDay1").val();
    var TourTypeId = $("#TourType1").val();

    if (Date1 === "" || TourTypeId === "seçiniz") {
        Alert(2, "Hata", "Lütfen tur tipi ve tarih seçin.");
        return;
    }

    var formattedDate = new Date(Date1).toISOString().slice(0, 10);

    $.ajax({
        url: '/Customer/ControlHours',
        method: 'POST',
        data: {
            TourTypeId: TourTypeId,
            Date1: formattedDate
        },
        success: function (response) {
            $('#reservationHour1').empty();
            $('#reservationHour1').append(new Option('Seç', ''));

            response.forEach(function (hour) {
                if (!hour || !hour.Hour) {
                    console.error("Geçersiz saat verisi:", hour);
                    return;
                }

                var option = new Option(hour.Hour, hour.Hour);

                if (hour.Status == 0) {
                    option.disabled = true;
                    option.style.color = "red";
                    option.style.opacity = 0.5;
                }

                $('#reservationHour1').append(option);
            });
            $("#reservationHour1").prop("disabled", false);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Hatası:", status, error);
            Alert(1, "Hata", "Saatler getirilirken bir hata oluştu. Lütfen tekrar deneyin.");
            $("#reservationHour1").prop("disabled", true);
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

                $('#remainingCapacity1').text(response);
            },

            error: function (xhr) {

            }
        });
    }
    else {
        Alert(3, "Hata", "Lütfen Tur tipi,tarihi ve saati doldurunuz.")
    }

}










function clearAllSelected() {
    $("#TourType1").val("seçiniz");
    $("#reservationDay1").val("");
    $("#reservationHour1").empty().append(new Option('Seç', ''));
    $("#CustomerCount1").val("");

    $("#reservationDay1").prop("disabled", true);
    $("#reservationHour1").prop("disabled", true);
}