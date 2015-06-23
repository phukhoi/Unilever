use master 
go
drop database Unilever
go
create database Unilever
go
use Unilever
go
create table Distributors
(
	ID int identity primary key,
	Name nvarchar(100),
	Email nvarchar(300),
	Phone char(15),
	[Addr] nvarchar(500)
)
go
create table Products
(
	ID int identity primary key,
	Name nvarchar(100),
	CatID int,
	Price int, 
	ImportDate date, -- ngày nhập
	RemainingAmount int, -- tồn kho
	Descript nvarchar(2000)
)
go
create table Categories
 (
	ID int identity primary key,
	Name nvarchar(150)
)
go
create table Orders
(
	ID int identity primary key,
	OrderDate datetime,
	DistributorID int,
	OrderTypeId int -- loại hóa đơn: định kỳ - không định kỳ
)
go
create table OrderDetails
(
	ID int identity primary key,
	ProID int, 
	OrderID int,
	Quantity int,
	Price int,
	TotalAmount int -- = quantity * price
)
go
create table DefferredLiabilities -- nợ
(
	ID int identity primary key,
	OrderID int unique,
	DebtDate date, -- ngày bắt đầu nợ
	PeriodOfDebt int -- thời gian được phép nợ (đơn vị: ngày)
)
go
create table InterestRate -- lãi suất
(
	ID int identity primary key,
	OutOfPeriod int, -- quá hạn (đơn vị: ngày)
	InterestPayable float -- lãi phải trả (đơn vị: % trên tổng giá trị đơn hàng)
)
go
create table Inventories -- hàng tồn kho
(
	ID int identity primary key,
	DistributorID int, -- nhà phân phối
	ProID int,
	OrderedQuantity int,
	SoldQuantity int,
	OrderDate date -- ngày nhập hàng
)
go
create table OrderType
(
	ID int primary key,
	OrderType nvarchar(100)
)
go
create table SaleRevenues -- doanh số bán hàng
(
	ID int identity primary key,
	DistributorID int,
	ProId int unique,
	SoldQuantity int,
	TotalCash int, -- tồng tiền 
	StatisDate date -- ngày thống kê
)
go
alter table inventories add constraint uq_distrib_pro unique (DistributorID, ProID)