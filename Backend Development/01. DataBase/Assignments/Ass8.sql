--- Question1
SELECT DISTINCT UserId, DisplayName, Reputation
FROM Users
WHERE Reputation > 8000 OR UserId IN (
    SELECT OwnerUserId 
    FROM Posts 
    GROUP BY OwnerUserId 
    HAVING COUNT(*) > 15
);

--- Question2
SELECT 
    u.UserId,
    u.DisplayName,
    u.Reputation
FROM Users u
WHERE u.Reputation > 3000
  AND (
      SELECT COUNT(*) 
      FROM Badges b 
      WHERE b.UserId = u.UserId
  ) >= 5;

--- Question3
SELECT 
    p.PostId,
    p.Title,
    p.Score
FROM Posts p
WHERE p.Score > 20
  AND NOT EXISTS (
      SELECT 1 FROM Comments c 
      WHERE c.PostId = p.PostId
  );

--- Question4
CREATE TABLE Posts_Backup AS
SELECT 
    Id,
    Title,
    Score,
    ViewCount,
    CreationDate,
    OwnerUserId
FROM Posts
WHERE Score > 10;

--- Question5
CREATE TABLE ActiveUsers AS
SELECT 
    u.UserId,
    u.DisplayName,
    u.Reputation,
    u.Location,
    (SELECT COUNT(*) FROM Posts p WHERE p.OwnerUserId = u.UserId) AS PostCount
FROM Users u
WHERE u.Reputation > 1000
  AND EXISTS (
      SELECT 1 FROM Posts p 
      WHERE p.OwnerUserId = u.UserId
  );

--- Question6
CREATE TABLE Comments_Template AS
SELECT * FROM Comments WHERE 1 = 0;

--- Question7
CREATE TABLE PostEngagementSummary AS
SELECT 
    p.PostId,
    p.Title,
    u.DisplayName AS AuthorName,
    p.Score,
    p.ViewCount,
    COUNT(c.CommentId) AS CommentCount,
    SUM(c.Score) AS TotalCommentScore
FROM Posts p
JOIN Users u ON p.OwnerUserId = u.UserId
LEFT JOIN Comments c ON p.PostId = c.PostId
GROUP BY p.PostId, p.Title, u.DisplayName, p.Score, p.ViewCount
HAVING COUNT(c.CommentId) >= 3;

--- Question8
CREATE FUNCTION GetBadgeLevel(@Reputation INT, @PostCount INT)
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @BadgeLevel VARCHAR(10);
    
    IF @Reputation > 10000 AND @PostCount > 50
        SET @BadgeLevel = 'Gold'
    ELSE IF @Reputation > 5000 AND @PostCount > 20
        SET @BadgeLevel = 'Silver'
    ELSE IF @Reputation > 1000 AND @PostCount > 5
        SET @BadgeLevel = 'Bronze'
    ELSE
        SET @BadgeLevel = 'None'
        
    RETURN @BadgeLevel;
END;

-- Test bt3ha
SELECT 
    DisplayName,
    Reputation,
    (SELECT COUNT(*) FROM Posts p WHERE p.OwnerUserId = u.UserId) AS PostCount,
    dbo.GetBadgeLevel(Reputation, 
        (SELECT COUNT(*) FROM Posts p WHERE p.OwnerUserId = u.UserId)
    ) AS BadgeLevel
FROM Users u;

--- Question9
CREATE PROCEDURE GetRecentPosts
    @DaysBack INT
AS
BEGIN
    SELECT 
        PostId,
        Title,
        Score,
        ViewCount,
        CreationDate
    FROM Posts
    WHERE CreationDate >= DATEADD(DAY, -@DaysBack, GETDATE());
END;

-- Test bt3ha
EXEC GetRecentPosts @DaysBack = 30;
EXEC GetRecentPosts @DaysBack = 90;

--- Question10
CREATE PROCEDURE FindTopUsers
    @MinReputation INT,
    @Location VARCHAR(100) = NULL
AS
BEGIN
    SELECT 
        UserId,
        DisplayName,
        Reputation,
        Location,
        CreationDate
    FROM Users
    WHERE Reputation >= @MinReputation
      AND (@Location IS NULL OR Location = @Location)
    ORDER BY Reputation DESC;
END;

-- Test bt3ha
EXEC FindTopUsers @MinReputation = 5000, @Location = 'New York';
EXEC FindTopUsers @MinReputation = 1000, @Location = NULL;

--- Question11
WITH RankedPosts AS (
    SELECT 
        PostTypeId,
        Title,
        Score,
        ROW_NUMBER() OVER (PARTITION BY PostTypeId ORDER BY Score DESC) AS Rank
    FROM Posts
)
SELECT 
    PostTypeId,
    Title,
    Score,
    Rank
FROM RankedPosts
WHERE Rank <= 3;

--- Question12
WITH AvgReputation AS (
    SELECT AVG(Reputation) AS AvgRep
    FROM Users
)
SELECT 
    u.DisplayName,
    u.Reputation,
    ar.AvgRep AS AverageReputation
FROM Users u
CROSS JOIN AvgReputation ar
WHERE u.Reputation > ar.AvgRep;

--- Question13
WITH UserPostStats AS (
    SELECT 
        OwnerUserId,
        COUNT(*) AS TotalPosts,
        AVG(Score) AS AvgScore
    FROM Posts
    GROUP BY OwnerUserId
    HAVING COUNT(*) > 5
)
SELECT 
    u.DisplayName,
    u.Reputation,
    ups.TotalPosts,
    ups.AvgScore
FROM Users u
JOIN UserPostStats ups ON u.UserId = ups.OwnerUserId;

--- Question14
WITH PostCountCTE AS (
    SELECT 
        OwnerUserId AS UserId,
        COUNT(*) AS PostCount
    FROM Posts
    GROUP BY OwnerUserId
),
BadgeCountCTE AS (
    SELECT 
        UserId,
        COUNT(*) AS BadgeCount
    FROM Badges
    GROUP BY UserId
)
SELECT 
    u.DisplayName,
    u.Reputation,
    ISNULL(pc.PostCount, 0) AS PostCount,
    ISNULL(bc.BadgeCount, 0) AS BadgeCount
FROM Users u
LEFT JOIN PostCountCTE pc ON u.UserId = pc.UserId
LEFT JOIN BadgeCountCTE bc ON u.UserId = bc.UserId;

--- Question15
WITH RECURSIVE NumberSequence AS (
    SELECT 1 AS Number
    UNION ALL
    SELECT Number + 1
    FROM NumberSequence
    WHERE Number < 20
)
SELECT Number FROM NumberSequence;