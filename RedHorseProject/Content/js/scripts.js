"use strict";

// ChartJS
if (window.Chart) {
    Chart.defaults.global.defaultFontFamily = "'Nunito', 'Segoe UI', 'Arial'";
    Chart.defaults.global.defaultFontSize = 11;
    Chart.defaults.global.defaultFontStyle = 500;
    Chart.defaults.global.defaultFontColor = "#999";
    Chart.defaults.global.tooltips.backgroundColor = '#000';
    Chart.defaults.global.tooltips.titleFontFamily = "'Nunito', 'Segoe UI', 'Arial'";
    Chart.defaults.global.tooltips.titleFontColor = '#fff';
    Chart.defaults.global.tooltips.titleFontSize = 20;
    Chart.defaults.global.tooltips.xPadding = 10;
    Chart.defaults.global.tooltips.yPadding = 10;
    Chart.defaults.global.tooltips.cornerRadius = 3;
}

// DropzoneJS
if (window.Dropzone) {
    Dropzone.autoDiscover = false;
}
$(document).ready(function () {
    // Modal'ı draggable yap
    $('#myModal').on('shown.bs.modal', function () {
        $('.modal-dialog').draggable({
            handle: ".modal-header" // Modal başlığı sürükleme alanı
        });
    });
});

// Basic confirm box
$('[data-confirm]').each(function () {
    var me = $(this),
        me_data = me.data('confirm');

    me_data = me_data.split("|");
    me.fireModal({
        title: me_data[0],
        body: me_data[1],
        buttons: [
            {
                text: me.data('confirm-text-yes') || 'Yes',
                class: 'btn btn-danger btn-shadow',
                handler: function () {
                    eval(me.data('confirm-yes'));
                }
            },
            {
                text: me.data('confirm-text-cancel') || 'Cancel',
                class: 'btn btn-secondary',
                handler: function (modal) {
                    $.destroyModal(modal);
                    eval(me.data('confirm-no'));
                }
            }
        ]
    })
});

