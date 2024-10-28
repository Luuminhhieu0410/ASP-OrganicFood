using System;
using System.Collections.Generic;

namespace ThucPham.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string TenSp { get; set; } = null!;

    public decimal Gia { get; set; }

    public string? MoTa { get; set; }

    public int SoLuongTon { get; set; }

    public int MaDanhMuc { get; set; }

    public string? HinhAnh { get; set; }

    public DateOnly? NgayTao { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    public virtual DanhMuc MaDanhMucNavigation { get; set; } = null!;
}
