use UnileverDMS_Distributors
go

SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Bột giặt OMO')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Dầu gội đầu ')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Kem')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (4, N'Kem dưỡng da')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (5, N'Xà bông Lifebuoy')
go

INSERT [dbo].[Products] ([ID], [Name], [CatID], [Price], [ImportDate], [RemainingAmount], [Descript]) VALUES (1, N'Bột giặt OMO Comfort Hương Ban Mai', 4, 119000, CAST(0x183A0B00 AS Date), 100, N'Bột giặt OMO Comfort tinh dầu thơm mới kết hợp sức mạnh xoáy bay vết bẩn cứng đầu nhanh hơn và ngát hương thơm Comfort Hương ban mai

Hạt Năng Lượng Xoáy hòa tan, thấm sâu thật nhanh vào sợi vải, giúp xoáy bay các bết bẩn cứng đầu chỉ trong 1 lần giặt.
Hương thơm ban mai của Comfort giúp quần áo thơm mát dài lâu.
Sản phẩm có các loại trọng lượng: 360g, 720g, 2.7kg, 4.1kg, 5.5kg.')
INSERT [dbo].[Products] ([ID], [Name], [CatID], [Price], [ImportDate], [RemainingAmount], [Descript]) VALUES (2, N'Bột giặt OMO Comfort Tinh Dầu Thơm', 3, 118000, CAST(0x183A0B00 AS Date), 100, N'OMO Comfort tinh dầu thơm mới kết hợp sức mạnh xoáy bay vết bẩn cứng đầu nhanh hơn và ngát hương thơm Comfort tinh dầu thơm tinh tế

Hạt Năng Lượng Xoáy hòa tan, thấm sâu thật nhanh vào sợi vải, giúp xoáy bay các bết bẩn cứng đầu chỉ trong 1 lần giặt.
Hương tinh dầu thơm của Comfort Tinh dầu thơm tinh tế giúp quần áo thơm mát dài lâu.
Sản phẩm có các loại trọng lượng: 360g, 720g, 2.7kg, 4.1kg, 5.5kg.')
INSERT [dbo].[Products] ([ID], [Name], [CatID], [Price], [ImportDate], [RemainingAmount], [Descript]) VALUES (1003, N'Bột giặt OMO Comfort Hương Ban Mai', 4, 119000, CAST(0xA1390B00 AS Date), 100, N'Bột giặt OMO Comfort tinh dầu thơm mới kết hợp sức mạnh xoáy bay vết bẩn cứng đầu nhanh hơn và ngát hương thơm Comfort Hương ban mai

Hạt Năng Lượng Xoáy hòa tan, thấm sâu thật nhanh vào sợi vải, giúp xoáy bay các bết bẩn cứng đầu chỉ trong 1 lần giặt.
Hương thơm ban mai của Comfort giúp quần áo thơm mát dài lâu.
Sản phẩm có các loại trọng lượng: 360g, 720g, 2.7kg, 4.1kg, 5.5kg.')
go
go
SET IDENTITY_INSERT [dbo].[Products] OFF