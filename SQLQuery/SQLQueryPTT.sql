use QuanLyLichThucHanh
--- Viết trigger ngăn không cho thêm phiếu trực với thời gian kết thúc nhỏ hơn thời gian bắt đầu hoặc thêm trùng giờ trùng nhân viên
--- ý là không cho thêm nếu trong khung giờ đó, ngày đó, phòng/ nhân viên đã được xếp lịch
select * from PhieuTaiTruc
create trigger KiemTraXepPhong on PhieuTaiTruc instead of insert
as
begin
	if exists (select 1 from inserted where GioBD > GioKT)
	begin 
		raiserror(N'Giờ bắt đầu phải nhỏ hơn giờ kết thúc', 16, 1)
		return
		--- nếu đã thực hiện 1 cái gì đó mà muốn hỷ thì nhập lệnh (rollback tran)
	end
	--- kiem tra nhân viên đã được xếp lịch
	if exists 
	(select 1 from inserted i join PhieuTaiTruc ptt on i.NgayTH = ptt.NgayTH and i.MaNV = ptt.MaNV
	and ((i.GioBD between ptt.GioBD and ptt.GioKT) 
			or (i.GioKT between ptt.GioBD and ptt.GioKT)
			or (ptt.GioBD between i.GioBD and i.GioKT)))
	begin 
		raiserror(N'Trùng nhân viên', 16, 1)
		return
	end
	--- kiểm tra trùng phòng
	if exists 
	(select 1 from inserted i join PhieuTaiTruc ptt on i.NgayTH = ptt.NgayTH and i.MaPhong = ptt.MaPhong
	and ((i.GioBD between ptt.GioBD and ptt.GioKT) 
			or (i.GioKT between ptt.GioBD and ptt.GioKT)
			or (ptt.GioBD between i.GioBD and i.GioKT)))
	begin 
		raiserror(N'Trùng phòng', 16, 1)
		return
	end
	--- nếu đã thỏa mãn
	insert into PhieuTaiTruc select * from inserted 
end
-- thủ tục thêm phiếu tải trực
drop proc bich_xepLichTruc
Go
CREATE PROCEDURE bich_xepLichTruc	
	@maLich char(10), @maNV char(10), @maPM char(10), @nd ntext,  
    @ngay DATE, @gioBD time, @gioKT time, @loaiGT nvarchar(10),
    @output NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	declare @soPhieu char(10)
    DECLARE @stt INT;

    -- Kiểm tra lịch thực hành đã tồn tại chưa
    IF NOT EXISTS (
        SELECT 1 FROM PhieuTaiTruc
        WHERE MaLich = @maLich and MaNV = @maNV and MaPhong = @maPM
    )
    BEGIN
        SELECT @stt = MAX(CAST(SoPhieu AS INT)) FROM PhieuTaiTruc
        SET @stt = ISNULL(@stt, 0) + 1;

        SET @soPhieu = CAST(@stt AS char(10));

        INSERT INTO PhieuTaiTruc(MaLich, MaNV, MaPhong, SoPhieu, NoiDungTH, NgayTH, GioBD, GioKT, LoaiGioTruc)
        VALUES (@maLich, @maNV, @maPM, @soPhieu, @nd, @ngay, @gioBD, @gioKT, @loaiGT)

        SET @output = N'Tạo thành công phiếu tải trực ' + @soPhieu;
    END
    ELSE
    BEGIN
        SET @output = N'Lịch trực đã tồn tại';
    END
END;
-- thủ tục tính lại  hệ số tải trực và số giờ khi update phiếu tải trực/ môn học
CREATE PROC tinhHeSoTT
    @ml CHAR(10),
    @mn CHAR(10),
    @mp CHAR(10)
