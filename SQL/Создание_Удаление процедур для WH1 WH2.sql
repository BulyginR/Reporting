USE GHOST
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [_CopyProcForAllUsers]
(
@proc VARCHAR(100)
)
AS
begin

	SET NOCOUNT ON;


declare @txt varchar(8000)
declare @txtWH1 varchar(8000)
declare @txtWH2 varchar(8000)


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = 'WH1' AND 
		 SPECIFIC_NAME = @proc )
	begin
	exec ('DROP PROCEDURE [WH1].['+ @proc +']')
	end

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = 'WH2' AND 
		 SPECIFIC_NAME = @proc )
	begin
	exec ('DROP PROCEDURE [WH2].['+ @proc +']')
	end


set @txt=(select c.text from dbo.syscomments c, dbo.sysobjects o where o.id = c.id and c.id = object_id(@proc))
set @txtWH1=replace(@txt, '['+@proc+']', '[WH1].' + '['+@proc+']')
set @txtWH2=replace(@txt, '['+@proc+']', '[WH2].' + '['+@proc+']')

exec (@txtWH1)
exec (@txtWH2)

exec ('GRANT EXECUTE ON [WH1].[' + @proc + '] TO [R_WH1]');
exec ('GRANT EXECUTE ON [WH1].[' + @proc + '] TO [R_WH1_RPT]');
exec ('GRANT EXECUTE ON [WH2].[' + @proc + '] TO [R_WH2]');
exec ('GRANT EXECUTE ON [WH2].[' + @proc + '] TO [R_WH2_RPT]');

end


GO
ALTER PROCEDURE [_DeleteProcIfExists]
(
@proc VARCHAR(100),
@SCHEMA VARCHAR(100) = NULL
)
AS
begin

	IF @SCHEMA IS NULL
		BEGIN
		IF EXISTS (SELECT * 
			FROM INFORMATION_SCHEMA.ROUTINES 
		   WHERE SPECIFIC_SCHEMA = 'dbo' AND 
				 SPECIFIC_NAME = @proc )
		EXEC ('DROP PROCEDURE [dbo].[' + @proc + ']')

		IF EXISTS (SELECT * 
			FROM INFORMATION_SCHEMA.ROUTINES 
		   WHERE SPECIFIC_SCHEMA = 'WH1' AND 
				 SPECIFIC_NAME = @proc )
		EXEC ('DROP PROCEDURE [WH1].[' + @proc + ']')

		IF EXISTS (SELECT * 
			FROM INFORMATION_SCHEMA.ROUTINES 
		   WHERE SPECIFIC_SCHEMA = 'WH2' AND 
				 SPECIFIC_NAME = @proc )
		EXEC ('DROP PROCEDURE [WH2].[' + @proc + ']')
		END
	ELSE
		BEGIN
		IF EXISTS (SELECT * 
			FROM INFORMATION_SCHEMA.ROUTINES 
		   WHERE SPECIFIC_SCHEMA = @SCHEMA AND 
				 SPECIFIC_NAME = @proc )

		EXEC ('DROP PROCEDURE [' + @SCHEMA + '].[' + @proc + ']')
		END
end