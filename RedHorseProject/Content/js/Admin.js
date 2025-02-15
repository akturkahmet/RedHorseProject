
var formattedStartDate, formattedEndDate
$(document).ready(function () {
    createAgenciesTable()
    createunAprrovedAgenciesTable()
    createReservetionTable()
    CreateSpecificDateCapacityTable();

    $('#calendar').dxCalendar({
        selectionMode: 'range',
        onValueChanged: function (e) {
            const selectedRange = e.value;

            if (selectedRange && selectedRange.length === 2) {
                const startDate = selectedRange[0];
                const endDate = selectedRange[1];

                formattedStartDate = startDate.toISOString().split('T')[0];
                formattedEndDate = endDate.toISOString().split('T')[0];


            }
        }
    }).dxCalendar('instance');

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
    Destroy("AgenciesTable")
    $('#AgenciesTable').DataTable({
        ajax: {
            url: '/Home/getAgencies',
            dataSrc: 'Data'
        },
        pageLength: 24,
        lengthMenu: [[24, 50, 100, -1], [24, 50, 100, "Tümü"]],
        dom: '<"top"lBf>rt<"bottom"ip><"clear">',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel"></i> Excel',
                filename: 'Acenta Listesi',
                titleAttr: 'Excel Olarak İndir',
                title: 'Acenta Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf"></i> PDF',
                filename: 'Acenta Listesi',
                titleAttr: 'PDF Olarak İndir',
                title: 'Acenta Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10]
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
                        return `<button onclick="UpdateCustomerStatus(${row.Id}, false)" id="activeButton" class="btn btn-danger">Deaktif Et</button>`;
                    }
                    else {
                        return `<button onclick="UpdateCustomerStatus(${row.Id})" id="activeButton" class="btn btn-primary">Aktif Et</button>`;
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="openFrmAgencyDetails(${row.Id})" id="activeButton" class="btn btn-primary">Düzenle</button>`;
                }
            }
        ],
        initComplete: function () {
            $('#AgenciesTable').closest('.dataTables_wrapper').css('margin', '6px');

        }

    });
}

function openFrmAgencyDetails(Id) {
    $.ajax({
        url: "/Home/GetAgencyById/",
        method: "GET",
        data: {
            AgencyId: Id
        },
        success: function (response) {

            fetch('/Home/frmAgencyDetails')
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok ' + response.statusText);
                    }
                    return response.text();
                })
                .then(html => {
                    document.getElementById('modalTitle').textContent = 'Ajans Detayları';
                    document.getElementById('Body').innerHTML = html;
                    const modal = document.getElementById('myModal');
                    modal.style.display = 'block';
                    modal.classList.add('show');
                    document.body.classList.add('modal-open');



                    $("#FirstName").val(response.FirstName);
                    $("#Id").val(response.Id);
                    $("#LastName").val(response.LastName);
                    $("#AgencyName").val(response.AgencyName);
                    $("#Phone").val(response.Phone);
                    $("#Mail").val(response.Mail);
                    $("#TursabNo").val(response.TursabNo);
                    $("#Tc").val(response.Tc);
                    $("#TaxNo").val(response.TaxNo);
                })
                .catch(error => console.error('Error loading modal content:', error));
        },
        error: function (xhr, status, error) {
            Alert(4)
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
            },
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
    var ReservationDate = $("#ReservationDate").val();

    Destroy("example")

    $('#example').DataTable({
        ajax: {
            url: '/Home/GetRezervation',
            dataSrc: "",
            type: 'POST',
            data: function (d) {
                return $.extend({}, d, {
                    TourTypeId: TourTypeId,
                    Hour: Hour,
                    ReservationDate: ReservationDate,
                });
            },
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
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf"></i> PDF',
                filename: 'Rezervasyonlar',
                titleAttr: 'PDF Olarak İndir',
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            }
        ],
        columns: [
            { data: 'AgencyName' },
            { data: 'TourTypeName' },
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'Phone' },
            { data: 'HotelName' },
            { data: 'PassportNo' },
            { data: 'HotelRoomNo' },
            { data: 'CustomerCount' },
            { data: 'ReservationDate' },
            { data: 'TourNote' },
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
        ],
        pageLength: 25,
        lengthMenu: [[25, -1], [24, "Tümü"]],
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

        Destroy("hoursTable")
        $('#hoursTable').DataTable({
            ajax: {
                url: url,
                dataSrc: ''
            },
            /*     dom: 'Bfrtip', */
            pageLength: 24,
            lengthMenu: [[24, 50, 100, -1], [24, 50, 100, "Tümü"]],
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
            }, pageLength: 24,
            lengthMenu: [[24, -1], [24, "Tümü"]],
            columns: [
                { data: 'Name' },
                { data: 'Hour' },
                { data: 'Capacity' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return `<button type="button" onclick="
                        openEditCapacityModal('${row.TourTypeId}', '${row.Hour}')" class="btn btn-primary">Düzenle</button>`;

                    }
                }
            ]
        });
    }
}
function openEditCapacityModal(TourTypeId, Hour, Day) {
    fetch('/Home/frmEditCapacityHour')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            // Modal içeriğini yükle
            document.getElementById('Body').innerHTML = html;
            document.getElementById('modalTitle').textContent = 'Kapasite Düzenle';

            // Modal'ı aç
            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');
            document.body.classList.add('modal-open');
            var currentPath = window.location.pathname;

            if (currentPath.includes("SpecificEditCapacity")) {
                $("#updateButton").attr("onclick", "UpdateSpecificDateCapacity()");
            } else {
                $("#updateButton").attr("onclick", "updateCapacity()");
            }
            // Değerleri inputlara atama
            $("#TourTypeId").val(TourTypeId);
            $("#Hour").val(Hour);
            $("#Day").val(Day);
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

function CreateSpecificDateCapacity() {


    var TourType = $("#TourTypeId").val();
    var Capacity = $("#Capacity").val();
    const selectedHours = [];
    $('#checkbox-container input[type="checkbox"]:checked').each(function () {
        selectedHours.push($(this).attr('id'));
    });
    if (TourType == "Select" || Capacity == null || formattedStartDate == null || selectedHours.length === 0) {
        Alert(3);
    }
    else {
        $.ajax({
            url: '/Home/CreateSpecificDateCapacity',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                TourTypeId: TourType,
                Capacity: parseInt(Capacity, 10),
                startDate: formattedStartDate,
                endDate: formattedEndDate,
                selectedHours: selectedHours
            }),
            success: function (response) {
                if (response.success) {
                    Alert(1, response.message, "Başarılı");
                } else {
                    Alert(2, response.message, "Hata");
                }
            },
            error: function (xhr) {
                Alert(4);
            }
        });
    }

}

