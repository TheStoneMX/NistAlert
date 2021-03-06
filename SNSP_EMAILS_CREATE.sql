USE [Biometrics]
GO
/****** Object:  Table [dbo].[SNSP_EMAILS]    Script Date: 09/01/2008 16:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SNSP_EMAILS](
	[EmailDI] [int] IDENTITY(1,1) NOT NULL,
	[DE] [varchar](50) NULL,
	[PARA] [varchar](50) NULL,
	[Subject] [varchar](50) NULL,
	[Date] [datetime] NULL,
	[Attachment] [image] NULL,
	[AttachmentName] [varchar](50) NULL,
	[TOT] [varchar](50) NULL,
	[NCP] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[rawEmail] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF