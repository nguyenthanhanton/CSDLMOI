USE QuanLyLichThucHanh
GO

--Procedure cho MonHoc
--1. Procedure insert monHoc
CREATE PROC Quang_InsertMonHoc (
	@MaMH CHAR(10),
	@TenMH NVARCHAR(50),
	@SoTinChi INT,
	@SoTiet INT
) AS INSERT INTO MonHoc(MaMH, TenMH, SoTC, SoTiet) 
VALUES (@MaMH, @TenMH, @SoTinChi, @SoTiet)

--2. Procedure updateMonHoc
CREATE PROC Quang_UpdateMonHoc (
	@MaMH CHAR(10),
	@TenMH NVARCHAR(50),
	@SoTinChi INT,
	@SoTiet INT
) AS BEGIN
	UPDATE MonHoc
	SET TenMH = @TenMH, SoTC = @SoTinChi, SoTiet = @SoTiet
	WHERE MaMH = @MaMH
END

--3. Procedure deleteMonHoc
CREATE PROC Quang_DeleteMonHoc (
	@MaMH CHAR(10)
) AS BEGIN
	DELETE FROM MonHoc
	WHERE MaMH = @MaMH
END

--4. Procedure buscarMonHoc
CREATE PROC Quang_TimKiemMonHoc (
	@MaMH CHAR(10)
) AS SELECT * FROM MonHoc
WHERE MaMH = @MaMH

--5. Procedure danhSach
CREATE PROC Quang_DanhSachMonHoc
AS SELECT * FROM MonHoc

EXEC Quang_InsertMonHoc 'MH100', 'DATABASE FUNDAMENTALS', 3, 70
EXEC Quang_UpdateMonHoc 'MH100', 'Database Fundamentals', 3, 60
EXEC Quang_TimKiemMonHoc 'MH100'
EXEC Quang_DeleteMonHoc 'MH100'
EXEC Quang_DanhSachMonHoc

--Procedure cho LopHoc
--1. Procedure danhSachLH
CREATE PROC Quang_DanhSachLopHoc
AS SELECT * FROM LopHoc

--2. Procedure insertLH
CREATE PROC Quang_InsertLopHoc (
	@MaLop CHAR(10),
	@TenLop NVARCHAR(50),
	@QuanSo INT,
	@KhoaHoc INT,
	@LoaiHinhDT NVARCHAR(10) 
) AS BEGIN
	IF @LoaiHinhDT NOT IN (N'Đại học', N'Cao học')
	BEGIN
		PRINT 'Err: Must be "Đại học" or "Cao học"'
		RETURN
	END
	INSERT INTO LopHoc(MaLop, TenLop, QuanSo, KhoaHoc, LoaiHinhDT)
	VALUES (@MaLop, @TenLop, @QuanSo, @KhoaHoc, @LoaiHinhDT)
END

--3. Procedure updateLH
CREATE PROC Quang_UpdateLopHoc (
	@MaLop CHAR(10),
	@TenLop NVARCHAR(50),
	@QuanSo INT,
	@KhoaHoc INT,
	@LoaiHinhDT NVARCHAR(10) 
) AS BEGIN
	IF @LoaiHinhDT NOT IN (N'Đại học', N'Cao học')
	BEGIN
		PRINT 'Err: Must be "Đại học" or "Cao học"'
		RETURN
	END
	UPDATE LopHoc
	SET TenLop = @TenLop, QuanSo = @QuanSo, KhoaHoc = @KhoaHoc, LoaiHinhDT = @LoaiHinhDT
	WHERE MaLop = @MaLop
END

--4. Trigger insert + updateLH
CREATE TRIGGER Quang_UpdateHeSoTaiTruc ON LopHoc AFTER INSERT, UPDATE
AS 
BEGIN
	--DECLARE @QuanSo INT, @LoaiHinhDT NVARCHAR(10)
	--SELECT @QuanSo = QuanSo, @LoaiHinhDT = LoaiHinhDT FROM inserted
	UPDATE PhieuTaiTruc
	SET HeSoTaiTruc =
		CASE 
			WHEN i.LoaiHinhDT = N'Cao học' THEN 0.1
			ELSE 0
		END
		+
		CASE 
			WHEN i.QuanSo <= 50 THEN 0
			WHEN i.QuanSo <= 60 THEN 0.05
			WHEN i.QuanSo <= 70 THEN 0.1
			ELSE 0.15
		END
		+
		CASE 
			WHEN p.LoaiGioTruc = N'Giờ nghỉ' THEN 0.05
			ELSE 0
		END
	FROM inserted i JOIN LichTH l ON i.MaLop = l.MaLop JOIN PhieuTaiTruc p ON l.MaLich = p.MaLich
END

--5. Procedure deleteLH
CREATE PROC Quang_DeleteLopHoc (
	@MaLop CHAR(10)
) AS BEGIN
	DELETE FROM LopHoc
	WHERE MaLop = @MaLop
END

--6. Trigger deleteLichTH via LopHoc
ALTER TRIGGER Quang_DeleteCorrespondingLichTH ON LopHoc INSTEAD OF DELETE
AS DECLARE @MaLop CHAR(10), @MaLich CHAR(10)
BEGIN 
	SELECT @MaLop = deleted.MaLop, @MaLich = MaLich FROM deleted, LichTh
	DELETE PhieuTaiTruc WHERE MaLich = @MaLich
	DELETE LichTH WHERE MaLop = @MaLop
	DELETE LopHoc WHERE MaLop = @MaLop
END

--7. Procedure buscarLH
CREATE PROC Quang_TimKiemLopHoc (
	@MaLop CHAR(10)
) AS BEGIN
	SELECT * FROM LopHoc
	WHERE MaLop = @MaLop
END


EXEC Quang_DanhSachLopHoc
EXEC Quang_DeleteLopHoc 'CNTT60'
EXEC Quang_InsertLopHoc 'CNTT60', N'Công nghệ thông tin', 16, 60, N'Đại học'
EXEC Quang_UpdateLopHoc 'CNTT60', N'Công nghệ thông tin', 50, 75, N'Cao học'
EXEC Quang_TimKiemLopHoc 'CNTT60'