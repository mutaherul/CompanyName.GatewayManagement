USE [GATEWAYDB]
GO
SET IDENTITY_INSERT [dbo].[Gateway] ON 

INSERT [dbo].[Gateway] ([Id], [GatewayName], [SerialNumber], [AddressIpv4], [IsDeleted], [CreatedOn], [ModifiedOn]) VALUES (1, N'dhaka.net', N'cbd20ec4-8a17-ec11-826c-cd7eff2d0157', N'20.168.30.1', 0, CAST(N'2021-09-17T13:42:11.387' AS DateTime), CAST(N'2021-09-17T13:42:11.387' AS DateTime))
INSERT [dbo].[Gateway] ([Id], [GatewayName], [SerialNumber], [AddressIpv4], [IsDeleted], [CreatedOn], [ModifiedOn]) VALUES (4, N'north.net', N'82a805a3-9717-ec11-826c-cd7eff2d0157', N'20.168.30.10', 0, CAST(N'2021-09-17T15:14:19.420' AS DateTime), CAST(N'2021-09-17T15:14:19.420' AS DateTime))
INSERT [dbo].[Gateway] ([Id], [GatewayName], [SerialNumber], [AddressIpv4], [IsDeleted], [CreatedOn], [ModifiedOn]) VALUES (5, N'sophina.net', N'7d1b8b36-7f19-ec11-826f-c2de2baa7110', N'20.100.25.10', 0, CAST(N'2021-09-20T01:24:31.823' AS DateTime), CAST(N'2021-09-20T01:24:31.823' AS DateTime))
SET IDENTITY_INSERT [dbo].[Gateway] OFF
SET IDENTITY_INSERT [dbo].[PeripheralDevice] ON 

INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1005, N'DELL', 1, 0, CAST(N'2021-09-18T17:58:48.187' AS DateTime), CAST(N'2021-09-18T17:58:48.187' AS DateTime), 1)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1007, N'Lenovo', 4, 0, CAST(N'2021-09-18T17:59:14.717' AS DateTime), CAST(N'2021-09-18T17:59:14.717' AS DateTime), 1)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1008, N'CISCO', 1, 0, CAST(N'2021-09-18T17:59:31.753' AS DateTime), CAST(N'2021-09-19T18:31:35.010' AS DateTime), 0)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1009, N'LinkSys', 1, 0, CAST(N'2021-09-18T17:59:48.093' AS DateTime), CAST(N'2021-09-19T18:24:55.617' AS DateTime), 0)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1011, N'Lenovo', 4, 0, CAST(N'2021-09-19T21:19:02.843' AS DateTime), CAST(N'2021-09-19T21:19:02.843' AS DateTime), 1)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1012, N'Apple', 4, 0, CAST(N'2021-09-19T21:20:28.957' AS DateTime), CAST(N'2021-09-19T21:20:28.957' AS DateTime), 2)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1013, N'Hp', 4, 0, CAST(N'2021-09-19T21:21:37.207' AS DateTime), CAST(N'2021-09-19T21:21:37.207' AS DateTime), 0)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1014, N'Microsoft', 4, 0, CAST(N'2021-09-20T00:05:07.027' AS DateTime), CAST(N'2021-09-20T00:05:07.027' AS DateTime), 1)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1015, N'Acer', 4, 0, CAST(N'2021-09-20T00:17:37.900' AS DateTime), CAST(N'2021-09-20T00:17:37.900' AS DateTime), 1)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1016, N'Siemens', 4, 0, CAST(N'2021-09-22T03:40:56.610' AS DateTime), CAST(N'2021-09-22T03:40:56.610' AS DateTime), 1)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1017, N'Sony', 4, 0, CAST(N'2021-09-22T03:44:11.637' AS DateTime), CAST(N'2021-09-22T03:44:11.637' AS DateTime), 0)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1018, N'TaTa', 4, 0, CAST(N'2021-09-22T03:44:48.553' AS DateTime), CAST(N'2021-09-22T03:44:48.553' AS DateTime), 0)
INSERT [dbo].[PeripheralDevice] ([UID], [VendorName], [GatewayId], [IsDeleted], [CreatedOn], [ModifiedOn], [Status]) VALUES (1019, N'Asus', 4, 0, CAST(N'2021-09-22T03:46:15.123' AS DateTime), CAST(N'2021-09-22T03:46:15.123' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[PeripheralDevice] OFF
