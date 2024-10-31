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
                    HttpContext.Session.SetString("MaKh", u.MaKh.ToString());
                    var a = HttpContext.Session.GetString("UserName");


                    Console.WriteLine($"Username from session: {a}");  // Kiểm tra giá trị
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();

        }
		[HttpGet]
		public IActionResult Signup()
		{
			if (HttpContext.Session.GetString("UserName") == null)
				return View();
			else
				return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Signup(KhachHang kh, string NhapLaiMatKhau)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				if (kh.MatKhau != NhapLaiMatKhau)
				{
					ModelState.AddModelError("MatKhau", "Mật khẩu và Nhập lại mật khẩu không trùng khớp.");
					return View();
				}

				if (db.KhachHangs.Any(x => x.Email == kh.Email))
				{
					ModelState.AddModelError("Email", "Email đã tồn tại.");
					return View();
				}

				db.KhachHangs.Add(kh);
				db.SaveChanges();

				HttpContext.Session.SetString("UserName", kh.HoTen);
				HttpContext.Session.SetString("Email", kh.Email);
				HttpContext.Session.SetString("MaKh", kh.MaKh.ToString());

				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("MaKh");
            return RedirectToAction("login", "Access");
        }
    }
}
     