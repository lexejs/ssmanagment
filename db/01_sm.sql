USE [SM]
GO
/****** Object:  Table [dbo].[group]    Script Date: 02/11/2009 18:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[group](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) COLLATE Cyrillic_General_CI_AS NULL,
	[parent] [int] NULL,
 CONSTRAINT [PK_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[group]  WITH CHECK ADD  CONSTRAINT [FK_group_group] FOREIGN KEY([parent])
REFERENCES [dbo].[group] ([id])
GO
ALTER TABLE [dbo].[group] CHECK CONSTRAINT [FK_group_group]

USE [SM]
GO
/****** Object:  Table [dbo].[item]    Script Date: 02/11/2009 18:29:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](500) COLLATE Cyrillic_General_CI_AS NULL,
	[groupId] [int] NULL,
	[count] [int] NULL,
	[measure] [varchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[price] [float] NULL,
	[pct] [float] NULL,
	[adminPrice] [float] NULL,
	[reserveCount] [int] NULL,
	[reserveEndDate] [datetime] NULL,
	[order] [bit] NULL,
	[countToOrder] [int] NULL,
	[canGiveBack] [bit] NULL,
 CONSTRAINT [PK_item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[item]  WITH CHECK ADD  CONSTRAINT [FK_item_group] FOREIGN KEY([groupId])
REFERENCES [dbo].[group] ([id])
GO
ALTER TABLE [dbo].[item] CHECK CONSTRAINT [FK_item_group]

USE [SM]
GO
/****** Object:  Table [dbo].[seller]    Script Date: 02/11/2009 18:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[seller](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [varchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[isAdmin] [bit] NULL,
	[login] [varchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[password] [varchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_seller] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [SM]
GO
/****** Object:  Table [dbo].[buyer]    Script Date: 02/11/2009 18:30:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[buyer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[pct] [float] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_buyer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [SM]
GO
/****** Object:  Table [dbo].[logActivity]    Script Date: 02/11/2009 18:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[logActivity](
	[id] [int] NOT NULL,
	[action] [varchar](150) COLLATE Cyrillic_General_CI_AS NULL,
	[date] [datetime] NULL CONSTRAINT [DF_logActivity_date]  DEFAULT (getdate()),
	[buyerId] [int] NULL,
	[informAdmin] [bit] NULL,
 CONSTRAINT [PK_logActivity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[logActivity]  WITH CHECK ADD  CONSTRAINT [FK_logActivity_buyer] FOREIGN KEY([buyerId])
REFERENCES [dbo].[buyer] ([id])
GO
ALTER TABLE [dbo].[logActivity] CHECK CONSTRAINT [FK_logActivity_buyer]

USE [SM]
GO
/****** Object:  Table [dbo].[logSales]    Script Date: 02/11/2009 18:31:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logSales](
	[id] [int] NOT NULL,
	[itemId] [int] NULL,
	[itemsCount] [int] NULL,
	[buyerId] [int] NULL,
	[sellerId] [int] NULL,
	[date] [datetime] NULL CONSTRAINT [DF_logSales_date]  DEFAULT (getdate()),
	[isGiveBack] [bit] NULL,
 CONSTRAINT [PK_logSales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[logSales]  WITH CHECK ADD  CONSTRAINT [FK_logSales_buyer] FOREIGN KEY([buyerId])
REFERENCES [dbo].[buyer] ([id])
GO
ALTER TABLE [dbo].[logSales] CHECK CONSTRAINT [FK_logSales_buyer]
GO
ALTER TABLE [dbo].[logSales]  WITH CHECK ADD  CONSTRAINT [FK_logSales_item] FOREIGN KEY([itemId])
REFERENCES [dbo].[item] ([id])
GO
ALTER TABLE [dbo].[logSales] CHECK CONSTRAINT [FK_logSales_item]
GO
ALTER TABLE [dbo].[logSales]  WITH CHECK ADD  CONSTRAINT [FK_logSales_seller] FOREIGN KEY([sellerId])
REFERENCES [dbo].[seller] ([id])
GO
ALTER TABLE [dbo].[logSales] CHECK CONSTRAINT [FK_logSales_seller]