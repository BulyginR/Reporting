USE [GHOST]
exec [_DeleteProcIfExists] 'SrvPr_PRINTLOG_VIEW', 'FUNCTION'
GO
CREATE FUNCTION [WH1].[SrvPr_PRINTLOG_VIEW]
(
@DOCSREPORTS VARCHAR(MAX) = null,
@DOCS VARCHAR(MAX) = null,
@REPORTS VARCHAR(MAX) = null,
@DateFrom datetime = null,
@DateTo datetime = null,
@UserName [varchar](100) = null,
@WORKSTATION [varchar](200) = null
)

RETURNS @Values TABLE (
	[SERIALKEY] [int],
	[REPORTNAME] [varchar](100),
	[DOCNUMBER] [varchar](100),
	[ORIGINAL] [bit],
	[PRINTAMOUNT] [int],
	[PRINTFORMAT] [int],
	[FORCEDCOPY] [bit],
	[ADDWHO] [varchar](100),
	[ADDDATE] [datetime],
	[EDITWHO] [varchar](100),
	[EDITDATE] [datetime],
	[WORKSTATION] [varchar](200)
)AS

BEGIN

	 DECLARE @WAREHOUSEPROC nvarchar(10)
	 SET @WAREHOUSEPROC='WH1'
	 /*
	 if OBJECT_ID('tempdb..#TBL_DOCSREPORTS') is not null
		 drop table #TBL_DOCSREPORTS
	 if OBJECT_ID('tempdb..#TBL_DOCS') is not null
		 drop table #TBL_DOCS
	 if OBJECT_ID('tempdb..#TBL_REPORTS') is not null
		 drop table #TBL_REPORTS*/
	 /*CREATE TABLE #TBL_DOCSREPORTS (IDENT INT, DOCNUMBER  [varchar](100), REPORTNAME [varchar](100))
	 CREATE TABLE #TBL_DOCS (DOCNUMBER [varchar](100))
	 CREATE TABLE #TBL_REPORTS (REPORTNAME [varchar](100))*/
		 
	 DECLARE @TBL_DOCSREPORTS TABLE(IDENT INT, DOCNUMBER  [varchar](100), REPORTNAME [varchar](100))
	 DECLARE @TBL_DOCS TABLE(DOCNUMBER [varchar](100))
	 DECLARE @TBL_REPORTS TABLE(REPORTNAME [varchar](100))
	 
	 IF ISNULL(@DOCSREPORTS,'') <> ''
		  INSERT INTO @TBL_DOCSREPORTS (IDENT, DOCNUMBER, REPORTNAME)
		  SELECT 
				ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS IDENT,
				SUBSTRING(ITEM,1,CASE WHEN CHARINDEX('#',ITEM)>0 THEN CHARINDEX('#',ITEM)-1 ELSE 0 END) AS DOCNUMBER,
				SUBSTRING(ITEM,CHARINDEX('#',ITEM)+1, LEN(ITEM)) AS REPORTNAME
		  FROM PRD1.DBO.SPLITLIST(@DOCSREPORTS,'|')

	 IF ISNULL(@DOCS,'') <> ''
		  INSERT INTO @TBL_DOCS (DOCNUMBER)
		  SELECT ITEM AS DOCNUMBER FROM PRD1.DBO.SPLITLIST(@DOCS,'|')

	 IF ISNULL(@REPORTS,'') <> ''
		  INSERT INTO @TBL_REPORTS (REPORTNAME)
		  SELECT ITEM AS REPORTNAME FROM PRD1.DBO.SPLITLIST(@REPORTS,'|')
		  
	INSERT INTO @Values
	([SERIALKEY],[REPORTNAME],[DOCNUMBER],[ORIGINAL],[PRINTAMOUNT],[PRINTFORMAT],[FORCEDCOPY],[ADDWHO],[ADDDATE],[EDITWHO],[EDITDATE],[WORKSTATION])
	SELECT 
	 PL.[SERIALKEY],PL.[REPORTNAME],PL.[DOCNUMBER],PL.[ORIGINAL],PL.[PRINTAMOUNT],PL.[PRINTFORMAT], PL.[FORCEDCOPY],PL.[ADDWHO],PL.[ADDDATE],PL.[EDITWHO],PL.[EDITDATE],PL.[WORKSTATION]
	FROM SrvPr_PRINTLOG PL WITH(NOLOCK)
		 LEFT JOIN (SELECT * FROM @TBL_DOCSREPORTS) TBL_DOCSREPORTS ON TBL_DOCSREPORTS.DOCNUMBER=PL.DOCNUMBER AND TBL_DOCSREPORTS.REPORTNAME=PL.REPORTNAME
		 LEFT JOIN (SELECT * FROM @TBL_DOCS) TBL_DOCS  ON TBL_DOCS.DOCNUMBER=PL.DOCNUMBER
		 LEFT JOIN (SELECT * FROM @TBL_REPORTS) TBL_REPORTS  ON TBL_REPORTS.REPORTNAME=PL.REPORTNAME
	WHERE 
		  (@DOCSREPORTS IS NULL OR TBL_DOCSREPORTS.DOCNUMBER IS NOT NULL)
		  AND (@DOCS IS NULL OR TBL_DOCS.DOCNUMBER IS NOT NULL)
		  AND (@REPORTS IS NULL OR TBL_REPORTS.REPORTNAME IS NOT NULL)
		  AND (@DateFrom IS NULL OR PL.[ADDDATE] >= @DateFrom)
		  AND (@DateTo IS NULL OR 
						  ( 
								(@DateTo = DATEADD(DAY, DATEDIFF(DAY, 0, @DateTo), 0) and PL.[ADDDATE] < DATEADD(DAY, 1, @DateTo )) OR 
								(@DateTo <> DATEADD(DAY, DATEDIFF(DAY, 0, @DateTo), 0) and PL.[ADDDATE] <= @DateTo) 
						  ) 
				)
		  AND (@UserName IS NULL OR PL.[ADDWHO] LIKE '%' + @UserName + '%')
		  AND (@WORKSTATION IS NULL OR PL.[WORKSTATION] LIKE '%' + @WORKSTATION + '%')
	 RETURN
END
GO
GRANT SELECT ON  [WH1].[SrvPr_PRINTLOG_VIEW] TO [PUBLIC]

GO
exec [_CopyProcForAllSchemas] 'SrvPr_PRINTLOG_VIEW', 'FUNCTION'
