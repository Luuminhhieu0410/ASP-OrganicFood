/*  ---------------------------------------------------
    Template Name: Ogani
    Description:  Ogani eCommerce  HTML Template
    Author: Colorlib
    Author URI: https://colorlib.com
    Version: 1.0
    Created: Colorlib
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
        Preloader
    --------------------*/
    $(window).on('load', function () {
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");

        /*------------------
            Gallery filter
        --------------------*/
        $('.featured__controls li').on('click', function () {
            $('.featured__controls li').removeClass('active');
            $(this).addClass('active');
        });
        if ($('.featured__filter').length > 0) {
            var containerEl = document.querySelector('.featured__filter');
            var mixer = mixitup(containerEl);
        }
    });

    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    //Humberger Menu
    $(".humberger__open").on('click', function () {
        $(".humberger__menu__wrapper").addClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").addClass("active");
        $("body").addClass("over_hid");
    });

    $(".humberger__menu__overlay").on('click', function () {
        $(".humberger__menu__wrapper").removeClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").removeClass("active");
        $("body").removeClass("over_hid");
    });

    /*------------------
		Navigation
	--------------------*/
    $(".mobile-menu").slicknav({
        prependTo: '#mobile-menu-wrap',
        allowParentLinks: true
    });

    /*-----------------------
        Categories Slider
    ------------------------*/
    $(".categories__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 4,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            0: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 3,
            },

            992: {
                items: 4,
            }
        }
    });


    $('.hero__categories__all').on('click', function(){
        $('.hero__categories ul').slideToggle(400);
    });

    /*--------------------------
        Latest Product Slider
    ----------------------------*/
    $(".latest-product__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 1,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------------
        Product Discount Slider
    -------------------------------*/
    $(".product__discount__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 3,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            320: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 2,
            },

            992: {
                items: 3,
            }
        }
    });

    /*---------------------------------
        Product Details Pic Slider
    ----------------------------------*/
    $(".product__details__pic__slider").owlCarousel({
        loop: true,
        margin: 20,
        items: 4,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------
		Price Range Slider
	------------------------ */
    var rangeSlider = $(".price-range"),
        minamount = $("#minamount"),
        maxamount = $("#maxamount"),
        minPrice = rangeSlider.data('min'),
        maxPrice = rangeSlider.data('max');
    rangeSlider.slider({
        range: true,
        min: minPrice,
        max: maxPrice,
        values: [minPrice, maxPrice],
        slide: function (event, ui) {
            minamount.val('$' + ui.values[0]);
            maxamount.val('$' + ui.values[1]);
        }
    });
    minamount.val('$' + rangeSlider.slider("values", 0));
    maxamount.val('$' + rangeSlider.slider("values", 1));

    /*--------------------------
        Select
    ----------------------------*/
    $("select").niceSelect();

    /*------------------
		Single Product
	--------------------*/
    $('.product__details__pic__slider img').on('click', function () {

        var imgurl = $(this).data('imgbigurl');
        var bigImg = $('.product__details__pic__item--large').attr('src');
        if (imgurl != bigImg) {
            $('.product__details__pic__item--large').attr({
                src: imgurl
            });
        }
    });

    /*-------------------
		Quantity change
	--------------------- */
    $(document).ready(function () {
        // Thêm nút tăng/giảm vào mỗi phần tử có class 'pro-qty'
        $('.pro-qty').each(function () {
            $(this).prepend('<span class="dec qtybtn">-</span>');
            $(this).append('<span class="inc qtybtn">+</span>');
        });

        // Hàm cập nhật tổng tiền cho từng sản phẩm
        function updateProductTotal($input) {
            const qty = parseFloat($input.val()); // Số lượng mới
            const price = parseFloat(
                $input.closest('tr').find('.shoping__cart__price').text().replace(/[^0-9.-]+/g, "")
            ); // Lấy giá sản phẩm

            const total = qty * price; // Tính tổng tiền của sản phẩm đó
            $input.closest('tr').find('.shoping__cart__total').text(
                total.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
            );

            return total; // Trả về tổng tiền của sản phẩm
        }

        // Hàm cập nhật tổng tiền của tất cả sản phẩm trong giỏ hàng
        function updateCartTotal() {
            let cartTotal = 0;
            $('.shoping__cart__total').each(function () {
                const productTotal = parseFloat($(this).text().replace(/[^0-9.-]+/g, ""));
                cartTotal += productTotal; // Cộng dồn tổng tiền sản phẩm
            });

            // Cập nhật giá trị tổng tiền giỏ hàng
            $('.shoping__checkout ul li:nth-child(1) span').text(
                cartTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
            );
            $('.shoping__checkout ul li:nth-child(2) span').text(
                cartTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
            );
        }

        // Sự kiện click cho nút tăng/giảm số lượng
        $('.pro-qty').on('click', '.qtybtn', function () {
            const $button = $(this);
            const $input = $button.siblings('input'); // Lấy input tương ứng trong hàng

            let oldValue = parseFloat($input.val());
            let newVal;

            // Xử lý tăng hoặc giảm số lượng
            if ($button.hasClass('inc')) {
                newVal = oldValue + 1; // Tăng số lượng
            } else {
                newVal = oldValue > 0 ? oldValue - 1 : 0; // Giảm số lượng (không dưới 0)
            }

            $input.val(newVal); // Cập nhật số lượng mới vào input
            const productTotal = updateProductTotal($input); // Cập nhật tổng tiền sản phẩm
            updateCartTotal(); // Cập nhật tổng tiền giỏ hàng
        });

        // Cập nhật tổng tiền khi trang được tải
        updateCartTotal();
    });

    $(document).ready(function () {
        // Hàm cập nhật tổng tiền cho từng sản phẩm
        function updateProductTotal($input) {
            const qty = parseFloat($input.val()); // Số lượng mới
            const price = parseFloat(
                $input.closest('tr').find('.shoping__cart__price').text().replace(/[^0-9.-]+/g, "")
            ); // Lấy giá sản phẩm

            const total = qty * price; // Tính tổng tiền của sản phẩm đó
            $input.closest('tr').find('.shoping__cart__total').text(
                total.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
            );

            return total; // Trả về tổng tiền của sản phẩm
        }

        // Hàm cập nhật tổng tiền của tất cả sản phẩm trong giỏ hàng
        function updateCartTotal() {
            let cartTotal = 0;
            $('.shoping__cart__total').each(function () {
                const productTotal = parseFloat($(this).text().replace(/[^0-9.-]+/g, ""));
                cartTotal += productTotal; // Cộng dồn tổng tiền sản phẩm
            });

            // Cập nhật giá trị tổng tiền giỏ hàng
            $('#TongTienToanSanPham').text(
                cartTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
            );
        }

        // Sự kiện click cho nút tăng/giảm số lượng
        $('.pro-qty').on('click', '.qtybtn', function () {
            const $button = $(this);
            const $input = $button.siblings('input'); // Lấy input tương ứng trong hàng

            let oldValue = parseFloat($input.val());
            let newVal;

            // Xử lý tăng hoặc giảm số lượng
            if ($button.hasClass('inc')) {
                newVal = oldValue + 1; // Tăng số lượng
            } else {
                newVal = oldValue > 0 ? oldValue - 1 : 0; // Giảm số lượng (không dưới 0)
            }

            $input.val(newVal); // Cập nhật số lượng mới vào input
            updateProductTotal($input); // Cập nhật tổng tiền sản phẩm
            updateCartTotal(); // Cập nhật tổng tiền giỏ hàng
        });

        // Cập nhật tổng tiền khi trang được tải
        updateCartTotal();

        // Sự kiện khi nhấn nút "Update Cart"
        $('.cart-btn-right').on('click', function (e) {
            e.preventDefault(); // Ngăn chặn hành động mặc định của liên kết

            // Tạo một mảng để lưu trữ các sản phẩm trong giỏ hàng
            const cartItems = [];

            // Duyệt qua từng hàng trong giỏ hàng để lấy thông tin
            $('.shoping__cart__table tbody tr').each(function () {
                const $tr = $(this);
                const item = {
                    TenSp: $tr.find('h5').text(), // Tên sản phẩm
                    SoLuong: $tr.find('.SoLg').val(), // Số lượng
                    Gia: parseFloat($tr.find('.shoping__cart__price').text().replace(/[^0-9.-]+/g, "")), // Giá
                    TongTien: parseFloat($tr.find('.shoping__cart__total').text().replace(/[^0-9.-]+/g, "")) // Tổng tiền
                };
                cartItems.push(item);
            });

            // Gửi dữ liệu đến server (có thể sử dụng AJAX)
            $.ajax({
                type: "POST",
                url: '/Cart/UpdateCart', // Đường dẫn đến action UpdateCart
                contentType: "application/json",
                data: JSON.stringify(cartItems),
                success: function (response) {
                    alert("Giỏ hàng đã được cập nhật!");
                    // Có thể reload lại trang hoặc cập nhật lại thông tin hiển thị
                    location.reload(); // Reload lại trang
                },
                error: function (xhr, status, error) {
                    alert("Cập nhật giỏ hàng không thành công.");
                }
            });

        });
    });



})(jQuery);