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
    }
}
