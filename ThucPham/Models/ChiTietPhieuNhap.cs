using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class ChiTietPhieuNhap
{
    public int MaPn { get; set; }

    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    public decimal GiaNhap { get; set; }

    public virtual PhieuNhap MaPnNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
