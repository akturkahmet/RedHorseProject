$('#showPassword').change(function () {
    var passwordField = $('.togglePassword');

    if ($(this).is(':checked')) {
        passwordField.attr('type', 'text');
    } else {
        passwordField.attr('type', 'password');
    }
});
function closeModal() {
    $('#myModal').hide();
}


function register() {
    var formData = $("#createAgencyForm").serialize();
    var isEmptyField = false;

    // Formdaki tüm input elemanlarını kontrol et
    $("#createAgencyForm input").each(function () {
        if ($(this).val() === null || $(this).val().trim() === "") {
            isEmptyField = true;
            return false; // Döngüyü durdur
        }
    });

    if (isEmptyField) {
        Swal.fire({
            icon: 'error',
            title: 'Hata',
            text: "Lütfen tüm alanları doldurun.",
        });
        return; 
    }

    $.ajax({
        url: '/Login/CreateCustomer',
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı',
                    text: response.message,
                    confirmButtonText: 'Tamam',
                    customClass: {
                        confirmButton: 'btn btn-success'
                    },
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/Login/Index';
                    }
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata',
                    text: response.message,
                });
            }
        },
    });
}
function openForgotPasswordModal() {

    fetch('/Login/frmForgotPassword')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            document.getElementById('modalTitle').textContent = 'Şifre Yenile';

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
function changePassword() {
    var formData = $("#changePasswordForm").serialize();
    $.ajax({
        url: '/Login/ChangePassword',
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


function SendVerificationCode() {
    var email = $("#email").val();
    $.ajax({
        url: '/Login/SendVerificationCode',
        method: 'POST',
        data: {
            userEmail: email,
        },
        success: function (response) {
            if (response.success) {
                Alert(1, response.message, "Başarılı")
            } else {
              Alert(2)
            }
        },

        error: function (xhr) {
          Alert(3)
        }
    });
}


function openfrmEmailVerification() {

    fetch('/Login/frmEmailVerification')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            document.getElementById('modalTitle').textContent = 'Şifre Yenile';

            document.getElementById('Body').innerHTML = html;

            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');

            document.body.classList.add('modal-open');





        })
        .catch(error => console.error('Error loading modal content:', error));
}

function VerifyCode() {
    var userCode = $("#userCode").val();
    $.ajax({
        url: '/Login/VerifyCode',
        method: 'POST',
        data: {
            userCode: userCode,
        },
        success: function (response) {
            if (response.success) {
                Alert(1, response.message, "Başarılı")
            } else {
                Alert(2)
            }
        },

        error: function (xhr) {
            Alert(3)
        }
    });
}