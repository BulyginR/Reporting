USE GHOST
GO
_DeleteProcIfExists 'UP_SrvPr_Test'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [UP_SrvPr_Test]
AS
begin
SET NOCOUNT ON
--ТЕКСТ ПРОЦЕДУРЫ--НАЧАЛО--
select top 1000 * from [PRD1]..SKU



--ТЕКСТ ПРОЦЕДУРЫ--КОНЕЦ--
end
GO
_CopyProcForAllUsers 'UP_SrvPr_Test'

