--- Question 1
CREATE TRIGGER tr_LogNewPost
ON Posts
AFTER INSERT
AS
BEGIN
    INSERT INTO ChangeLog (TableName, ActionType, UserId, NewData, LogDate)
    SELECT 'Posts', 'INSERT', UserId, Title, GETDATE()
    FROM inserted;
END;

--- Question 2
CREATE TRIGGER tr_TrackReputationChange
ON Users
AFTER UPDATE
AS
BEGIN
    INSERT INTO ChangeLog (TableName, ActionType, UserId, OldData, NewData, LogDate)
    SELECT 'Users', 'UPDATE', i.Id, d.Reputation, i.Reputation, GETDATE()
    FROM inserted i
    JOIN deleted d ON i.Id = d.Id
    WHERE i.Reputation <> d.Reputation;
END;

--- Question 3
CREATE TRIGGER tr_ArchiveDeletedPosts
ON Posts
AFTER DELETE
AS
BEGIN
    INSERT INTO DeletedPosts (Id, UserId, Title, Body, Score, DeletedDate)
    SELECT Id, UserId, Title, Body, Score, GETDATE()
    FROM deleted;
END;

--- Question 4
CREATE TRIGGER tr_ValidateNewUser
ON vw_NewUsers
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE DisplayName IS NULL OR DisplayName = '')
    BEGIN
        RAISERROR('DisplayName cannot be empty.', 16, 1);
        RETURN;
    END;

    INSERT INTO Users (DisplayName, Reputation)
    SELECT DisplayName, Reputation
    FROM inserted;
END;

--- Question 5
CREATE TRIGGER tr_PreventIdUpdate
ON Posts
INSTEAD OF UPDATE
AS
BEGIN
    IF UPDATE(Id)
    BEGIN
        INSERT INTO ChangeLog (TableName, ActionType, Details, LogDate)
        VALUES ('Posts', 'BLOCKED UPDATE', 'Attempted to update Id column', GETDATE());
        RAISERROR('Id column cannot be updated.', 16, 1);
        RETURN;
    END;

    UPDATE Posts
    SET Score = i.Score, Title = i.Title, Body = i.Body
    FROM Posts p
    JOIN inserted i ON p.Id = i.Id;
END;

--- Question 6
CREATE TRIGGER tr_SoftDeleteComments
ON Comments
INSTEAD OF DELETE
AS
BEGIN
    UPDATE c
    SET IsDeleted = 1
    FROM Comments c
    JOIN deleted d ON c.Id = d.Id;

    INSERT INTO ChangeLog (TableName, ActionType, Details, LogDate)
    VALUES ('Comments', 'SOFT DELETE', 'Marked as deleted', GETDATE());
END;

--- Question 7
CREATE TRIGGER tr_PreventDropTable
ON DATABASE
FOR DROP_TABLE
AS
BEGIN
    INSERT INTO ChangeLog (TableName, ActionType, Details, LogDate)
    VALUES (EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'NVARCHAR(100)'),
            'DROP TABLE ATTEMPT',
            EVENTDATA().value('(/EVENT_INSTANCE/TSQLCommand/CommandText)[1]', 'NVARCHAR(MAX)'),
            GETDATE());
    RAISERROR('Table drop is not allowed.', 16, 1);
    ROLLBACK;
END;

--- Question 8
--- ?
--- Question 9 
--- ?

--- Question 10 
CREATE TRIGGER tr_TrackBadgeChanges
ON Badges
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @Action NVARCHAR(10);

    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
        SET @Action = 'UPDATE';
    ELSE IF EXISTS (SELECT 1 FROM inserted)
        SET @Action = 'INSERT';
    ELSE
        SET @Action = 'DELETE';

    INSERT INTO ChangeLog (TableName, ActionType, UserId, LogDate)
    SELECT 'Badges', @Action, UserId, GETDATE()
    FROM (SELECT UserId FROM inserted UNION SELECT UserId FROM deleted) t;
END;

--- Question 11
CREATE TRIGGER tr_UpdatePostStatistics
ON Posts
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    MERGE PostStatistics AS target
    USING (
        SELECT UserId, COUNT(*) AS PostCount, SUM(Score) AS TotalScore, AVG(Score) AS AvgScore
        FROM Posts
        WHERE UserId IN (SELECT UserId FROM inserted UNION SELECT UserId FROM deleted)
        GROUP BY UserId
    ) AS source
    ON target.UserId = source.UserId
    WHEN MATCHED THEN
        UPDATE SET 
            TotalPosts = source.PostCount,
            TotalScore = source.TotalScore,
            AvgScore = source.AvgScore,
            LastUpdated = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (UserId, TotalPosts, TotalScore, AvgScore, LastUpdated)
        VALUES (source.UserId, source.PostCount, source.TotalScore, source.AvgScore, GETDATE());
END;

--- Question 12
CREATE TRIGGER tr_PreventDeleteHighScorePost
ON Posts
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted WHERE Score > 100)
    BEGIN
        INSERT INTO ChangeLog (TableName, ActionType, Details, LogDate)
        VALUES ('Posts', 'BLOCKED DELETE', 'Attempted to delete high score post', GETDATE());
        RAISERROR('Cannot delete posts with score > 100.', 16, 1);
        RETURN;
    END;

    DELETE FROM Posts
    WHERE Id IN (SELECT Id FROM deleted);
END;

--- Question 13
-- 1. Disable trigger
DISABLE TRIGGER tr_PreventDeleteHighScorePost ON Posts;

-- 2. Enable trigger
ENABLE TRIGGER tr_PreventDeleteHighScorePost ON Posts;

-- 3. Check trigger status
SELECT name, is_disabled
FROM sys.triggers
WHERE name = 'tr_PreventDeleteHighScorePost';