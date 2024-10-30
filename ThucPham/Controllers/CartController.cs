using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThucPham.Models;
using System.Linq;

namespace ThucPham.Controllers
{
    public class CartController : Controller
    {
        
        private readonly QlthucPhamHuuCoContext _context;

        public CartController(QlthucPhamHuuCoContext context)
        {
            _context = context;
        }

        public IActionResult AddToCart(int maSP)
        {
            int maKH = int.Parse(HttpContext.Session.GetString("MaKh"));

            var gioHangItem = _context.GioHangs
                .FirstOrDefault(gh => gh.MaKh == maKH && gh.MaSp == maSP);

            if (gioHangItem != null)
            {
                gioHangItem.SoLuong++;
            }
            else
            {
                gioHangItem = new GioHang
                {
                    MaKh = maKH,
                    MaSp = maSP,
                    SoLuong = 1
                };
                _context.GioHangs.Add(gioHangItem);
            }

            _context.SaveChanges();
            return RedirectToAction("ShopCart");
        }

        public IActionResult ShopCart()
        {
            int maKH = int.Parse(HttpContext.Session.GetString("MaKh"));

            var cartItems = _context.GioHangs
                .Where(gh => gh.MaKh == maKH)
                .Include(gh => gh.MaSpNavigation)
                .Select(gh => new CartItemViewModel
                {
                    MaSp = gh.MaSp,
                    TenSp = gh.MaSpNavigation.TenSp,
                    Gia = gh.MaSpNavigation.Gia,
                    SoLuong = gh.SoLuong,
                    Anh = gh.MaSpNavigation.HinhAnh,
                    TongTien = gh.SoLuong * gh.MaSpNavigation.Gia
                })
                .ToList();

            return View(cartItems);
        }
        [HttpPost]
        public IActionResult UpdateCart([FromBody] List<CartItemViewModel> cartItems)
        {
            // Kiểm tra xem cartItems có null hay không
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            int maKH = int.Parse(HttpContext.Session.GetString("MaKh"));

            // Xóa tất cả sản phẩm trong giỏ hàng của khách hàng
            var existingItems = _context.GioHangs.Where(gh => gh.MaKh == maKH).ToList();
            _context.GioHangs.RemoveRange(existingItems);

            try
            {
                // Thêm sản phẩm mới vào giỏ hàng
                foreach (var item in cartItems)
                {
                    if (item.SoLuong > 0) // Chỉ thêm sản phẩm có số lượng lớn hơn 0
                    {
                        // Tìm sản phẩm trong cơ sở dữ liệu
                        var product = _context.SanPhams.Find(item.MaSp);

                        if (product != null)
                        {
                            var gioHangItem = new GioHang
                            {
                                MaKh = maKH,
                                MaSp = item.MaSp,
                                SoLuong = item.SoLuong
                            };
                            _context.GioHangs.Add(gioHangItem);
                        }
                        else
                        {
                            // Xử lý khi sản phẩm không tồn tại
                            return NotFound($"Sản phẩm với mã {item.MaSp} không tồn tại.");
                        }
                    }
                }

                // Lưu thay đổi vào database
                var result = _context.SaveChanges();

                if (result > 0) // Kiểm tra xem có thay đổi nào được ghi vào không
                {
                    return Ok(new { success = true });
                }
                else
                {
                    return StatusCode(500, "Không thể cập nhật giỏ hàng.");
                }
            }
            catch (Exception ex)
            {
                // Ghi lại thông tin chi tiết về lỗi
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }



    }

}
