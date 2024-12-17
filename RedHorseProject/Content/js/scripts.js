
function openRezervationModal() {

    fetch('/Home/frmRezervationDetails')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            document.getElementById('modalTitle').textContent = 'Rezervasyon Oluştur';

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






function closeModal() {
    $('#myModal').hide();
}




function toggleSettingsMenu() {
    const menu = document.getElementById('settingsMenu');
    if (menu.style.display === 'block') {
        menu.style.display = 'none'; // Menü açıkken kapat
    } else {
        menu.style.display = 'block'; // Menü kapalıyken aç
    }
}



function updateStatus(reservationId, newStatus) {
    $.ajax({
        url: '/Customer/updateStatus',
        method: 'POST',
        data: {
            id: reservationId,
            status: newStatus
        },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: response.message,
                    confirmButtonText: 'Tamam'
                }).then(() => {
                    $('#example').DataTable().ajax.reload();
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: response.message,
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            console.error('Güncelleme hatası:', error);
            Swal.fire({
                icon: 'error',
                title: 'Sunucu Hatası',
                text: 'Bir hata oluştu, lütfen tekrar deneyin.',
                confirmButtonText: 'Tamam'
            });
        }
    });
}
var editReservationTable;




function getReservationDetails() {
    var id = $("#ReservationId").val()
    $.ajax({
        url: '/Customer/GetReservationDetails',
        method: 'GET',
        data: {
            id: id
        },
        success: function (response) {
            $("#FirstName").val(response.FirstName)
            $("#LastName").val(response.LastName)
            $("#Phone").val(response.Phone)
            $("#HotelName").val(response.HotelName)
            $("#PassportNo").val(response.PassportNo)
            $("#CustomerCount").val(response.CustomerCount)
            $("#HotelRoomNo").val(response.HotelRoomNo)
            $("#LastName").val(response.LastName)
        },
        error: function (error) {
            console.log("error:" + error);
        }
    });
}


function refreshTable() {
    if (editReservationTable) {
        editReservationTable.ajax.reload();
    }
}


    