function CreateSpecificDateCapacityTable() {
    var tourType = $("#tourType").val();
    var url = '/Home/GetSpecificDateCapacity?tourType=' + encodeURIComponent(tourType);
    if (tourType === "Select") {
        Alert(2, "Hata", "Lütfen geçerli bir tur kategorisi seçin.");
        return;
    }
    else {

        Destroy("specificHoursTable")
        $('#specificHoursTable').DataTable({
            ajax: {
                url: url,
                dataSrc: ''
            },
            pageLength: 24,
            lengthMenu: [[24,50,100, -1], [24,50,100, "Tümü"]],
            columns: [
                { data: 'Name' },
                { data: 'Date' },
                { data: 'Capacity' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return `<button onclick="openEditCapacityModal('${String(row.TourTypeId)}', '${String(row.Hour)}', '${String(row.Day)}')" class="btn btn-primary">Düzenle</button>`;
                    }


                }
            ]
        });
    }
}

function UpdateSpecificDateCapacity() {
    var formData = $("#EditCapacityForm").serialize()
    $.ajax({
        url: '/Home/UpdateSpecificDateCapacity',
        method: 'POST',
        data: formData,
        success: function (response) {
            Alert(1, "Başarıyla Güncellendi!", "Kapasite başarıyla güncellendi.")
            $("#myModal").hide()
            $('#hoursTable').DataTable().ajax.reload(null, false);

        },
        error: function (error) {
            Alert(4, error)
        }
    });
}

function UpdateAgencyInformation() {
    var formData = $("#updateAgencyForm").serialize()
    $.ajax({
        url: '/Home/UpdateAgencyInformation',
        method: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                Alert(6, "Başarılı", response.message, createAgenciesTable);

            } else {
                Alert(2, response.message, "Hata")

            }
        },

        error: function (xhr) {
            Alert(4)
        }
    });
}

function CreateSpecificDateStatusTable() {
    var tourType = $("#tourType").val();
    var url = '/Home/GetSpecificDateCapacity?tourType=' + encodeURIComponent(tourType);
    if (tourType === "Select") {
        Alert(2, "Hata", "Lütfen geçerli bir tur kategorisi seçin.");
        return;
    }
    else {

        Destroy("specificClockStatusTable")
        $('#specificClockStatusTable').DataTable({
            ajax: {
                url: url,
                dataSrc: ''
            },
            pageLength: 24,
            lengthMenu: [[24, -1], [24, "Tümü"]],
            columns: [
                { data: 'Name' },
                { data: 'Date' },
                { data: 'Capacity' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return `<button onclick="UpdateClockStatus(${row.Id})" class="btn ${row.Status == 1 ? 'btn-danger' : 'btn-primary'}">${row.Status == 1 ? 'Deaktif Et' : 'Aktif Et'}</button>`;
                    }


                }
            ]
        });
    }
}
function UpdateClockStatus(Id) {
    $.ajax({
        url: '/Home/UpdateClockStatus',
        method: 'POST',
        data: {
            Id: Id
        },
        success: function (response) {
            if (response.success) {
                Alert(6, "Başarılı", response.message, CreateSpecificDateStatusTable);

            } else {
                Alert(2, response.message, "Hata")

            }
        },

        error: function (xhr) {
            Alert(4)
        }
    });
}

function SelectAll() {
    $('#checkbox-container input[type="checkbox"]').prop('checked', true);
};

function DeSelectAll() {
    $('#checkbox-container input[type="checkbox"]').prop('checked', false);
};