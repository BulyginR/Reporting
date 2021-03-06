USE GHOST
ALTER TABLE [dbo].[SrvPr_REPORTS]
ADD
	[ADDWHO] [varchar](100) NULL CONSTRAINT [DF_SrvPr_REPORTS_ADDWHO]  DEFAULT (USER_NAME()),
	[ADDDATE] [datetime] NULL CONSTRAINT [DF_SrvPr_REPORTS_ADDDATE]  DEFAULT (getdate()),
	[EDITWHO] [varchar](100) NULL CONSTRAINT [DF_SrvPr_REPORTS_EDITWHO]  DEFAULT (USER_NAME()),
	[EDITDATE] [datetime] NULL CONSTRAINT [DF_SrvPr_REPORTS_EDITDATE]  DEFAULT (getdate())



UPDATE [dbo].[SrvPr_REPORTS] SET [ADDWHO] = 'INIT',[ADDDATE] = getdate(),[EDITWHO] = 'INIT',[EDITDATE] = getdate()
SELECT * FROM  [dbo].[SrvPr_REPORTS]
/*
ALTER TABLE [dbo].[SrvPr_REPORTS] DROP CONSTRAINT [DF_SrvPr_REPORTS_ADDDATE]
ALTER TABLE [dbo].[SrvPr_REPORTS] DROP CONSTRAINT [DF_SrvPr_REPORTS_EDITDATE]

ALTER TABLE [dbo].[SrvPr_REPORTS] DROP CONSTRAINT [DF_SrvPr_REPORTS_ADDWHO]
ALTER TABLE [dbo].[SrvPr_REPORTS] DROP CONSTRAINT [DF_SrvPr_REPORTS_EDITWHO]

ALTER TABLE [dbo].[SrvPr_REPORTS] DROP COLUMN [ADDWHO]
ALTER TABLE [dbo].[SrvPr_REPORTS] DROP COLUMN [ADDDATE]
ALTER TABLE [dbo].[SrvPr_REPORTS] DROP COLUMN [EDITWHO]
ALTER TABLE [dbo].[SrvPr_REPORTS] DROP COLUMN [EDITDATE]
*/
