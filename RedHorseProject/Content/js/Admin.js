$(document).ready(function () {

    createReservetionTable()
});

function updateCapacity() {
    var formData = $("#EditCapacityForm").serialize()
    $.ajax({
        url: '/Home/UpdateCapacity',
        method: 'POST',
        data: formData,
        success: function (response) {
            Swal.fire({
                icon: 'success',
                title: 'Başarıyla Güncellendi!',
                text: 'Kapasite başarıyla güncellendi.',
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            })
        },
        error: function (error) {
            console.log("error:" + error);
        }
    });
}

function openCustomerModal(id) {
    $.ajax({
        url: "/Home/getAgencyDetails/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {
            if (response ) {
                // Modal'ı açmadan önce HTML içeriğini yükle
                fetch('/Home/frmCustomerDetails')
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok ' + response.statusText);
                        }
                        return response.text();
                    })
                    .then(html => {
                        document.getElementById('Body').innerHTML = html;

                        // HTML içeriği yüklendikten sonra modal'ı aç
                        const modal = document.getElementById('myModal');
                        modal.style.display = 'block';
                        modal.classList.add('show');
                        document.body.classList.add('modal-open');

                        // Verileri inputlara yerleştir
                        $("#AgencyName").val(response.AgencyName);
                        $("#Mail").val(response.Mail);
                        $("#TursabNo").val(response.TursabNo);
                        $("#Phone").val(response.Phone);
                        $("#TaxNo").val(response.TaxNo);
                    })
                    .catch(error => console.error('Error loading modal content:', error));
            } else {
                console.error("Invalid response structure", response);
                alert("Veri alırken bir hata oluştu.");
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            alert("Bir hata oluştu.");
        }
    });
}


function createReservetionTable() {
    $('#example').DataTable({
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
                    columns: [0, 1, 2, 3, 4, 5,6,7] 
                }
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF Olarak Dışa Aktar',
                filename: 'Rezervasyonlar',
                title: 'Rezervasyonlar Listesi',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7] 
                }
            }
        ],
        columns: [
            { data: 'AgencyName' },
            { data: 'FirstName' },
            { data: 'LastName' },
            { data: 'Phone' },
            { data: 'HotelName' },
            { data: 'PassportNo' },
            { data: 'HotelRoomNo' },
            { data: 'CustomerCount' },
            {
                data: null,
                render: function (data, type, row) {
                    if (data.Status == 1) {
                        return `<button onclick="updateStatus(${row.Id}, false)" class="btn btn-danger">Deaktif Et</button>`;
                    }
                    else {
                        return `<button onclick="updateStatus(${row.Id}, true)" class="btn btn-primary">Aktif Et</button>`;
                    }
                }
            }
        ]
    });
}