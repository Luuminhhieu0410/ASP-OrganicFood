using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class PhieuNhap
{
    public int MaPn { get; set; }

    public int? MaAdmin { get; set; }

    public DateOnly? NgayNhap { get; set; }

    public decimal TongTien { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual TaiKhoanAdmin? MaAdminNavigation { get; set; }
}
