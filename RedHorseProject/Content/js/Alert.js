function Alert(number, title, text, thenFunction) {
    switch (number) {
        case 1:
            Swal.fire({
                icon: 'success',
                title: title,
                text: text,
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            });
            break;
        case 2:
            Swal.fire({
                icon: 'error',
                title: title,
                text: text,
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            });
            break;
        case 3:
            Swal.fire({
                icon: 'error',
                title: "Hata",
                text: "Lütfen tüm alanları doldurunuz.",
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            });
            break;
        case 4:
            Swal.fire({
                icon: 'error',
                title: "Hata",
                text: "Bir hata oluştu lütfen sonra deneyin.",
                showConfirmButton: true,
                confirmButtonText: 'Tamam'
            });
            break;
        case 6:
            Swal.fire({
                title: title,
                text: text,
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                if (result.isConfirmed) {
                    thenFunction();
                    $("#myModal").hide();
                }
            });
            break;
        default:
            console.warn("Geçersiz uyarı numarası.");
    }
}