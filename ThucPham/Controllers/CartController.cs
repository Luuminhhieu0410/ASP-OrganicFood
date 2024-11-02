using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThucPham.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using System;

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
        [HttpPost]
        public IActionResult AddToCart1(int ProductId, int Quantity = 1)
        {
            if (Quantity <= 0)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });
            }

            // Lấy MaKH từ session
            int maKH = int.Parse(HttpContext.Session.GetString("MaKh"));

            // Kiểm tra xem sản phẩm có tồn tại không
            var product = _context.SanPhams.Find(ProductId);
            if (product == null)
            {
                return NotFound(new { message = "Sản phẩm không tồn tại." });
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
            var existingCartItem = _context.GioHangs
                .FirstOrDefault(g => g.MaKh == maKH && g.MaSp == ProductId);

            if (existingCartItem != null)
            {
                // Nếu có, cập nhật số lượng
                existingCartItem.SoLuong += Quantity;
            }
            else
            {
                // Nếu không, tạo mới sản phẩm trong giỏ hàng
                var cartItem = new GioHang
                {
                    MaKh = maKH,
                    MaSp = ProductId,
                    SoLuong = Quantity
                };
                _context.GioHangs.Add(cartItem);
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return Ok(new { message = "Sản phẩm đã được thêm vào giỏ hàng!" });
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
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
            }

            // Lấy mã khách hàng từ session
            int maKH = int.Parse(HttpContext.Session.GetString("MaKh"));

            using (var connection = (SqlConnection)_context.Database.GetDbConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Xóa tất cả sản phẩm trong giỏ hàng của khách hàng hiện tại
                        var deleteCommand = new SqlCommand(
                            "DELETE FROM GioHang WHERE MaKH = @MaKH", connection, transaction);
                        deleteCommand.Parameters.AddWithValue("@MaKH", maKH);
                        deleteCommand.ExecuteNonQuery();

                        // 2. Thêm từng sản phẩm mới vào giỏ hàng
                        foreach (var item in cartItems)
                        {
                            if (item.SoLuong > 0)
                            {
                                var insertCommand = new SqlCommand(
                                    "INSERT INTO GioHang (MaKH, MaSP, SoLuong) VALUES (@MaKH, @MaSP, @SoLuong)",
                                    connection, transaction);
                                insertCommand.Parameters.AddWithValue("@MaKH", maKH);
                                insertCommand.Parameters.AddWithValue("@MaSP", item.MaSp);
                                insertCommand.Parameters.AddWithValue("@SoLuong", item.SoLuong);

                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction if all commands succeed
                        transaction.Commit();

                        return Ok(new { success = true, message = "Giỏ hàng đã được cập nhật thành công!" });
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if any command fails
                        transaction.Rollback();
                        return StatusCode(500, new { success = false, message = $"Không thể cập nhật giỏ hàng: {ex.Message}" });
                    }
                }
            }
        }

        
        public IActionResult DeleteItem(int MaSp)
        {
            TempData["Mes"] = "";
            int maKH = int.Parse(HttpContext.Session.GetString("MaKh"));
            var cartItem = _context.GioHangs.FirstOrDefault(g => g.MaKh == maKH && g.MaSp == MaSp);
            if (cartItem != null)
            {
                _context.GioHangs.Remove(cartItem);
                _context.SaveChanges();
                TempData["Mes"] = "Xóa thành công";
                return RedirectToAction("ShopCart", "Cart");
            }
            return RedirectToAction("ShopCart", "Cart");

        }

    }

}
