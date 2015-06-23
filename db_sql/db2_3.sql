use master 
go
create database UnileverDMS_Distributors
go
use UnileverDMS_Distributors
go
create table SaleMans
(
	ID int identity primary key,
	Name nvarchar(100),
	Addr nvarchar(300),
	Email nvarchar(300),
	Phone nvarchar(15),
	SaleAreaId int
)	
go
create table SaleAreas
(
	ID int identity primary key,
	Area nvarchar(200)
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
	SaleManId int
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
create table DefferredLiabilities  -- công nợ
(
	ID int identity primary key,
	OrderId int unique,
	SaleManId int,
	DebtDate date,
	unique (OrderId, SaleManId)
)
go
create table SaleRevenues -- doanh số
(
	ID int identity primary key,
	SaleManId int,
	ProId int,
	SoldQuantity int,
	TotalCash int,
	StatisDate date
)
go
use master
go
create database UnileverDMS_Customers
go
use UnileverDMS_Customers
go
create table Customers
(
	ID int identity primary key,
	Name nvarchar(100),
	Addr nvarchar(300),
	Email nvarchar(300),
	Phone nvarchar(15),
	CustomerTypeId int
)
go
create table CustomerType
(
	ID int primary key,
	Details nvarchar(100),
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
	CustomerId int
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
create table DefferredLiabilities  -- công nợ
(
	ID int identity primary key,
	OrderId int unique,
	CustomerId int,
	DebtDate date,
	unique (OrderId, CustomerId)
)
go
create table SaleAreas
(
	ID int identity primary key,
	Area nvarchar(200)
)
go
create table SaleRevenues -- doanh số
(
	ID int identity primary key,
	SaleAreaId int,
	ProId int,
	SoldQuantity int,
	TotalCash int,
	StatisDate date
)
