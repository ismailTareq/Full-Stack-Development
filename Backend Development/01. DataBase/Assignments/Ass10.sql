--- Question 1
CREATE VIEW vw_BasicUserInfo
AS
SELECT 
    DisplayName,
    Reputation,
    Location,
    CreationDate AS AccountCreationDate
FROM Users;

--- Test the view
SELECT * FROM vw_BasicUserInfo;
GO

--- Question 2
CREATE VIEW vw_HighScoringPosts
AS
SELECT 
    Title,
    Score,
    ViewCount,
    CreationDate
FROM Posts
WHERE Score > 10;

--- Test the view
SELECT * FROM vw_HighScoringPosts;
GO

--- Question 03
CREATE VIEW vw_PostsWithAuthors
AS
SELECT 
    p.Title AS PostTitle,
    p.Score AS PostScore,
    u.DisplayName AS AuthorName,
    u.Reputation AS AuthorReputation
FROM Posts p
INNER JOIN Users u ON p.OwnerUserId = u.UserId;

--- Test the view
SELECT * FROM vw_PostsWithAuthors;
GO

--- Question 4
CREATE VIEW vw_PostCommentStats
AS
SELECT 
    PostId,
    COUNT(*) AS TotalCommentCount,
    SUM(Score) AS SumCommentScores,
    AVG(CAST(Score AS DECIMAL(10,2))) AS AvgCommentScore
FROM Comments
GROUP BY PostId;

--- Test the view
SELECT * FROM vw_PostCommentStats;
GO

--- Question 5
CREATE VIEW vw_UserActivityIndexed
WITH SCHEMABINDING
AS
SELECT 
    u.UserId,
    u.DisplayName,
    u.Reputation,
    COUNT_BIG(*) AS TotalPostsCount
FROM dbo.Users u
INNER JOIN dbo.Posts p ON u.UserId = p.OwnerUserId
GROUP BY u.UserId, u.DisplayName, u.Reputation;
GO

CREATE UNIQUE CLUSTERED INDEX IX_vw_UserActivityIndexed
ON vw_UserActivityIndexed(UserId);
GO

--- Question 6
CREATE VIEW vw_UsersPartitioned
AS

SELECT 
    UserId,
    DisplayName,
    Reputation,
    'High Reputation' AS ReputationCategory
FROM Users
WHERE Reputation > 5000

UNION ALL

SELECT 
    UserId,
    DisplayName,
    Reputation,
    'Low Reputation' AS ReputationCategory
FROM Users
WHERE Reputation <= 5000;

--- Test the view
SELECT * FROM vw_UsersPartitioned;
GO

--- Question 7
CREATE VIEW vw_EditableUsers
AS
SELECT 
    UserId,
    DisplayName,
    Location
FROM Users;

-- Test by updating through the view
UPDATE vw_EditableUsers 
SET Location = 'New York'
WHERE UserId = 1;

SELECT * FROM vw_EditableUsers WHERE UserId = 1;
GO

--- Question 8
CREATE VIEW vw_QualityPosts
AS
SELECT 
    PostId,
    Title,
    Score,
    CreationDate
FROM Posts
WHERE Score >= 20
WITH CHECK OPTION;

-- Test (mfrod this will fail if trying to update score to less than 20)
UPDATE vw_QualityPosts 
SET Score = 15 
WHERE PostId = 1; -- w dih will fail due to CHECK OPTION
GO

--- Question 9
CREATE VIEW vw_ComprehensivePostInfo
AS
SELECT 
    p.PostId,
    p.Title,
    p.Score,
    u.DisplayName AS AuthorName,
    u.Reputation AS AuthorReputation,
    COUNT(c.CommentId) AS CommentCount
FROM Posts p
INNER JOIN Users u ON p.OwnerUserId = u.UserId
LEFT JOIN Comments c ON p.PostId = c.PostId
GROUP BY p.PostId, p.Title, p.Score, u.DisplayName, u.Reputation;

--- Test the view
SELECT * FROM vw_ComprehensivePostInfo;
GO


--- Question 11
CREATE VIEW vw_ActiveUsers
AS
SELECT 
    UserId,
    DisplayName,
    Reputation,
    LastAccessDate AS LastActivityDate
FROM Users
WHERE 
    (LastAccessDate >= DATEADD(DAY, -365, GETDATE()))
    OR 
    (Reputation > 1000);

--- Test the view
SELECT * FROM vw_ActiveUsers;
GO

--- Question 12
CREATE VIEW vw_UserPostMetrics
WITH SCHEMABINDING
AS
SELECT 
    OwnerUserId AS UserId,
    COUNT_BIG(*) AS TotalPosts,
    SUM(ViewCount) AS TotalViews,
    AVG(CAST(Score AS DECIMAL(10,2))) AS AvgScore
FROM dbo.Posts
WHERE OwnerUserId IS NOT NULL
GROUP BY OwnerUserId;
GO

CREATE UNIQUE CLUSTERED INDEX IX_vw_UserPostMetrics
ON vw_UserPostMetrics(UserId);
GO

--- Question 13
CREATE VIEW vw_PostsByCategory
AS
SELECT 
    PostId,
    Title,
    Score,
    CASE
        WHEN Score >= 100 THEN 'Excellent'
        WHEN Score >= 50 AND Score <= 99 THEN 'Good'
        WHEN Score >= 10 AND Score <= 49 THEN 'Average'
        WHEN Score < 10 THEN 'Low'
        ELSE 'Uncategorized'
    END AS Category
FROM Posts;

--- Test the view
SELECT * FROM vw_PostsByCategory;
GO