USE [GATEWAYDB]
GO
ALTER TABLE [dbo].[PeripheralDevice] DROP CONSTRAINT [FK_PeripheralDevice_Gateway]
GO
ALTER TABLE [dbo].[PeripheralDevice] DROP CONSTRAINT [DF__Periphera__Statu__5CD6CB2B]
GO
ALTER TABLE [dbo].[PeripheralDevice] DROP CONSTRAINT [DF__Periphera__Modif__4CA06362]
GO
ALTER TABLE [dbo].[PeripheralDevice] DROP CONSTRAINT [DF__Periphera__Creat__4BAC3F29]
GO
ALTER TABLE [dbo].[PeripheralDevice] DROP CONSTRAINT [DF__Periphera__IsDel__4AB81AF0]
GO
ALTER TABLE [dbo].[Gateway] DROP CONSTRAINT [DF__Gateway__Modifie__403A8C7D]
GO
ALTER TABLE [dbo].[Gateway] DROP CONSTRAINT [DF__Gateway__Created__3F466844]
GO
ALTER TABLE [dbo].[Gateway] DROP CONSTRAINT [DF__Gateway__IsDelet__3E52440B]
GO
ALTER TABLE [dbo].[Gateway] DROP CONSTRAINT [DF__Gateway__SerialN__3D5E1FD2]
GO
/****** Object:  Table [dbo].[PeripheralDevice]    Script Date: 9/22/2021 12:16:06 PM ******/
DROP TABLE [dbo].[PeripheralDevice]
GO
/****** Object:  Table [dbo].[Gateway]    Script Date: 9/22/2021 12:16:06 PM ******/
DROP TABLE [dbo].[Gateway]
GO
/****** Object:  User [gatewaydb_dba]    Script Date: 9/22/2021 12:16:06 PM ******/
DROP USER [gatewaydb_dba]
GO
/****** Object:  User [gatewaydb_dba]    Script Date: 9/22/2021 12:16:06 PM ******/
CREATE USER [gatewaydb_dba] FOR LOGIN [gatewaydb_dba] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [gatewaydb_dba]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [gatewaydb_dba]
GO
ALTER ROLE [db_datareader] ADD MEMBER [gatewaydb_dba]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [gatewaydb_dba]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [gatewaydb_dba]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [gatewaydb_dba]
GO
/****** Object:  Table [dbo].[Gateway]    Script Date: 9/22/2021 12:16:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gateway](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GatewayName] [nvarchar](80) NOT NULL,
	[SerialNumber] [uniqueidentifier] NOT NULL,
	[AddressIpv4] [nvarchar](15) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_Gateway] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeripheralDevice]    Script Date: 9/22/2021 12:16:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeripheralDevice](
	[UID] [bigint] IDENTITY(1000,1) NOT NULL,
	[VendorName] [nvarchar](250) NOT NULL,
	[GatewayId] [bigint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_PeripheralDevice] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Gateway] ADD  DEFAULT (newsequentialid()) FOR [SerialNumber]
GO
ALTER TABLE [dbo].[Gateway] ADD  DEFAULT ('FALSE') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Gateway] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Gateway] ADD  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[PeripheralDevice] ADD  DEFAULT ('FALSE') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PeripheralDevice] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[PeripheralDevice] ADD  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[PeripheralDevice] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[PeripheralDevice]  WITH CHECK ADD  CONSTRAINT [FK_PeripheralDevice_Gateway] FOREIGN KEY([GatewayId])
REFERENCES [dbo].[Gateway] ([Id])
GO
ALTER TABLE [dbo].[PeripheralDevice] CHECK CONSTRAINT [FK_PeripheralDevice_Gateway]
GO
