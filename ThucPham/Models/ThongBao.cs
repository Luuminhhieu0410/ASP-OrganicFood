using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class ThongBao
{
    public int MaTb { get; set; }

    public int? MaNguoiNhan { get; set; }

    public string? LoaiNguoiNhan { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateOnly? NgayGui { get; set; }
}
