using Microsoft.AspNetCore.Mvc;
using ThucPham.Models;
namespace ThucPham.Controllers
{
    public class AccessController : Controller
    {
        public QlthucPhamHuuCoContext db = new QlthucPhamHuuCoContext();
        public AccessController() { }
        public IActionResult login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult login(KhachHang kh)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.KhachHangs.Where(x => x.Email.Equals(kh.Email) && x.MatKhau.Equals(kh.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.HoTen.ToString());
                    HttpContext.Session.SetString("Email", u.Email.ToString());
                    var a = HttpContext.Session.GetString("UserName");


                    Console.WriteLine($"Username from session: {a}");  // Kiểm tra giá trị
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();

        }
    }
}
     