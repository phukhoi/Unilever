USE [Unilever]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Bột giặt OMO')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Dầu gội đầu ')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Kem')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (4, N'Kem dưỡng da')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (5, N'Xà bông Lifebuoy')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Distributors] ON 

INSERT [dbo].[Distributors] ([ID], [Name], [Email], [Phone], [Addr]) VALUES (1, N'VẠN THỊNH PHÁT - CTY TNHH MTV TM VẠN THỊNH PHÁT', N'VTP@gmail.com', N'(08)39675812   ', N'279 TRẦN VĂN KIỂU, P.3, Q.6, TP. HCM')
INSERT [dbo].[Distributors] ([ID], [Name], [Email], [Phone], [Addr]) VALUES (2, N'UNILEVER VIỆT NAM - CTY LD UNILEVER VIỆT NAM', N'Unilerver@gmail.com', N'(08)54135600   ', N'156 NGUYỄN LƯƠNG BẰNG, P.TÂN PHÚ, Q.7, TP. HCM')
INSERT [dbo].[Distributors] ([ID], [Name], [Email], [Phone], [Addr]) VALUES (3, N'KIM HUÊ - ĐẠI LÝ BỘT GIẶT OMO KIM HUÊ', N'KimHue@gmail.com', N'(0650)3824260  ', N'137 BÁC SĨ YERSIN, P.PHÚ CƯỜNG, TX.THỦ DẦU MỘT, BÌNH DƯƠNG')
go
INSERT [dbo].[Products] ([ID], [Name], [CatID], [Price], [ImportDate], [RemainingAmount], [Descript]) VALUES (1, N'Bột giặt OMO Comfort Hương Ban Mai', 1, 119000, CAST(0x183A0B00 AS Date), 100, N'Bột giặt OMO Comfort tinh dầu thơm mới kết hợp sức mạnh xoáy bay vết bẩn cứng đầu nhanh hơn và ngát hương thơm Comfort Hương ban mai

Hạt Năng Lượng Xoáy hòa tan, thấm sâu thật nhanh vào sợi vải, giúp xoáy bay các bết bẩn cứng đầu chỉ trong 1 lần giặt.
Hương thơm ban mai của Comfort giúp quần áo thơm mát dài lâu.
Sản phẩm có các loại trọng lượng: 360g, 720g, 2.7kg, 4.1kg, 5.5kg.')
INSERT [dbo].[Products] ([ID], [Name], [CatID], [Price], [ImportDate], [RemainingAmount], [Descript]) VALUES (2, N'Bột giặt OMO Comfort Tinh Dầu Thơm', 1, 118000, CAST(0x183A0B00 AS Date), 100, N'OMO Comfort tinh dầu thơm mới kết hợp sức mạnh xoáy bay vết bẩn cứng đầu nhanh hơn và ngát hương thơm Comfort tinh dầu thơm tinh tế

Hạt Năng Lượng Xoáy hòa tan, thấm sâu thật nhanh vào sợi vải, giúp xoáy bay các bết bẩn cứng đầu chỉ trong 1 lần giặt.
Hương tinh dầu thơm của Comfort Tinh dầu thơm tinh tế giúp quần áo thơm mát dài lâu.
Sản phẩm có các loại trọng lượng: 360g, 720g, 2.7kg, 4.1kg, 5.5kg.')
SET IDENTITY_INSERT [dbo].[Products] OFF
go
SET IDENTITY_INSERT [dbo].[InterestRate] ON 

INSERT [dbo].[InterestRate] ([ID], [OutOfPeriod], [InterestPayable]) VALUES (1, 7, 2.5)
INSERT [dbo].[InterestRate] ([ID], [OutOfPeriod], [InterestPayable]) VALUES (2, 14, 5)
INSERT [dbo].[InterestRate] ([ID], [OutOfPeriod], [InterestPayable]) VALUES (3, 21, 7.5)
INSERT [dbo].[InterestRate] ([ID], [OutOfPeriod], [InterestPayable]) VALUES (4, 30, 15)
SET IDENTITY_INSERT [dbo].[InterestRate] OFF
go
insert into Orders(OrderDate, DistributorID, OrderType) values
(GETDATE(),1,1),
(GETDATE(),2,2),
(GETDATE(),1,2),
(GETDATE(),3,1),
(GETDATE(),2,1),
(GETDATE(),2,1),
(GETDATE(),3,2),
(GETDATE(),2,2),
(GETDATE(),1,1),
(GETDATE(),3,1),
(GETDATE(),1,1)
go
insert into DefferredLiabilities(DebtDate, OrderID, PeriodOfDebt) values
('6/21/2015', 1, 14)
go
insert into DefferredLiabilities(DebtDate, OrderID, PeriodOfDebt) values
(GETDATE(), 1, 14),
(GETDATE(), 1, 11),
(GETDATE(), 1, 8),
('6/22/2015', 1, 5),
(GETDATE(), 1, 2),
('6/28/2015', 1, 13),
('7/29/2015', 1, 17),
('6/30/2015', 1, 15),
('6/26/2015', 1, 9),
('6/16/2015', 1, 24),
(GETDATE(), 1, 7),
(GETDATE(), 1, 3),
(GETDATE(), 1, 12)
go
insert into DefferredLiabilities(DebtDate, OrderID, PeriodOfDebt) values
(GETDATE(), 2, 14),
(GETDATE(), 4, 11),
(GETDATE(), 2, 8),
('6/22/2015', 3, 5),
(GETDATE(), 1, 2),
('6/28/2015', 5, 13),
('7/29/2015', 8, 17),
('6/30/2015', 4, 15),
('6/26/2015', 7, 9),
('6/16/2015', 7, 24),
(GETDATE(), 6, 7),
(GETDATE(), 2, 3),
(GETDATE(), 3, 12)
go
insert into ordertype values
(1,N'Thường kỳ'),
(2,N'Không thường kỳ')
go

