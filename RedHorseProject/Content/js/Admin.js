$(document).ready(function () {
    createAgenciesTable()
    createunAprrovedAgenciesTable()
    createReservetionTable()
    function closeModal() {
        $('#myModal').hide();

    }

});
function addNewAgency() {
    var formData = $("#createAgencyForm").serialize()

    if (validateForm("createAgencyForm")) {
        $.ajax({
            url: '/Home/createNewAgency',
            method: 'POST',
            data: formData,
            success: function (response) {
                Alert(1, "Başarıyla Eklendi!", response.message)
            },
            error: function (error) {
                Alert(4, error)
            }
        });
    }
    else {
        Alert(1)
    }
}
function updateCapacity() {
    var formData = $("#EditCapacityForm").serialize()
    $.ajax({
        url: '/Home/UpdateCapacity',
        method: 'POST',
        data: formData,
        success: function (response) {
            Alert(1, "Başarıyla Güncellendi!", "Kapasite başarıyla güncellendi.")
            $("#myModal").hide()
            $('#hoursCapacityTable').DataTable().ajax.reload(null, false);

        },
        error: function (error) {
            Alert(4, error)
        }
    });
}

function createAgenciesTable() {
    $('#AgenciesTable').DataTable({
        ajax: {
            url: '/Home/getAgencies',
            dataSrc: 'Data'
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Excel Olarak Dışa Aktar',
                filename: 'Ajanslar',
                title: 'Ajans Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF Olarak Dışa Aktar',
                filename: 'Ajanslar',
                title: 'Ajans Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            }
        ],
        columns: [
            { data: 'AgencyName' },
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'CreatedDate' },
            { data: 'Phone' },
            { data: 'Mail' },
            { data: 'TaxNo' },
            { data: 'TursabNo' },
            { data: 'Tc' },
            { data: 'Region' },
            {
                data: null,
                render: function (data, type, row) {
                    if (data.Status == 1) {
                        return `<button onclick="UpdateCustomerStatus(${row.Id}, false)" id="deactivebutton" class="btn btn-danger">Deaktif Et</button>`;
                    }
                    else {
                        return `<button onclick="UpdateCustomerStatus(${row.Id})" class="btn btn-primary">Aktif Et</button>`;
                    }
                }
            }
        ],
        initComplete: function () {
            // Tablodaki tüm hücrelerde padding'i sıfırlama ve metni ortalama
            $('#AgenciesTable tbody th, #AgenciesTable tbody td').css({
                'padding': '0',
                'text-align': 'center',   // Yatayda ortalama
                'vertical-align': 'middle' // Dikeyde ortalama
            });
            $('#AgenciesTable .btn').css({
                'font-size': '12px', // Buton yazı boyutunu küçültme
                'padding': '5px 10px', // Buton içindeki padding'i küçültme
                'width': 'auto'  ,// Buton genişliğini otomatik yapmak
                'height':'33px'
            });
        }
    });
}