// Global
$(function () {
    let sidebar_nicescroll_opts = {
        cursoropacitymin: 0,
        cursoropacitymax: .8,
        zindex: 892
    }, now_layout_class = null;

    var sidebar_sticky = function () {
        if ($("body").hasClass('layout-2')) {
            $("body.layout-2 #sidebar-wrapper").stick_in_parent({
                parent: $('body')
            });
            $("body.layout-2 #sidebar-wrapper").stick_in_parent({ recalc_every: 1 });
        }
    }
    sidebar_sticky();

    var sidebar_nicescroll;
    var update_sidebar_nicescroll = function () {
        let a = setInterval(function () {
            if (sidebar_nicescroll != null)
                sidebar_nicescroll.resize();
        }, 10);

        setTimeout(function () {
            clearInterval(a);
        }, 600);
    }

    var sidebar_dropdown = function () {
        if ($(".main-sidebar").length) {
            $(".main-sidebar").niceScroll(sidebar_nicescroll_opts);
            sidebar_nicescroll = $(".main-sidebar").getNiceScroll();

            $(".main-sidebar .sidebar-menu li a.has-dropdown").off('click').on('click', function () {
                var me = $(this);

                me.parent().find('> .dropdown-menu').slideToggle(500, function () {
                    update_sidebar_nicescroll();
                    return false;
                });
                return false;
            });
        }
    }
    sidebar_dropdown();

    if ($("#top-5-scroll").length) {
        $("#top-5-scroll").css({
            height: 315
        }).niceScroll();
    }

    $(".main-content").css({
        minHeight: $(window).outerHeight() - 95
    })

    $(".nav-collapse-toggle").click(function () {
        $(this).parent().find('.navbar-nav').toggleClass('show');
        return false;
    });

    $(document).on('click', function (e) {
        $(".nav-collapse .navbar-nav").removeClass('show');
    });

    var toggle_sidebar_mini = function (mini) {
        let body = $('body');

        if (!mini) {
            body.removeClass('sidebar-mini');
            $(".main-sidebar").css({
                overflow: 'hidden'
            });
            setTimeout(function () {
                $(".main-sidebar").niceScroll(sidebar_nicescroll_opts);
                sidebar_nicescroll = $(".main-sidebar").getNiceScroll();
            }, 500);
            $(".main-sidebar .sidebar-menu > li > ul .dropdown-title").remove();
            $(".main-sidebar .sidebar-menu > li > a").removeAttr('data-toggle');
            $(".main-sidebar .sidebar-menu > li > a").removeAttr('data-original-title');
            $(".main-sidebar .sidebar-menu > li > a").removeAttr('title');
        } else {
            body.addClass('sidebar-mini');
            body.removeClass('sidebar-show');
            sidebar_nicescroll.remove();
            sidebar_nicescroll = null;
            $(".main-sidebar .sidebar-menu > li").each(function () {
                let me = $(this);

                if (me.find('> .dropdown-menu').length) {
                    me.find('> .dropdown-menu').hide();
                    me.find('> .dropdown-menu').prepend('<li class="dropdown-title pt-3">' + me.find('> a').text() + '</li>');
                } else {
                    me.find('> a').attr('data-toggle', 'tooltip');
                    me.find('> a').attr('data-original-title', me.find('> a').text());
                    $("[data-toggle='tooltip']").tooltip({
                        placement: 'right'
                    });
                }
            });
        }
    }

    $("[data-toggle='sidebar']").click(function () {
        var body = $("body"),
            w = $(window);

        if (w.outerWidth() <= 1024) {
            body.removeClass('search-show search-gone');
            if (body.hasClass('sidebar-gone')) {
                body.removeClass('sidebar-gone');
                body.addClass('sidebar-show');
            } else {
                body.addClass('sidebar-gone');
                body.removeClass('sidebar-show');
            }

            update_sidebar_nicescroll();
        } else {
            body.removeClass('search-show search-gone');
            if (body.hasClass('sidebar-mini')) {
                toggle_sidebar_mini(false);
            } else {
                toggle_sidebar_mini(true);
            }
        }

        return false;
    });

    var toggleLayout = function () {
        var w = $(window),
            layout_class = $('body').attr('class') || '',
            layout_classes = (layout_class.trim().length > 0 ? layout_class.split(' ') : '');

        if (layout_classes.length > 0) {
            layout_classes.forEach(function (item) {
                if (item.indexOf('layout-') != -1) {
                    now_layout_class = item;
                }
            });
        }

        if (w.outerWidth() <= 1024) {
            if ($('body').hasClass('sidebar-mini')) {
                toggle_sidebar_mini(false);
                $('.main-sidebar').niceScroll(sidebar_nicescroll_opts);
                sidebar_nicescroll = $(".main-sidebar").getNiceScroll();
            }

            $("body").addClass("sidebar-gone");
            $("body").removeClass("layout-2 layout-3 sidebar-mini sidebar-show");
            $("body").off('click').on('click', function (e) {
                if ($(e.target).hasClass('sidebar-show') || $(e.target).hasClass('search-show')) {
                    $("body").removeClass("sidebar-show");
                    $("body").addClass("sidebar-gone");
                    $("body").removeClass("search-show");

                    update_sidebar_nicescroll();
                }
            });

            update_sidebar_nicescroll();

            if (now_layout_class == 'layout-3') {
                let nav_second_classes = $(".navbar-secondary").attr('class'),
                    nav_second = $(".navbar-secondary");

                nav_second.attr('data-nav-classes', nav_second_classes);
                nav_second.removeAttr('class');
                nav_second.addClass('main-sidebar');

                let main_sidebar = $(".main-sidebar");
                main_sidebar.find('.container').addClass('sidebar-wrapper').removeClass('container');
                main_sidebar.find('.navbar-nav').addClass('sidebar-menu').removeClass('navbar-nav');
                main_sidebar.find('.sidebar-menu .nav-item.dropdown.show a').click();
                main_sidebar.find('.sidebar-brand').remove();
                main_sidebar.find('.sidebar-menu').before($('<div>', {
                    class: 'sidebar-brand'
                }).append(
                    $('<a>', {
                        href: $('.navbar-brand').attr('href'),
                    }).html($('.navbar-brand').html())
                ));
                setTimeout(function () {
                    sidebar_nicescroll = main_sidebar.niceScroll(sidebar_nicescroll_opts);
                    sidebar_nicescroll = main_sidebar.getNiceScroll();
                }, 700);

                sidebar_dropdown();
                $(".main-wrapper").removeClass("container");
            }
        } else {
            $("body").removeClass("sidebar-gone sidebar-show");
            if (now_layout_class)
                $("body").addClass(now_layout_class);

            let nav_second_classes = $(".main-sidebar").attr('data-nav-classes'),
                nav_second = $(".main-sidebar");

            if (now_layout_class == 'layout-3' && nav_second.hasClass('main-sidebar')) {
                nav_second.find(".sidebar-menu li a.has-dropdown").off('click');
                nav_second.find('.sidebar-brand').remove();
                nav_second.removeAttr('class');
                nav_second.addClass(nav_second_classes);

                let main_sidebar = $(".navbar-secondary");
                main_sidebar.find('.sidebar-wrapper').addClass('container').removeClass('sidebar-wrapper');
                main_sidebar.find('.sidebar-menu').addClass('navbar-nav').removeClass('sidebar-menu');
                main_sidebar.find('.dropdown-menu').hide();
                main_sidebar.removeAttr('style');
                main_sidebar.removeAttr('tabindex');
                main_sidebar.removeAttr('data-nav-classes');
                $(".main-wrapper").addClass("container");
                // if(sidebar_nicescroll != null)
                //   sidebar_nicescroll.remove();
            } else if (now_layout_class == 'layout-2') {
                $("body").addClass("layout-2");
            } else {
                update_sidebar_nicescroll();
            }
        }
    }
    toggleLayout();
    $(window).resize(toggleLayout);

    $("[data-toggle='search']").click(function () {
        var body = $("body");

        if (body.hasClass('search-gone')) {
            body.addClass('search-gone');
            body.removeClass('search-show');
        } else {
            body.removeClass('search-gone');
            body.addClass('search-show');
        }
    });

    // tooltip
    $("[data-toggle='tooltip']").tooltip();

    // popover
    $('[data-toggle="popover"]').popover({
        container: 'body'
    });

    // Select2
    if (jQuery().select2) {
        $(".select2").select2();
    }

    // Selectric
    if (jQuery().selectric) {
        $(".selectric").selectric({
            disableOnMobile: false,
            nativeOnMobile: false
        });
    }

    $(".notification-toggle").dropdown();
    $(".notification-toggle").parent().on('shown.bs.dropdown', function () {
        $(".dropdown-list-icons").niceScroll({
            cursoropacitymin: .3,
            cursoropacitymax: .8,
            cursorwidth: 7
        });
    });

    $(".message-toggle").dropdown();
    $(".message-toggle").parent().on('shown.bs.dropdown', function () {
        $(".dropdown-list-message").niceScroll({
            cursoropacitymin: .3,
            cursoropacitymax: .8,
            cursorwidth: 7
        });
    });

    if ($(".chat-content").length) {
        $(".chat-content").niceScroll({
            cursoropacitymin: .3,
            cursoropacitymax: .8,
        });
        $('.chat-content').getNiceScroll(0).doScrollTop($('.chat-content').height());
    }

    if (jQuery().summernote) {
        $(".summernote").summernote({
            dialogsInBody: true,
            minHeight: 250,
        });
        $(".summernote-simple").summernote({
            dialogsInBody: true,
            minHeight: 150,
            toolbar: [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough']],
                ['para', ['paragraph']]
            ]
        });
    }

    if (window.CodeMirror) {
        $(".codeeditor").each(function () {
            let editor = CodeMirror.fromTextArea(this, {
                lineNumbers: true,
                theme: "duotone-dark",
                mode: 'javascript',
                height: 200
            });
            editor.setSize("100%", 200);
        });
    }

    // Follow function
    $('.follow-btn, .following-btn').each(function () {
        var me = $(this),
            follow_text = 'Follow',
            unfollow_text = 'Following';

        me.click(function () {
            if (me.hasClass('following-btn')) {
                me.removeClass('btn-danger');
                me.removeClass('following-btn');
                me.addClass('btn-primary');
                me.html(follow_text);

                eval(me.data('unfollow-action'));
            } else {
                me.removeClass('btn-primary');
                me.addClass('btn-danger');
                me.addClass('following-btn');
                me.html(unfollow_text);

                eval(me.data('follow-action'));
            }
            return false;
        });
    });

    // Dismiss function
    $("[data-dismiss]").each(function () {
        var me = $(this),
            target = me.data('dismiss');

        me.click(function () {
            $(target).fadeOut(function () {
                $(target).remove();
            });
            return false;
        });
    });

    // Collapsable
    $("[data-collapse]").each(function () {
        var me = $(this),
            target = me.data('collapse');

        me.click(function () {
            $(target).collapse('toggle');
            $(target).on('shown.bs.collapse', function () {
                me.html('<i class="fas fa-minus"></i>');
            });
            $(target).on('hidden.bs.collapse', function () {
                me.html('<i class="fas fa-plus"></i>');
            });
            return false;
        });
    });

    // Gallery
    $(".gallery .gallery-item").each(function () {
        var me = $(this);

        me.attr('href', me.data('image'));
        me.attr('title', me.data('title'));
        if (me.parent().hasClass('gallery-fw')) {
            me.css({
                height: me.parent().data('item-height'),
            });
            me.find('div').css({
                lineHeight: me.parent().data('item-height') + 'px'
            });
        }
        me.css({
            backgroundImage: 'url("' + me.data('image') + '")'
        });
    });
    if (jQuery().Chocolat) {
        $(".gallery").Chocolat({
            className: 'gallery',
            imageSelector: '.gallery-item',
        });
    }

    // Background
    $("[data-background]").each(function () {
        var me = $(this);
        me.css({
            backgroundImage: 'url(' + me.data('background') + ')'
        });
    });

    // Custom Tab
    $("[data-tab]").each(function () {
        var me = $(this);

        me.click(function () {
            if (!me.hasClass('active')) {
                var tab_group = $('[data-tab-group="' + me.data('tab') + '"]'),
                    tab_group_active = $('[data-tab-group="' + me.data('tab') + '"].active'),
                    target = $(me.attr('href')),
                    links = $('[data-tab="' + me.data('tab') + '"]');

                links.removeClass('active');
                me.addClass('active');
                target.addClass('active');
                tab_group_active.removeClass('active');
            }
            return false;
        });
    });

    // Bootstrap 4 Validation


    // alert dismissible
    $(".alert-dismissible").each(function () {
        var me = $(this);

        me.find('.close').click(function () {
            me.alert('close');
        });
    });

    if ($('.main-navbar').length) {
    }

    // Image cropper
    $('[data-crop-image]').each(function (e) {
        $(this).css({
            overflow: 'hidden',
            position: 'relative',
            height: $(this).data('crop-image')
        });
    });

    // Slide Toggle
    $('[data-toggle-slide]').click(function () {
        let target = $(this).data('toggle-slide');

        $(target).slideToggle();
        return false;
    });

    // Dismiss modal
    $("[data-dismiss=modal]").click(function () {
        $(this).closest('.modal').modal('hide');

        return false;
    });

    // Width attribute
    $('[data-width]').each(function () {
        $(this).css({
            width: $(this).data('width')
        });
    });

    // Height attribute
    $('[data-height]').each(function () {
        $(this).css({
            height: $(this).data('height')
        });
    });

    // Chocolat
    if ($('.chocolat-parent').length && jQuery().Chocolat) {
        $('.chocolat-parent').Chocolat();
    }

    // Sortable card
    if ($('.sortable-card').length && jQuery().sortable) {
        $('.sortable-card').sortable({
            handle: '.card-header',
            opacity: .8,
            tolerance: 'pointer'
        });
    }

    // Daterangepicker
    if (jQuery().daterangepicker) {
        if ($(".datepicker").length) {
            $('.datepicker').daterangepicker({
                locale: { format: 'YYYY-MM-DD' },
                singleDatePicker: true,
            });
        }
        if ($(".datetimepicker").length) {
            $('.datetimepicker').daterangepicker({
                locale: { format: 'YYYY-MM-DD hh:mm' },
                singleDatePicker: true,
                timePicker: true,
                timePicker24Hour: true,
            });
        }
        if ($(".daterange").length) {
            $('.daterange').daterangepicker({
                locale: { format: 'YYYY-MM-DD' },
                drops: 'down',
                opens: 'right'
            });
        }
    }

    // Timepicker
    if (jQuery().timepicker && $(".timepicker").length) {
        $(".timepicker").timepicker({
            icons: {
                up: 'fas fa-chevron-up',
                down: 'fas fa-chevron-down'
            }
        });
    }
});
$(document).ready(function () {
    $('.hamb-menu a').on('click', function () {
        $('#app').toggleClass('shift'); // 'app' ana sınıfına 'shift' ekle veya kaldır
        $('.layout').toggleClass('shift'); // 'layout' ana sınıfına 'shift' ekle veya kaldır
        $('header').toggleClass('expanded'); // 'header' öğesine 'expanded' sınıfını ekle veya kaldır
    });
});


