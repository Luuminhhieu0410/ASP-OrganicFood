create database QLThucPhamHuuCo
go
use QLThucPhamHuuCo
go

-- Khách hàng
CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    SoDienThoai NVARCHAR(15),
    DiaChi NVARCHAR(255),
    MatKhau NVARCHAR(255) NOT NULL,
    NgayDangKy DATE DEFAULT GETDATE()
);

-- Tài khoản quản trị viên
CREATE TABLE TaiKhoanAdmin (
    MaAdmin INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    QuyenHan NVARCHAR(50) DEFAULT N'Quản trị viên'
);

-- Danh mục sản phẩm
CREATE TABLE DanhMuc (
    MaDanhMuc INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX)
);

-- Sản phẩm
CREATE TABLE SanPham (
    MaSP INT PRIMARY KEY IDENTITY(1,1),
    TenSP NVARCHAR(100) NOT NULL,
    Gia DECIMAL(18,2) NOT NULL,
    MoTa NVARCHAR(MAX),
    SoLuongTon INT NOT NULL,
    MaDanhMuc INT NOT NULL,
    HinhAnh NVARCHAR(255),
    NgayTao DATE DEFAULT GETDATE(),
    FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
);

-- Nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNCC INT PRIMARY KEY IDENTITY(1,1),
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDienThoai NVARCHAR(15),
    Email NVARCHAR(100)
);

-- Phiếu nhập hàng
CREATE TABLE PhieuNhap (
    MaPN INT PRIMARY KEY IDENTITY(1,1),
    MaAdmin INT ,
    NgayNhap DATE DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (MaAdmin) REFERENCES TaiKhoanAdmin(MaAdmin)
);


-- Chi tiết phiếu nhập
CREATE TABLE ChiTietPhieuNhap (
    MaPN INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT NOT NULL,
    GiaNhap DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaPN, MaSP),
    FOREIGN KEY (MaPN) REFERENCES PhieuNhap(MaPN),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


