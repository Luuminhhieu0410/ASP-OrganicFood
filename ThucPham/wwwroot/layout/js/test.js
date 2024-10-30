(function ($) {
    $(document).ready(function () {
        // Thêm nút tăng/giảm vào mỗi phần tử có class 'pro-qty'
        $('.pro-qty').each(function () {
            $(this).prepend('<span class="dec qtybtn">-</span>');
            $(this).append('<span class="inc qtybtn">+</span>');
        });

        // Hàm cập nhật tổng tiền từng sản phẩm
        function updateProductTotal($input) {
            const qty = parseFloat($input.val()); // Số lượng mới
            const price = parseFloat(
                $input.closest('tr').find('.shoping__cart__price').text().replace(/[^0-9.-]+/g, "")
            ); // Lấy giá sản phẩm

            const total = qty * price; // Tính tổng tiền của sản phẩm đó
            $input.closest('tr').find('.shoping__cart__total').text(
                total.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
            );
        }

        // Hàm cập nhật tổng tiền của toàn bộ giỏ hàng
        function updateCartTotal() {
            let cartTotal = 0;
            $('.shoping__cart__total').each(function () {
                const productTotal = parseFloat($(this).text().replace(/[^0-9.-]+/g, ""));
                cartTotal += productTotal;
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
                newVal = oldValue + 1;
            } else {
                newVal = oldValue > 0 ? oldValue - 1 : 0;
            }

            $input.val(newVal); // Cập nhật số lượng mới vào input
            updateProductTotal($input); // Cập nhật tổng tiền sản phẩm
            updateCartTotal(); // Cập nhật tổng tiền giỏ hàng
        });
    });

})(jQuery);