$('#showPassword').change(function () {
    var passwordField = $('.togglePassword');

    if ($(this).is(':checked')) {
        passwordField.attr('type', 'text');
    } else {
        passwordField.attr('type', 'password');
    }
});


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