-- Giỏ hàng
CREATE TABLE GioHang (
    MaKH INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT NOT NULL,
    PRIMARY KEY (MaKH, MaSP),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- Đơn hàng
CREATE TABLE HoaDon (
    MaHD INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NOT NULL,
    NgayLap DATE DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT N'Chờ xử lý',
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- Chi tiết đơn hàng
CREATE TABLE ChiTietHoaDon (
    MaHD INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT NOT NULL,
    Gia DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaHD, MaSP),
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


-- Đánh giá sản phẩm
CREATE TABLE DanhGia (
    MaDG INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NOT NULL,
    MaSP INT NOT NULL,
    Diem INT CHECK (Diem BETWEEN 1 AND 5),
    BinhLuan NVARCHAR(MAX),
    NgayDanhGia DATE DEFAULT GETDATE(),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- Thông báo
CREATE TABLE ThongBao (
    MaTB INT PRIMARY KEY IDENTITY(1,1),
    MaNguoiNhan INT,
    LoaiNguoiNhan NVARCHAR(50) CHECK (LoaiNguoiNhan IN (N'KhachHang', N'Admin')),
    NoiDung NVARCHAR(MAX) NOT NULL,
    NgayGui DATE DEFAULT GETDATE()
);
INSERT INTO DanhMuc (TenDanhMuc, MoTa) VALUES 
(N'Rau củ', N'Các loại rau củ hữu cơ'),
(N'Trái cây', N'Trái cây tươi sạch'),
(N'Ngũ cốc', N'Ngũ cốc dinh dưỡng hữu cơ'),
(N'Sữa và sản phẩm từ sữa', N'Sữa hữu cơ và các sản phẩm liên quan');

INSERT INTO SanPham (TenSP, Gia, MoTa, SoLuongTon, MaDanhMuc, HinhAnh) 
VALUES
-- Rau củ
(N'Rau cải ngọt', 15000, N'Rau cải ngọt trồng hữu cơ, không thuốc trừ sâu.', 100, 1, '1.jpg'),
(N'Cà rốt', 25000, N'Cà rốt Đà Lạt tươi ngon, giàu vitamin A.', 80, 1, '2.jpg'),
(N'Khoai tây', 30000, N'Khoai tây hữu cơ nhập khẩu từ Hà Lan.', 60, 1, '3.jpg'),
(N'Dưa leo', 18000, N'Dưa leo tươi mát, thích hợp làm salad.', 120, 1, '4.jpg'),

-- Trái cây
(N'Táo Fuji', 50000, N'Táo Fuji nhập khẩu từ Nhật Bản, giòn ngọt.', 50, 2, '5.jpg'),
(N'Chuối Laba', 22000, N'Chuối Laba Đà Lạt, giàu dinh dưỡng.', 200, 2, '6.jpg'),
(N'Bưởi da xanh', 60000, N'Bưởi da xanh Bến Tre, thơm ngon.', 70, 2, '7.jpg'),
(N'Dâu tây', 80000, N'Dâu tây hữu cơ, không thuốc bảo vệ thực vật.', 30, 2, '8.jpg'),

-- Ngũ cốc
(N'Gạo lứt đỏ', 45000, N'Gạo lứt hữu cơ giàu chất xơ và vitamin.', 100, 3, '9.jpg'),
(N'Hạt chia', 300000, N'Hạt chia nhập khẩu từ Úc, giúp tăng cường sức khỏe.', 40, 3, '10.jpg'),
(N'Yến mạch', 120000, N'Yến mạch nguyên chất, giàu dinh dưỡng.', 60, 3, '11.jpg'),
(N'Đậu nành', 20000, N'Đậu nành hữu cơ, không biến đổi gen.', 90, 3, '12.jpg'),

-- Sữa và sản phẩm từ sữa
(N'Sữa tươi không đường', 25000, N'Sữa tươi nguyên chất, không chất bảo quản.', 80, 4, '13.jpg'),
(N'Phô mai hữu cơ', 70000, N'Phô mai hữu cơ, dùng trong chế biến món ăn.', 50, 4, '14.jpg'),
(N'Sữa chua dẻo', 30000, N'Sữa chua dẻo, thơm ngon tốt cho tiêu hóa.', 100, 4, '15.jpg'),
(N'Kem sữa tươi', 50000, N'Kem sữa tươi từ sữa hữu cơ nguyên chất.', 40, 4, '16.jpg'),

-- Rau củ bổ sung
(N'Súp lơ xanh', 35000, N'Súp lơ xanh tươi, tốt cho sức khỏe.', 90, 1, '17.jpg'),
(N'Rau muống', 20000, N'Rau muống sạch, không sử dụng phân bón hóa học.', 110, 1, '18.jpg'),
(N'Bắp cải tím', 40000, N'Bắp cải tím nhập khẩu, giòn ngọt.', 70, 1, '19.jpg'),

-- Trái cây bổ sung
(N'Xoài cát Hòa Lộc', 55000, N'Xoài cát Hòa Lộc nổi tiếng miền Nam.', 60, 2, '20.jpg'),
(N'Vú sữa', 35000, N'Vú sữa Lò Rèn, đặc sản miền Tây.', 80, 2, '21.jpg'),

-- Ngũ cốc bổ sung
(N'Hạt điều', 500000, N'Hạt điều rang muối, giàu protein.', 40, 3, '22.jpg'),
(N'Hạnh nhân', 600000, N'Hạnh nhân nhập khẩu từ Mỹ, tốt cho tim mạch.', 50, 3, '23.jpg'),

-- Sữa và sản phẩm bổ sung
(N'Sữa đậu nành', 20000, N'Sữa đậu nành nguyên chất, không đường.', 100, 4, '24.jpg'),
(N'Sữa chua uống', 15000, N'Sữa chua uống có men sống, tốt cho tiêu hóa.', 120, 4, '25.jpg'),

-- Rau củ bổ sung
(N'Bí đỏ', 25000, N'Bí đỏ hữu cơ, giàu vitamin A.', 70, 1, '26.jpg'),
(N'Mướp hương', 20000, N'Mướp hương thơm ngon, thích hợp nấu canh.', 80, 1, '27.jpg'),
(N'Dậu bắp', 18000, N'Đậu bắp giòn, hỗ trợ tiêu hóa tốt.', 120, 1, '28.jpg'),

-- Trái cây bổ sung
(N'Mít thái', 45000, N'Mít thái ngọt, không mủ.', 60, 2, '29.jpg'),
(N'Cam sành', 40000, N'Cam sành Vĩnh Long, thơm ngon.', 100, 2, '30.jpg'),
(N'Quýt đường', 35000, N'Quýt đường, vị ngọt thanh.', 90, 2, '31.jpg'),

-- Ngũ cốc bổ sung
(N'Hạt sen', 300000, N'Hạt sen khô, bổ dưỡng cho sức khỏe.', 50, 3, '32.jpg'),
(N'Đậu xanh', 20000, N'Đậu xanh hữu cơ, nguyên vỏ.', 110, 3, '33.jpg'),
(N'Lúa mì', 150000, N'Lúa mì nguyên cám, làm bánh mì.', 70, 3, '34.jpg'),

-- Sữa và sản phẩm từ sữa bổ sung
(N'Kem chanh', 30000, N'Kem tươi hương chanh, mát lạnh.', 40, 4, '35.jpg'),
(N'Sữa hạt óc chó', 60000, N'Sữa hạt giàu omega-3.', 80, 4, '36.jpg'),
(N'Sữa dừa', 45000, N'Sữa dừa tự nhiên, không chất bảo quản.', 100, 4, '37.jpg'),

-- Rau củ thêm
(N'Rau mồng tơi', 18000, N'Rau mồng tơi sạch, nhiều chất xơ.', 110, 1, '38.jpg'),
(N'Rau dền', 17000, N'Rau dền đỏ, bổ máu.', 100, 1, '39.jpg'),
(N'Dưa hấu', 20000, N'Dưa hấu không hạt, ngọt mát.', 80, 2, '40.jpg'),

-- Trái cây thêm
(N'Nho đen', 90000, N'Nho đen không hạt, nhập khẩu.', 50, 2, '41.jpg'),
(N'Thanh long ruột đỏ', 30000, N'Thanh long Bình Thuận.', 70, 2, '42.jpg'),
(N'Sầu riêng', 150000, N'Sầu riêng Monthong, cơm vàng.', 30, 2, '43.jpg'),

-- Ngũ cốc thêm
(N'Hạt hướng dương', 100000, N'Hạt hướng dương rang muối.', 60, 3, '44.jpg'),
(N'Hạt óc chó', 700000, N'Hạt óc chó nhập khẩu, tốt cho não.', 40, 3, '45.jpg'),
(N'Ngô sấy', 25000, N'Ngô sấy giòn, không đường.', 120, 3, '46.jpg'),

-- Sữa và sản phẩm thêm
(N'Sữa chua Hy Lạp', 35000, N'Sữa chua giàu protein.', 100, 4, '47.jpg'),
(N'Phô mai tươi', 75000, N'Phô mai tươi nhập khẩu.', 50, 4, '48.jpg'),
(N'Kem vani', 40000, N'Kem vani nguyên chất.', 60, 4, '49.jpg'),

-- Rau củ thêm
(N'Hành tím', 30000, N'Hành tím Lý Sơn.', 70, 1, '50.jpg'),
(N'Tỏi đen', 500000, N'Tỏi đen cô đơn, bổ dưỡng.', 30, 1, '51.jpg'),
(N'Củ dền', 22000, N'Củ dền đỏ, giàu sắt.', 80, 1, '52.jpg'),

-- Ngũ cốc thêm
(N'Hạt dẻ', 400000, N'Hạt dẻ Trùng Khánh.', 40, 3, '53.jpg'),
(N'Hạt đậu phộng', 15000, N'Đậu phộng rang muối.', 200, 3, '54.jpg'),

-- Sữa và sản phẩm thêm
(N'Sữa gạo lứt', 30000, N'Sữa gạo lứt tự nhiên.', 90, 4, '55.jpg'),

(N'Bơ sáp', 80000, N'Bơ sáp Đắk Lắk, thịt dày, béo ngậy.', 100, 2, '56.jpg');

go
INSERT INTO NhaCungCap (TenNCC, DiaChi, SoDienThoai, Email) VALUES 
(N'Nông trại Hòa Bình', N'123 Lê Lợi, Hà Nội', N'0912345678', N'nhcc1@example.com'),
(N'Trang trại Đà Lạt', N'456 Trần Phú, Đà Lạt', N'0938765432', N'nhcc2@example.com');



INSERT INTO KhachHang (HoTen, DiaChi, SoDienThoai, Email,MatKhau) VALUES 
(N'Nguyễn Văn A', N'12 Hoàng Diệu, Hà Nội', N'0987654321', N'nguyenvana@gmail.com','123456'),
(N'Trần Thị B', N'34 Lý Thường Kiệt, Đà Nẵng', N'0971234567', N'tranthib@gmail.com','123456');

INSERT INTO TaiKhoanAdmin (TenDangNhap, MatKhau, HoTen, Email) VALUES 
(N'admin1', N'123456', N'Trần Văn A', N'admin1@domain.com'),
(N'admin2', N'123456', N'Ngô Thị B', N'admin2@domain.com');

INSERT INTO HoaDon (MaKH, TongTien) VALUES 
(1, 100000),
(2, 150000);

go
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, Gia) VALUES 
(1, 1, 2, 20000),
(1, 2, 1, 80000),
(2, 3, 3, 50000);

INSERT INTO GioHang (MaKH, MaSP, SoLuong) VALUES 
(1, 3, 2),
(2, 4, 1);

INSERT INTO DanhGia (MaKH, MaSP, Diem, BinhLuan) VALUES 
(1, 1, 5, N'Rau cải rất tươi và ngon!'),
(2, 2, 4, N'Táo rất ngọt nhưng hơi đắt.');

INSERT INTO PhieuNhap (MaAdmin, TongTien) VALUES
(1, 3000000), -- Phiếu nhập của admin 1
(2, 4500000), -- Phiếu nhập của admin 2
(1, 1200000), -- Phiếu nhập của admin 1 lần 2
(2, 6000000); -- Phiếu nhập của admin 2 lần 2


INSERT INTO ChiTietPhieuNhap (MaPN, MaSP, SoLuong, GiaNhap) VALUES
(1, 1, 100, 30000),
(1, 2, 50, 40000),
(2, 3, 200, 85000),
(3, 4, 70, 90000),
(3, 1, 50, 30000);

INSERT INTO ThongBao (MaNguoiNhan, LoaiNguoiNhan, NoiDung) VALUES 
(1, N'KhachHang', N'Đơn hàng của bạn đang được xử lý'),
(2, N'Admin', N'Có phiếu nhập mới cần xác nhận.');
