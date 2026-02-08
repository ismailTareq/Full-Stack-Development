--- Question 1
CREATE PROCEDURE sp_GetRecentBadges
    @DaysBack INT
AS
BEGIN
    SELECT *
    FROM Badges
    WHERE DateEarned >= DATEADD(DAY, -@DaysBack, GETDATE());
END;

--- Question 2
CREATE PROCEDURE sp_GetUserSummary
    @UserId INT,
    @TotalPosts INT OUTPUT,
    @TotalBadges INT OUTPUT,
    @AvgScore DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @TotalPosts = COUNT(*)
    FROM Posts
    WHERE UserId = @UserId;

    SELECT @TotalBadges = COUNT(*)
    FROM Badges
    WHERE UserId = @UserId;

    SELECT @AvgScore = AVG(Score)
    FROM Posts
    WHERE UserId = @UserId;
END;

--- Question 3
CREATE PROCEDURE sp_SearchPosts
    @Keyword NVARCHAR(100),
    @MinScore INT = 0
AS
BEGIN
    SELECT *
    FROM Posts
    WHERE Title LIKE '%' + @Keyword + '%'
      AND Score >= @MinScore
    ORDER BY Score DESC;
END;

--- Question 4
CREATE PROCEDURE sp_GetUserOrError
    @UserId INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
            RAISERROR('User not found.', 16, 1);

        SELECT * FROM Users WHERE Id = @UserId;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;

--- Question 5
CREATE PROCEDURE sp_AnalyzeUserActivity
    @UserId INT,
    @ActivityScore INT OUTPUT
AS
BEGIN
    DECLARE @Reputation INT, @PostCount INT;

    SELECT @Reputation = Reputation
    FROM Users
    WHERE Id = @UserId;

    SELECT @PostCount = COUNT(*)
    FROM Posts
    WHERE UserId = @UserId;

    SET @ActivityScore = @Reputation + (@PostCount * 10);

    SELECT TOP 5 *
    FROM Posts
    WHERE UserId = @UserId
    ORDER BY Score DESC;
END;

--- Question 6
CREATE PROCEDURE sp_GetReputationInOut
    @UserIdReputation INT OUTPUT
AS
BEGIN
    SELECT @UserIdReputation = Reputation
    FROM Users
    WHERE Id = @UserIdReputation;
END;

--- Question 7
CREATE PROCEDURE sp_UpdatePostScore
    @PostId INT,
    @NewScore INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Posts WHERE Id = @PostId)
            RAISERROR('Post not found.', 16, 1);

        UPDATE Posts
        SET Score = @NewScore
        WHERE Id = @PostId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

--- Question 8
CREATE PROCEDURE sp_GetTopUsersByReputation
    @TopN INT,
    @MinReputation INT
AS
BEGIN
    SELECT TOP (@TopN) *
    FROM Users
    WHERE Reputation >= @MinReputation
    ORDER BY Reputation DESC;
END;

CREATE TABLE TopUsersArchive (
    UserId INT,
    DisplayName NVARCHAR(100),
    Reputation INT,
    ArchivedDate DATETIME DEFAULT GETDATE()
);

--- Question 9
CREATE PROCEDURE sp_InsertUserLog
    @UserId INT,
    @Action NVARCHAR(50),
    @Details NVARCHAR(MAX),
    @LogId INT OUTPUT
AS
BEGIN
    INSERT INTO UserLog (UserId, Action, Details, LogDate)
    VALUES (@UserId, @Action, @Details, GETDATE());

    SET @LogId = SCOPE_IDENTITY();
END;

--- Question 10
--- ?

--- Question 11
CREATE PROCEDURE sp_DeleteLowScorePosts
    @MinScore INT,
    @DeletedCount INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM Posts
        WHERE Score <= @MinScore;

        SET @DeletedCount = @@ROWCOUNT;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

--- Question 12
CREATE PROCEDURE sp_BulkInsertBadges
    @UserId INT,
    @BadgeCount INT
AS
BEGIN
    DECLARE @i INT = 1;
    WHILE @i <= @BadgeCount
    BEGIN
        INSERT INTO Badges (UserId, BadgeName, DateEarned)
        VALUES (@UserId, 'AutoBadge', GETDATE());
        SET @i = @i + 1;
    END
END;

--- Question 13
CREATE PROCEDURE sp_GenerateUserReport
    @UserId INT
AS
BEGIN
    DECLARE @TotalPosts INT, @TotalBadges INT, @AvgScore DECIMAL(10,2);

    EXEC sp_GetUserSummary 
        @UserId = @UserId,
        @TotalPosts = @TotalPosts OUTPUT,
        @TotalBadges = @TotalBadges OUTPUT,
        @AvgScore = @AvgScore OUTPUT;

    SELECT 
        u.DisplayName,
        u.Reputation,
        @TotalPosts AS TotalPosts,
        @TotalBadges AS TotalBadges,
        @AvgScore AS AvgScore,
        CASE 
            WHEN u.Reputation > 1000 THEN 'Expert'
            WHEN u.Reputation > 500 THEN 'Advanced'
            ELSE 'Beginner'
        END AS UserLevel
    FROM Users u
    WHERE u.Id = @UserId;
END;
