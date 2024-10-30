using Microsoft.AspNetCore.Mvc;

namespace ThucPham.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
