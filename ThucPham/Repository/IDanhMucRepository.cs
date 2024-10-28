using ThucPham.Models;
namespace ThucPham.Repository
{
    public interface IDanhMucRepository
    {
        DanhMuc Add(DanhMuc danhMuc);
        DanhMuc Update(DanhMuc danhMuc);
        DanhMuc Delete(String MaDanhMuc);
        DanhMuc GetDanhMuc(String MaDanhMuc);
        IEnumerable<DanhMuc> GetAllDanhMuc();  
    }
}
