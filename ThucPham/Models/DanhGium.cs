using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class DanhGium
{
    public int MaDg { get; set; }

    public int MaKh { get; set; }

    public int MaSp { get; set; }

    public int? Diem { get; set; }

    public string? BinhLuan { get; set; }

    public DateOnly? NgayDanhGia { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
