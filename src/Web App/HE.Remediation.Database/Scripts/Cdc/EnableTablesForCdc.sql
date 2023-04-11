DECLARE @TablesToIgnore Table (TableName nvarchar(128))
DECLARE @Schema nvarchar(10) = 'dbo'

INSERT INTO @TablesToIgnore
VALUES 
('systranschemas'),
('__RefactorLog'),
('ApplicationReferenceNumbers')

DECLARE @Name nvarchar(128)

DECLARE table_cursor CURSOR FOR
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME NOT IN (SELECT TableName FROM @TablesToIgnore)

OPEN table_cursor 
FETCH NEXT FROM table_cursor INTO  @Name
 WHILE @@FETCH_STATUS = 0  
    BEGIN   

	IF NOT EXISTS (SELECT [name] FROM sys.tables where is_tracked_by_cdc = 1 and [name] = @name)
	BEGIN
		EXEC sys.sp_cdc_enable_table  
			@source_schema = @Schema,  
			@source_name   = @Name,  
			@role_name     = NULL,  
			@filegroup_name = NULL,  
			@supports_net_changes = 0 
	END
	FETCH NEXT FROM table_cursor INTO @Name
	END
CLOSE table_cursor  