AS
BEGIN
    DECLARE @loai NVARCHAR(10), @lhdt NVARCHAR(10), @qso INT;
    DECLARE @hstt FLOAT = 0;

    SELECT @loai = LoaiGioTruc
    FROM PhieuTaiTruc
    WHERE MaLich = @ml AND MaNV = @mn AND MaPhong = @mp;

    IF @loai = N'Giờ nghỉ'
        SET @hstt = @hstt + 0.05;

    SELECT @lhdt = LopHoc.LoaiHinhDT, @qso = LopHoc.QuanSo
    FROM LichTh
    JOIN LopHoc ON LichTh.MaLop = LopHoc.MaLop
    WHERE LichTh.MaLich = @ml;

    IF @lhdt = N'Cao học'
        SET @hstt = @hstt + 0.1;

    IF @qso <= 50
        SET @hstt = @hstt;
    ELSE IF @qso <= 60
        SET @hstt = @hstt + 0.05;
    ELSE IF @qso <= 70
        SET @hstt = @hstt + 0.10;
    ELSE
        SET @hstt = @hstt + 0.15;
	declare @soGioThuc float = 0;
	SELECT @soGioThuc = CAST(DATEDIFF(MINUTE, GioBD, GioKT) AS FLOAT) / 60
    FROM PhieuTaiTruc
    WHERE MaLich = @ml AND MaNV = @mn AND MaPhong = @mp;
	
    -- Update HeSoTaiTruc và SoGio = SoGioThucTe * (1 + HSTT)
    UPDATE PhieuTaiTruc
    SET HeSoTaiTruc = @hstt,
        SoGio = @soGioThuc * (1 + @hstt)
    WHERE MaLich = @ml AND MaNV = @mn AND MaPhong = @mp;
END;
EXEC tinhHeSoTT 
    @ml = 'LT01',
    @mn = 'NV01',
    @mp = 'PM01';
-- thủ tục cập nhật phiếu tải trực
drop proc bich_suaPTT
CREATE PROC bich_suaPTT
    @maLich CHAR(10),
    @maNV CHAR(10),
    @maPhong CHAR(10),
    @nd NTEXT,
    @ngay DATE,
    @gioBD TIME,
    @gioKT TIME,
    @loaiGT NVARCHAR(10),
	@output NVARCHAR(100) OUTPUT
AS
BEGIN
    UPDATE PhieuTaiTruc
    SET NoiDungTH = @nd,
        NgayTH = @ngay,
        GioBD = @gioBD,
        GioKT = @gioKT,
        LoaiGioTruc = @loaiGT
    WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong

    DECLARE @loai NVARCHAR(10), @lhdt NVARCHAR(10), @qso INT
    DECLARE @hstt FLOAT = 0

    SELECT @loai = LoaiGioTruc
    FROM PhieuTaiTruc
    WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong

    IF @loai = N'Giờ nghỉ'
        SET @hstt = @hstt + 0.05;

    SELECT @lhdt = LopHoc.LoaiHinhDT, @qso = LopHoc.QuanSo
    FROM LichTh
    JOIN LopHoc ON LichTh.MaLop = LopHoc.MaLop
    WHERE LichTh.MaLich = @maLich;

    IF @lhdt = N'Cao học'
        SET @hstt = @hstt + 0.1;

    IF @qso <= 50
        SET @hstt = @hstt;
    ELSE IF @qso <= 60
        SET @hstt = @hstt + 0.05;
    ELSE IF @qso <= 70
        SET @hstt = @hstt + 0.10;
    ELSE
        SET @hstt = @hstt + 0.15;

    DECLARE @soGioThuc FLOAT = 0;
    SELECT @soGioThuc = CAST(DATEDIFF(MINUTE, GioBD, GioKT) AS FLOAT) / 60
    FROM PhieuTaiTruc
    WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong

    UPDATE PhieuTaiTruc
    SET HeSoTaiTruc = @hstt,
        SoGio = @soGioThuc * (1 + @hstt)
    WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong
	SET @output = N'Cập nhật và tính hệ số thành công!'
END;

-- thủ tục xóa phiếu tải trực đi
create procedure bich_xoaPTT
@maLich char(10), @maNV char(10), @maPhong char(10),
@output nvarchar(100) output
as
begin
delete from PhieuTaiTruc
where MaLich = @maLich and
MaPhong = @maPhong and
MaNV = @maNV
set @output = N'Xóa lịch thực hành thành công'
end
select* from LichTH
VARCHAR(10)
-- thủ tục tìm kiếm phiếu tải trực
CREATE PROCEDURE bich_TimKiemPTT
    @SoPhieu VARCHAR(10) = '',
    @MaLich VARCHAR(10) = '',
    @MaNV VARCHAR(10) = '',
    @MaPhong VARCHAR(10) = '',
    @NoiDung NVARCHAR(255) = '',
    @GioBD TIME = NULL,
    @GioKT TIME = NULL,
    @Loai VARCHAR(10) = '',
    @Ngay INT = 0,
    @Thang INT = 0,
    @Nam INT = 0,
	@HS1 FLOAT = 0, @HS2 FLOAT = 0,
	@SG1 FLOAT = 0, @SG2 FLOAT = 0
