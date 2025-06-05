use QuanLyLichThucHanh
Go
-- viết hàm thống kê lịch thực hành hiện có nma có thêm tên môn học nữa
drop function bich_xemLTH
create function bich_xemLTH()
returns Table
as
return (select MaLich, SoBuoi, 
MaGV, MaLop, LichTh.MaMH, TenMH, 
HK, NamHoc, CONVERT(VARCHAR(10), NgayDK, 103) AS NgayDK
from LichTh join MonHoc on LichTh.MaMH = MonHoc.MaMH)
-- hàm lọc dữ liệu khi có mã giảng viên

-- thủ tục thêm lịch thực hành vào khi thực hiện đăng ký
drop proc bich_dangKyLTH
Go

CREATE PROCEDURE bich_dangKyLTH 
    @soBuoi INT,
    @ngay DATE,
    @maLop CHAR(10),
    @tenMon NVARCHAR(50),
    @hk INT,
    @nam CHAR(9),
    @maGV CHAR(10),
    @output NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @maMon CHAR(10);
    DECLARE @maLich CHAR(10);
    DECLARE @stt INT;

    -- Lấy mã môn, ép đúng kiểu char(10)
    SELECT @maMon = CAST(MaMH AS CHAR(10))
    FROM MonHoc
    WHERE TenMH = @tenMon;

    -- Kiểm tra môn học có tồn tại không
    IF @maMon IS NULL
    BEGIN
        SET @output = N'Môn học không tồn tại!';
        RETURN;
    END

    -- Đảm bảo @maMon đủ 10 ký tự (padding nếu thiếu)
    SET @maMon = LEFT(@maMon + REPLICATE(' ', 10), 10);

    -- Kiểm tra lịch thực hành đã tồn tại chưa
    IF NOT EXISTS (
        SELECT 1 FROM LichTH
        WHERE MaGV = @maGV AND MaMH = @maMon AND MaLop = @maLop
    )
    BEGIN
        -- Lấy số thứ tự mới để tạo mã lịch
        SELECT @stt = MAX(CAST(SUBSTRING(MaLich, 3, 2) AS INT)) FROM LichTH;
        SET @stt = ISNULL(@stt, 0) + 1;

        -- Tạo mã lịch với định dạng LT + số thứ tự, pad đủ 2 chữ số
        SET @maLich = 'LT' + RIGHT('00' + CAST(@stt AS VARCHAR(2)), 2);
        SET @maLich = LEFT(@maLich + REPLICATE(' ', 10), 10);  -- padding nếu cần

        -- Thêm bản ghi mới vào LichTH
        INSERT INTO LichTH (MaLich, SoBuoi, NgayDK, MaLop, MaMH, HK, NamHoc, MaGV)
        VALUES (@maLich, @soBuoi, @ngay, @maLop, @maMon, @hk, @nam, @maGV);

        SET @output = N'Đăng ký thành công';
    END
    ELSE
    BEGIN
        SET @output = N'Lịch thực hành đã tồn tại';
    END
END;

GO
declare @result nvarchar(100);
SET @result = N'';
EXECUTE bich_dangKyLTH 
    @soBuoi = 10, 
    @ngay = '2025-06-03', 
    @maLop = 'CNTT58', 
    @tenMon = N'Cơ sở dữ liệu', 
    @hk = 2, 
    @nam = '2024-2025', 
    @maGV = 'GV06', 
    @output = @result OUTPUT;
PRINT @result;
-- thủ tục cập nhật  lại lịch thực hành (Không sửa mã lịch, mã gv, mã lớp, mã môn mà chỉ hiện thị)
create procedure bich_suaLTH @maLich char(10),
@soBuoi int, @hk int, @nam char(9),
@output nvarchar(100) output
as
begin
update LichTH set SoBuoi = @soBuoi, HK = @hk, NamHoc = @nam
where LichTh.MaLich = @maLich
set @output = N'Cập nhật thành công'
end
-- viết trigger cho việc delete LichTH
create trigger xoaLTH on LichTH
instead of delete
as
begin
declare @maLich char(10)
select @maLich = MaLich from deleted
delete from PhieuTaiTruc where MaLich = @maLich
delete from LichTH where MaLich = @maLich
end
-- thủ tục xóa lịch thực hành theo mã lịch
create procedure bich_xoaLTH @maLich char(10), @output nvarchar(100) output
as
begin
delete from LichTH where MaLich = @maLich
set @output = N'Xóa lịch thực hành thành công'
end
select* from LichTH
-- thủ tục tìm kiếm lịch thực hành trên tất cả các trường thông tin
CREATE PROCEDURE bich_TimKiemLTH
    @MaLich NVARCHAR(50) = '',
    @MaGV NVARCHAR(50) = '',
    @SoBuoi NVARCHAR(50) = '',
    @MaLop NVARCHAR(50) = '',
    @MaMon NVARCHAR(50) = '',
    @TenMon NVARCHAR(100) = '',
    @HocKy NVARCHAR(10) = '',
    @NamHoc NVARCHAR(20) = '',
    @Ngay NVARCHAR(10) = '',
    @Thang NVARCHAR(10) = '',
    @Nam NVARCHAR(10) = ''
AS
BEGIN
    -- Biến cục bộ kiểu INT để chứa giá trị chuyển đổi
    DECLARE @SoBuoiInt INT = TRY_CAST(@SoBuoi AS INT);
    DECLARE @HocKyInt INT = TRY_CAST(@HocKy AS INT);
    DECLARE @NgayInt INT = TRY_CAST(@Ngay AS INT);
    DECLARE @ThangInt INT = TRY_CAST(@Thang AS INT);
    DECLARE @NamInt INT = TRY_CAST(@Nam AS INT);

    SELECT 
        L.MaLich, L.SoBuoi, L.MaGV, L.MaLop, L.MaMH, M.TenMH, L.HK, L.NamHoc, L.NgayDK
    FROM 
        LichTh L
    JOIN 
        MonHoc M ON L.MaMH = M.MaMH
    WHERE
        (@MaLich = '' OR L.MaLich LIKE '%' + @MaLich + '%') AND
        (@MaGV = '' OR L.MaGV LIKE '%' + @MaGV + '%') AND
        (@SoBuoi = '' OR L.SoBuoi = @SoBuoiInt) AND
        (@MaLop = '' OR L.MaLop LIKE '%' + @MaLop + '%') AND
        (@MaMon = '' OR L.MaMH = @MaMon) AND
        (@TenMon = '' OR M.TenMH LIKE '%' + @TenMon + '%') AND
        (@HocKy = '' OR L.HK = @HocKyInt) AND
        (@NamHoc = '' OR L.NamHoc LIKE '%' + @NamHoc + '%') AND
        (@Ngay = '' OR DAY(L.NgayDK) = @NgayInt) AND
        (@Thang = '' OR MONTH(L.NgayDK) = @ThangInt) AND
        (@Nam = '' OR YEAR(L.NgayDK) = @NamInt);
END;
GO