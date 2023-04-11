IF NOT EXISTS (SELECT TOP 1 RowNumber FROM dbo.ApplicationReferenceNumbers)
BEGIN 
	;WITH RowNumbers AS 
    (
      SELECT TOP (2000000) rn = ROW_NUMBER() OVER (ORDER BY s1.[object_id])
        FROM sys.all_objects AS s1 
        CROSS JOIN sys.all_objects AS s2
        ORDER BY s1.[object_id]
    )
    INSERT dbo.ApplicationReferenceNumbers(RowNumber, NextID)
      SELECT rn, ROW_NUMBER() OVER (ORDER BY NEWID()) + 1000000
      FROM RowNumbers;
END