function createunAprrovedAgenciesTable() {
    $('#unAprrovedAgencies').DataTable({
        ajax: {
            url: '/Home/GetUnaprrovedAgencies',
            dataSrc: 'Data'
        },
        //dom: 'Bfrtip',
        columns: [
            { data: 'AgencyName' },
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'CreatedDate' },
            { data: 'Phone' },
            { data: 'Mail' },
            { data: 'TaxNo' },
            { data: 'TursabNo' },
            { data: 'Tc' },
            { data: 'Region' },
            {
                data: null,
                render: function (data, type, row) {
                    return `<a onclick="confirmCustomer(${row.Id})" class="btn btn-primary">Onayla</a>`;
                }
            }  ,
            {
                data: null,
                render: function (data, type, row) {
                    return `<a onclick="cancelCustomer(${row.Id})" class="btn btn-primary">Reddet</a>`;
                }
            }
        ]
    });
}
function openAppealModal() {

    fetch('/Home/frmAppealDetails')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            document.getElementById('modalTitle').textContent = 'Müşteriler';

            // İçeriği modal-body123'e yükle
            document.getElementById('Body').innerHTML = html;

            // Modal'ı göster
            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');

            // Arka plan için fade sınıfı
            document.body.classList.add('modal-open');
        })
        .catch(error => console.error('Error loading modal content:', error));
}
function confirmCustomer(id) {
    $.ajax({
        url: "/Home/ConfirmCustomer/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {

            Swal.fire({
                title: 'Başarılı',
                text: 'Müşteri onaylandı.',
                icon: 'success',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            });
        },
        error: function (xhr, status, error) {
            Alert(3)
        }
    });
}
function cancelCustomer(id) {
    $.ajax({
        url: "/Home/CancelCustomer/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {

            Swal.fire({
                title: 'Başarılı',
                text: 'Müşteri reddedildi.',
                icon: 'alert',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            });


        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            alert("Bir hata oluştu.");
        }
    });
}
function UpdateCustomerStatus(id) {
    $.ajax({
        url: "/Home/UpdateCustomerStatus/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: 'Başarılı',
                    text: response.message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then((result) => {
                    if (result.isConfirmed) {
                        location.reload();
                    }
                });
            }
        },
        error: function (xhr, status, error) {
            Alert(4, error)
        }
    });
}
function createReservetionTable() {

    var TourTypeId = $("#TourTypeId").val();
    var Hour = $("#Hour").val();

    const tableElement = $('#example');
    if ($.fn.DataTable.isDataTable(tableElement)) {
        tableElement.DataTable().clear().destroy();
    }

    tableElement.DataTable({
        ajax: {
            url: '/Home/GetRezervation',
            dataSrc: '',
            type: 'POST',
            data: function (d) {
                return $.extend({}, d, {
                    TourTypeId: TourTypeId,
                    Hour: Hour
                });
            },
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Excel Olarak Dışa Aktar',
                filename: 'Rezervasyonlar',
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF Olarak Dışa Aktar',
                filename: 'Rezervasyonlar',
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            }
        ],
        columns: [
            { data: 'AgencyName' },
            { data: 'Name' },
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'Phone' },
            { data: 'HotelName' },
            { data: 'PassportNo' },
            { data: 'HotelRoomNo' },
            { data: 'CustomerCount' },
            { data: 'ReservationDate' },
            {
                data: null,
                render: function (data, type, row) {
                    if (data.Status == 1) {
                        return `<button onclick="updateStatus(${row.Id}, false)" class="btn btn-danger" id="activeButton">Deaktif Et</button>`;
                    } else {
                        return `<button onclick="updateStatus(${row.Id}, true)" class="btn btn-primary" id="activeButton" >Aktif Et</button>`;
                    }
                }
            }
        ]
    });
}




function getHours() {
    var tourType = $("#tourType").val();
    var url = '/Home/getHours?tourType=' + encodeURIComponent(tourType);
    if (tourType === "Select") {
        Alert(2, "Hata", "Lütfen geçerli bir tur kategorisi seçin.");
        return;
    }
    else {



        if ($.fn.DataTable.isDataTable('#hoursTable')) {
            $('#hoursTable').DataTable().clear().destroy();
        }

        $('#hoursTable').DataTable({
            ajax: {
                url: url,
                dataSrc: ''
            },
            /*     dom: 'Bfrtip', */
            columns: [
                { data: 'Name' },
                { data: 'Hour' },
                {
                    data: null,
                    render: function (data, type, row) {
                        if (data.Status == 1) {
                            return `<button onclick="updateStatusHour(${row.Id}, false)" class="btn btn-danger">Deaktif Et</button>`;
                        }
                        else {
                            return `<button onclick="updateStatusHour(${row.Id}, true)" class="btn btn-primary">Aktif Et</button>`;
                        }
                    }
                }
            ]
        });
    }
}
function updateStatusHour(id) {
    $.ajax({
        url: '/Home/updateStatusHour',
        method: 'POST',
        data: { id: id },
        success: function (response) {
            Alert(1, response.message, "Başarıyla Güncellendi!")
            $('#hoursTable').DataTable().ajax.reload(null, false);

        },
        error: function () {
            Alert(4)
        }
    });
}


function getHoursCapacity() {
    var tourType = $("#tourTypeId").val();

    var url = '/Home/getHours?tourType=' + encodeURIComponent(tourType);
    if (tourType == "Select") {
        Alert(2, "Hata", "Lütfen geçerli bir tur kategorisi seçin.");
        return;
    }
    else {
        if ($.fn.DataTable.isDataTable('#hoursCapacityTable')) {
            $('#hoursCapacityTable').DataTable().clear().destroy();
        }

        $('#hoursCapacityTable').DataTable({
            ajax: {
                url: url,
                dataSrc: ''
            },
            columns: [
                { data: 'Name' },
                { data: 'Hour' },
                { data: 'Capacity' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return `<button type="button" onclick="openEditCapacityModal('${row.TourTypeId}', '${row.Hour}')" class="btn btn-primary">Düzenle</button>`;

                    }
                }
            ]
        });
    }
}
function openEditCapacityModal(TourTypeId, Hour) {

    fetch('/Home/frmEditCapacityHour')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            document.getElementById('Body').innerHTML = html;
            document.getElementById('modalTitle').textContent = 'Kapasite Düzenle';

            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');
            document.body.classList.add('modal-open');

            $("#TourTypeId").val(TourTypeId);
            $("#Hour").val(Hour);
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




}


function changePassword() {
    var formData = $("#changePasswordForm").serialize();
    $.ajax({
        url: '/Home/ChangePassword',
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