USE [GHOST]
GO

/****** Object:  Table [dbo].[SrvPr_PARAMETERS]    Script Date: 10.06.2015 15:55:02 ******/

if object_id('dbo.SrvPr_PARAMETERS') is not null
	drop table [dbo].[SrvPr_PARAMETERS]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SrvPr_PARAMETERS](
	[SERIALKEY] int unique identity(1,1),
	[Name] [varchar](50) NOT NULL,
	[Value] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[Type] [varchar](50) NOT NULL CONSTRAINT [DF_SrvPr_PARAMETERS_Type]  DEFAULT ('0'),
	[ADDDATE] datetime default GetDate() NOT NULL,
	[ADDWHO] [varchar](200) default User_Name() NOT NULL,
	[EDITDATE] datetime default GetDate() NOT NULL,
	[EDITWHO] [varchar](200) default User_Name() NOT NULL,
) ON [PRIMARY]

GO


SET ANSI_PADDING OFF
GO
GRANT SELECT ON [dbo].[SrvPr_PARAMETERS] TO PUBLIC
INSERT INTO [dbo].[SrvPr_PARAMETERS]
([Name],[Value],[Description],[Type])
SELECT 'PRINTSCALE',	'0.98',	'Масштаб при печати',	'0'
union
SELECT 'SERVER_NAME',	'SSRS02',	'Путь к серверу отчетов',	'0'
