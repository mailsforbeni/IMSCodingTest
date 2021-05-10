USE [InventoryManagementSystem]
GO
/****** Object:  Table [dbo].[inventory]    Script Date: 10-05-2021 11:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inventory](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
	[SupplierName] [nvarchar](max) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK__inventor__3213E83FE085FB08] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[inventory] ON 
GO
INSERT [dbo].[inventory] ([Id], [Name], [Description], [Price], [SupplierName], [AddedOn], [UpdatedOn]) VALUES (1001, N'iPhone', N'10x Max', 155000, N'Apple', CAST(N'2021-05-09T21:59:15.503' AS DateTime), CAST(N'2021-05-09T21:59:15.503' AS DateTime))
GO
INSERT [dbo].[inventory] ([Id], [Name], [Description], [Price], [SupplierName], [AddedOn], [UpdatedOn]) VALUES (1002, N'SmartWatch', N'SmartWatch Multiple', 1234.33, N'Zebronics', CAST(N'2021-05-10T09:53:02.517' AS DateTime), CAST(N'2021-05-10T09:54:04.377' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[inventory] OFF
GO
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_UpdatedOn]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
