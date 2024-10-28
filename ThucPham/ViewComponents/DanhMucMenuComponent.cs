using ThucPham.Models;
using Microsoft.AspNetCore.Mvc;
using ThucPham.Repository;
namespace ThucPham.ViewComponents
{
    [ViewComponent(Name = "DanhMucMenu")]

    public class DanhMucMenuComponent : ViewComponent
    {
        public readonly IDanhMucRepository _danhMuc;
        public DanhMucMenuComponent(IDanhMucRepository iDanhMuc)
        {
            _danhMuc = iDanhMuc;
        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _danhMuc.GetAllDanhMuc().OrderBy(x => x.MaDanhMuc);
            return View(loaisp);
        }
    }
}