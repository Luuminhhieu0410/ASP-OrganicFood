using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int MaKh { get; set; }

    public DateOnly? NgayLap { get; set; }

    public decimal TongTien { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
