using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;
using ThucPham.Models;
using ThucPham.Models.Authentication;
using X.PagedList;

namespace ThucPham.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public QlthucPhamHuuCoContext db = new QlthucPhamHuuCoContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.SanPhams.AsNoTracking().OrderBy(x=>x.TenSp);
            PagedList<SanPham> lst = new PagedList<SanPham>(lstsanpham,pageNumber,pageSize);
            return View(lst);
        }
        [Authentication]
        public IActionResult SanPhamTheoLoai(int maDanhMuc,int? page ,int maSP = 0)
        {
            int pageSize = 8;
            int pageNumber = page == null || page <0 ? 1 : page.Value;
            var lstsanpham = db.SanPhams.AsNoTracking().Where(x => x.MaDanhMuc == maDanhMuc).OrderBy(x => x.TenSp);
            PagedList<SanPham> lst = new PagedList<SanPham>(lstsanpham, pageNumber, pageSize);

            ViewBag.maDanhMuc = maDanhMuc;
            
            return View(lst);
        }

        [Authentication]

        public IActionResult ChiTietSanPham(int maSP, int maDanhMuc = 0)
        {
            var sanPham = db.SanPhams.SingleOrDefault(x => x.MaSp == maSP);
            var anhSanPham = sanPham.HinhAnh;
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }

         
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
