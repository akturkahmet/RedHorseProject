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
function openVerificationModal() {
    var email = $("#email").val();

    fetch('/Login/frmVerification')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            document.getElementById('modalTitle').textContent = 'Doğrulama Kodu';
            document.getElementById('Body').innerHTML = html;

            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');
            document.body.classList.add('modal-open');

            const emailInput = document.getElementById('email2');
            if (emailInput) {
                emailInput.value = email;
            }
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
    var newPassword = $("#newPassword").val();
    var confirmPassword = $("#confirmPassword").val();
    if (validateForm("changePasswordForm")) {
        verifyEmailAndPassword(email, newPassword, confirmPassword, function (isValid, message) {
            if (isValid) {
                $.ajax({
                    url: '/Login/SendVerificationCode',
                    method: 'POST',
                    data: {
                        userEmail: email,
                        newPassword: newPassword,
                        confirmPassword: confirmPassword,
                    },
                    success: function (response) {
                        if (response.success) {
                            Alert(6, response.message, "Başarılı", openVerificationModal);
                        } else {
                            Alert(2, "Hata", "Doğrulama kodu gönderilemedi.");
                        }
                    },
                    error: function (xhr) {
                        Alert(3, "Bir hata oluştu.", "Hata");
                    }
                });
            } else {
                Alert(2, "Hata", message);
            }
        });
    }
    else {
        Alert(3)
    }
}

function verifyEmailAndPassword(email, newPassword, confirmPassword, callback) {
    if (newPassword !== confirmPassword) {
        callback(false, "Şifreler uyuşmuyor.");
        return;
    }

    $.ajax({
        url: '/Login/verifyEmail',
        method: 'POST',
        data: { email: email },
        success: function (response) {
            if (response.success) {
                callback(true, "Başarılı.");
            } else {
                callback(false, "Sisteme kayıtlı böyle bir mail adresi bulunmamaktadır.");
            }
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
    if (userCode == null || userCode == "") {
        Alert(3)
    } else {
        $.ajax({
            url: '/Login/VerifyCode',
            method: 'POST',
            data: {
                userCode: userCode,
            },
            success: function (response) {
                if (response.success) {
                    Alert(7, "Başarılı", response.message)
                }
                else {
                    Alert(2, "Hata", response.message)
                }
            },

            error: function (xhr) {
                Alert(4)
            }
        });
    }

}