AS
BEGIN
    SELECT * FROM PhieuTaiTruc
    WHERE
        (@SoPhieu = '' OR SoPhieu LIKE '%' + @SoPhieu + '%') AND
        (@MaLich = '' OR MaLich LIKE '%' + @MaLich + '%') AND
        (@MaNV = '' OR MaNV LIKE '%' + @MaNV + '%') AND
        (@MaPhong = '' OR MaPhong LIKE '%'+  @MaPhong + '%') AND
        (@NoiDung = '' OR NoiDungTH LIKE N'%' + @NoiDung + '%') AND
        (@Loai = '' OR LoaiGioTruc = @Loai) AND
        (@Ngay = 0
		OR DAY(NgayTH) = @Ngay) AND
        (@Thang = 0
		OR MONTH(NgayTH) = @Thang) AND
        (@Nam = 0
		OR YEAR(NgayTH) = @Nam) AND
		(@GioBD IS NULL AND @GioKT IS NULL)
		OR (
			GioBD < ISNULL(@GioKT, '23:59') 
			AND GioKT > ISNULL(@GioBD, '00:00')
		) 
		AND (
		(@HS1 = 0 AND @HS2 = 0)
		OR (@HS1 > 0 AND @HS2 = 0 AND HeSoTaiTruc >= @HS1)
		OR (@HS1 = 0 AND @HS2 > 0 AND HeSoTaiTruc <= @HS2)
		OR (@HS1 > 0 AND @HS2 > 0 AND HeSoTaiTruc BETWEEN @HS1 AND @HS2)
		)
		AND (
		(@SG1 = 0 AND @SG2 = 0)
		OR (@SG1 > 0 AND @SG2 = 0 AND SoGio >= @SG1)
		OR (@SG1 = 0 AND @SG2 > 0 AND SoGio <= @SG2)
		OR (@SG1 > 0 AND @SG2 > 0 AND SoGio BETWEEN @SG1 AND @SG2)
		)
END
select * from PhieuTaiTruc
-- cập nhật tất cả
CREATE PROC CapNhatTatCaHeSoTaiTruc
AS
BEGIN
    -- Duyệt toàn bộ các phiếu trong PhieuTaiTruc
    DECLARE @maLich CHAR(10), @maNV CHAR(10), @maPhong CHAR(10);
    DECLARE @loai NVARCHAR(10), @lhdt NVARCHAR(10), @qso INT;
    DECLARE @hstt FLOAT, @soGioThuc FLOAT;

    -- Con trỏ để duyệt tất cả các phiếu
    DECLARE cur CURSOR FOR
    SELECT MaLich, MaNV, MaPhong
    FROM PhieuTaiTruc;

    OPEN cur;
    FETCH NEXT FROM cur INTO @maLich, @maNV, @maPhong;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Reset hệ số
        SET @hstt = 0;

        -- Lấy loại giờ trực
        SELECT @loai = LoaiGioTruc
        FROM PhieuTaiTruc
        WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong;

        IF @loai = N'Giờ nghỉ'
            SET @hstt = @hstt + 0.05;

        -- Lấy LoaiHinhDT và QuanSo
        SELECT @lhdt = LopHoc.LoaiHinhDT, @qso = LopHoc.QuanSo
        FROM LichTh
        JOIN LopHoc ON LichTh.MaLop = LopHoc.MaLop
        WHERE LichTh.MaLich = @maLich;

        IF @lhdt = N'Cao học'
            SET @hstt = @hstt + 0.1;

        IF @qso <= 50
            SET @hstt = @hstt;
        ELSE IF @qso <= 60
            SET @hstt = @hstt + 0.05;
        ELSE IF @qso <= 70
            SET @hstt = @hstt + 0.1;
        ELSE
            SET @hstt = @hstt + 0.15;

        -- Tính số giờ thực tế
        SELECT @soGioThuc = CAST(DATEDIFF(MINUTE, GioBD, GioKT) AS FLOAT) / 60
        FROM PhieuTaiTruc
        WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong;

        -- Cập nhật lại hệ số và số giờ
        UPDATE PhieuTaiTruc
        SET HeSoTaiTruc = @hstt,
            SoGio = @soGioThuc * (1 + @hstt)
        WHERE MaLich = @maLich AND MaNV = @maNV AND MaPhong = @maPhong;

        FETCH NEXT FROM cur INTO @maLich, @maNV, @maPhong;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;
exec CapNhatTatCaHeSoTaiTruc
select * from PhieuTaiTruc