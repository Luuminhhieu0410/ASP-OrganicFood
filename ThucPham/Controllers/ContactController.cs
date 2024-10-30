using Microsoft.AspNetCore.Mvc;

namespace ThucPham.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
