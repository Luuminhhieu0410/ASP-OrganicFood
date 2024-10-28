using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class GioHang
{
    public int MaKh { get; set; }

    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
