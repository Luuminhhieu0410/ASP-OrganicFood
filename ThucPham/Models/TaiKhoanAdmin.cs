using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class TaiKhoanAdmin
{
    public int MaAdmin { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? QuyenHan { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();
}