$(document).on('click', '.close', function () {
    $('#timeModal').modal('hide');
});

function openCustomerModal() {

    fetch('/Home/frmCustomerDetails')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
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
function openRezervationModal() {

    fetch('/Home/frmRezervationDetails')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
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
function openAppealModal() {

    fetch('/Home/frmAppealDetails')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
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
function openDetailsModal(id) {

    fetch('/Customer/frmDetails')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
            // İçeriği modal-body123'e yükle
            document.getElementById('Body').innerHTML = html;

            // Modal'ı göster
            const modal = document.getElementById('myModal');
            modal.style.display = 'block';
            modal.classList.add('show');

            // Arka plan için fade sınıfı
            document.body.classList.add('modal-open');

            $.ajax({
                url: "/Customer/GetRezervation/" + id,
                method: "GET",
                success: function (data) {
                    $("#NameSurname").text(data.FirstName + " " + data.LastName)
                    $("#Phone").text(data.Phone)
                    $("#HotelName").text(data.HotelName)
                    $("#RoomNo").text(data.HotelRoomNo)
                    $("#Pax").text(data.CustomerCount)
                    $("#PassportNo").text(data.PassportNo)
                },
                error: function (xhr, status, error) {

                }
            });
        })
        .catch(error => console.error('Error loading modal content:', error));



}
function openEditModal() {

    fetch('/Customer/frmEditRezervation')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
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
function openCreateReservationModal() {

    fetch('/Customer/frmCreateReservation')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
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
function openFilterModal() {

    fetch('/Home/frmFilter')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.text();
        })
        .then(html => {
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

// Modal'ı kapatma işlemi için bir event listener
document.addEventListener('click', function (event) {
    const modal = document.getElementById('myModal');

    if (event.target.classList.contains('close') || event.target.classList.contains('btn-default')) {
        modal.style.display = 'none';
        modal.classList.remove('show');
        document.body.classList.remove('modal-open');
    }
});


$(document).ready(function () {
    // Butonlara tıklama olayını dinle
    $(".btn-hour").on("click", function () {
        // Tıklanan butona "data-selected" özelliğini true yap
        $(this).attr("data-selected", "true");
    });


    $(".btn-hour").on("click", function () {
        // Tüm butonlardan "selected" sınıfını ve data-selected="true" değerini kaldır
        $(".btn-hour").removeClass("selected").attr("data-selected", "false");

        // Sadece tıklanan butona "selected" sınıfını ekle ve data-selected="true" yap
        $(this).addClass("selected").attr("data-selected", "true");
    });
});

function toggleSettingsMenu() {
    const menu = document.getElementById('settingsMenu');
    if (menu.style.display === 'block') {
        menu.style.display = 'none'; // Menü açıkken kapat
    } else {
        menu.style.display = 'block'; // Menü kapalıyken aç
    }
}

function saveCapacities() {
    const capacities = {
        horse: document.getElementById('horseCapacity').value,
        atv: document.getElementById('atvCapacity').value,
        jeep: document.getElementById('jeepCapacity').value,
        balloon: document.getElementById('balloonCapacity').value,
        camel: document.getElementById('camelCapacity').value,
    };
    console.log('Kaydedilen Kapasiteler:', capacities);
    alert('Kapasiteler başarıyla kaydedildi!');
}





// Rezervasyon başvuru eventi

function saveRezervation() {
    var dateText = $('#timeModalLabel').text();
    var timeText = $(".btn-hour[data-selected='true']").text();
    var fullDateTime = new Date(dateText + ' ' + timeText);

    // Date formatını 'yyyy-MM-dd HH:mm:ss' olarak ayarlıyoruz
    var formattedDate = fullDateTime.getFullYear() + '-' +
        ('0' + (fullDateTime.getMonth() + 1)).slice(-2) + '-' +
        ('0' + fullDateTime.getDate()).slice(-2) + ' ' +
        ('0' + fullDateTime.getHours()).slice(-2) + ':' +
        ('0' + fullDateTime.getMinutes()).slice(-2) + ':' +
        ('0' + fullDateTime.getSeconds()).slice(-2);

    // Backend'e doğru tarih formatında gönderiyoruz (ISO 8601 formatı)
    var isoDate = fullDateTime.toISOString();  // 2024-01-14T13:00:00.000Z

    var name = $("#guestName").val();
    var surname = $("#guestSurname").val();
    var phone = $("#phone").val();
    var hotelName = $("#hotelName").val();
    var roomNumber = $("#roomNumber").val();
    var customerCount = $("#customerCount").val();
    var passportNo = $("#passportNo").val();

    $.ajax({
        url: "/Customer/SaveRezervation/",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            name: name,
            surname: surname,
            phone: phone,
            roomNumber: roomNumber,
            hotelName: hotelName,
            reservationTime: isoDate,
            customerCount: customerCount,
            passportNo: passportNo
        }),
        success: function (response) {
            alert(`Seçili saat: ${isoDate}, Cevap: ${response.message}`);
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            alert("Bir hata oluştu.");
        }
    });
}
function ConfirmRezervation(id) {
    $.ajax({
        url: "/Home/ConfirmRezervation/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {

            Swal.fire({
                title: 'Başarılı',
                text: 'Başvuru Onaylandı',
                icon: 'success',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                // Eğer kullanıcı 'Tamam' butonuna tıklarsa
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
function CancelRezervation(id) {
    $.ajax({
        url: "/Home/CancelRezervation/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {

            Swal.fire({
                title: 'Başarılı',
                text: 'Başvuru İptal Edildi',
                icon: 'success',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                // Eğer kullanıcı 'Tamam' butonuna tıklarsa
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

function deleteCustomer(id) {
    $.ajax({
        url: "/Home/DeleteCustomer/",
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {

            Swal.fire({
                title: 'Başarılı',
                text: 'Müşteri silindi',
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
} function confirmCustomer(id) {
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
                icon: 'succes',
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
// Tur Kategorisi Dropdown İşlemleri
document.querySelectorAll('#dropdownKategoriler .dropdown-item').forEach(item => {
    item.addEventListener('click', function () {
        const kategoriInput = document.getElementById('kategori'); // Kategori input
        const kategoriHidden = document.getElementById('form_kategori'); // Gizli input

        // Seçilen değerleri inputlara yaz
        kategoriInput.value = this.getAttribute('data-baslik');
        kategoriHidden.value = this.getAttribute('data-id');

        // Dropdown'u kapat
        const dropdownMenu = this.closest('.dropdown-menu');
        dropdownMenu.classList.remove('show');
    });
});

// Tur Konumu Dropdown İşlemleri
document.querySelectorAll('#dropdownKonumlar .dropdown-item').forEach(item => {
    item.addEventListener('click', function () {
        const konumInput = document.getElementById('konum'); // Konum input
        const konumHidden = document.getElementById('form_konum'); // Gizli input

        // Seçilen değerleri inputlara yaz
        konumInput.value = this.getAttribute('data-baslik');
        konumHidden.value = this.getAttribute('data-id');

        // Dropdown'u kapat
        const dropdownMenu = this.closest('.dropdown-menu');
        dropdownMenu.classList.remove('show');
    });
});

// Genel Dropdown Aç-Kapat İşlemleri
document.querySelectorAll('.closeDropdown').forEach(action => {
    action.addEventListener('click', function () {
        const dropdown = this.nextElementSibling;
        if (dropdown.classList.contains('show')) {
            dropdown.classList.remove('show'); // Kapat
        } else {
            dropdown.classList.add('show'); // Aç
        }
    });
});





$(document).ready(function () {
    $('#example').DataTable({
        ajax: {
            url: '/Customer/GetRezervation',
            dataSrc: ''
        },
        columns: [

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
            
            },
            {
            orderable: false,
            searchable: false 
            }
            
            
        ]
    });
});




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
                alert(response.message); 
                $('#example').DataTable().ajax.reload(); 
            } else {
                alert(response.message); 
            }
        },
        error: function (xhr, status, error) {
            console.error('Güncelleme hatası:', error);
            alert('Bir hata oluştu, lütfen tekrar deneyin.');
        }
    });
}

