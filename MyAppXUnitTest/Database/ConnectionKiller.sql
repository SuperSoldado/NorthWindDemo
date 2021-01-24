--This runs before rest DB to kill other connections that prevent DB reset
CREATE PROCEDURE [dbo].[sp_KillSpidsByDBName] 
@dbname sysname = '[DatabaseNameToKill]'
AS
BEGIN

-- check the input database name
IF DATALENGTH(@dbname) = 0 OR LOWER(@dbname) = 'master' OR LOWER(@dbname) = 'msdb'
RETURN

DECLARE @sql VARCHAR(30) 
DECLARE @rowCtr INT
DECLARE @killStmts TABLE (stmt VARCHAR(30))

-- find all the SPIDs for the requested db, and create KILL statements 
--   for each of them in the @killStmts table variable
INSERT INTO @killStmts SELECT 'KILL ' + CONVERT (VARCHAR(25), spid)
FROM master..sysprocesses pr
INNER JOIN master..sysdatabases db
ON pr.dbid = db.dbid
WHERE db.name = @dbname

-- iterate through all the rows in @killStmts, executing each statement
SELECT @rowCtr = COUNT(1) FROM @killStmts
WHILE (@rowCtr > 0)
    BEGIN
        SELECT TOP(1) @sql = stmt FROM @killStmts
        EXEC (@sql)
        DELETE @killStmts WHERE stmt = @sql
        SELECT @rowCtr = COUNT(1) FROM @killStmts
    END

END

GO