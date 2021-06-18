CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
GO

--=======================================
CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Empty'	
)
GO
CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: 
)
GO
CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO
CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO
CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0 
	
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO
CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO
--=======================================

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN 
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood 
GO

CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	insert dbo.Bill (DateCheckIn, DateCheckOut, idTable, status, discount) values (GETDATE(), null, @idTable, 0, 0)
END
GO

CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	declare @isExitsBillInfo INT
	declare @foodCount INT = 1

	select @isExitsBillInfo = id, @foodCount = b.count
	from dbo.BillInfo AS b
	where idBill = @idBill and idFood = @idFood

	if (@isExitsBillInfo > 0)
	begin
		declare @newCount INT = @foodCount + @count
		if (@newCount > 0)
			update dbo.BillInfo set count = @foodCount + @count where idFood = @idFood
		else
			delete dbo.BillInfo where idBill = @idBill and idFood = @idFood
	end
	else
	begin
		insert dbo.BillInfo (idBill, idFood, count) values (@idBill, @idFood, @count)
	end
END
GO

create trigger UTG_updatebillinfo
on dbo.BillInfo for insert, update
as
begin
	declare @idBill int
	select @idBill = idBill from inserted
	
	declare @idTable int
	select @idTable = idTable from dbo.Bill where id = @idBill and status = 0

	update dbo.TableFood set status = N'Have people' where id = @idTable
end
go
create trigger UTG_updatebill
on dbo.Bill for update
as
begin
	declare @idBill int
	select @idBill = id from inserted

	declare @idTable int
	select @idTable = idTable from dbo.Bill where id = @idBill

	declare @count int = 0
	select @count = count(*) from dbo.Bill where idTable = @idTable and status = 0

	if (@count = 0)
		update dbo.TableFood set status = N'Empty' where id = @idTable

end
go

create proc USP_ListBillDate
@checkIn date, @checkOut date
as
begin
	select t.name, b.totalPrice, DateCheckIn, DateCheckOut, discount 
	from dbo.Bill as b, dbo.TableFood as t
	where DateCheckIn >= @checkIn and DateCheckOut <= @checkOut and b.status = 1 
		and t.id = b.idTable 
end
go

create proc USP_updateAccount
@userName nvarchar(100), @displayName nvarchar(100), @password nvarchar(100), @newPassword nvarchar(100)
as
begin
	declare @isRight int

	select @isRight = count(*) from dbo.Account where USERname = @userName and PassWord = @password

	if (@isRight = 1)
	begin
		if (@newPassword = null and @newPassword = '')
		begin
			update dbo.Account set DisplayName = @displayName where UserName = @userName
		end
		else
			update dbo.Account set DisplayName = @displayName, PassWord = @newPassword where UserName = @userName
	end
end
go

create trigger UTG_deleteBillInfo
on dbo.BillInfo for delete
as
begin
	declare @idBI int
	declare @idBill int
	select @idBI = id, @idBill = deleted.id from deleted

	declare @idTable int
	select @idTable = idTable from dbo.Bill where id = @idBill
	
	declare @count int = 0
	select @count = count(*) from dbo.BillInfo as bi, dbo.Bill as b where b.id = bi.idBill and b.id = @idBill and b.status = 0

	if (@count = 0)
		update dbo.TableFood set status = N'Empty' where id = @idTable
end
go

CREATE PROC USP_getfoodlist
AS SELECT * FROM dbo.Food
GO

select * from dbo.Account
select * from dbo.Bill
select * from dbo.TableFood

select * from dbo.BillInfo
select * from dbo.Food
select * from dbo.FoodCategory

delete BillInfo
delete Bill


select MAX(id) from dbo.Bill