--- Question 1
SELECT UPPER(DisplayName) AS UpperDisplayName, LEN(DisplayName) AS NameLength
FROM Users;

--- Question 2
SELECT Title, DATEDIFF(DAY, CreationDate, GETDATE()) AS DaysPassed
FROM Posts;

--- Question 3
SELECT OwnerUserId, COUNT(*) AS TotalPosts
FROM Posts
WHERE OwnerUserId IS NOT NULL
GROUP BY OwnerUserId;

--- Question 4
SELECT DisplayName, Reputation
FROM Users
WHERE Reputation > (SELECT AVG(Reputation) FROM Users);

--- Question 5
SELECT ISNULL(Title, 'No Title') AS FullTitle,
       SUBSTRING(ISNULL(Title, 'No Title'), 1, 50) AS ShortTitle
FROM Posts;

--- Question 6
SELECT PostTypeId, SUM(Score) AS TotalScore, AVG(CAST(Score AS FLOAT)) AS AverageScore, COUNT(*) AS PostCount
FROM Posts
GROUP BY PostTypeId
HAVING COUNT(*) > 100;

--- Question 7
SELECT DisplayName, 
       (SELECT COUNT(*) FROM Badges B WHERE B.UserId = U.Id) AS TotalBadges
FROM Users U;

--- Question 8
SELECT Title, Score, FORMAT(CreationDate, 'MMM dd, yyyy') AS FormattedDate
FROM Posts
WHERE CHARINDEX('SQL', Title) > 0;

--- Question 10
SELECT DisplayName, Location, 
       IIF(Reputation > 5000, 'High', 'Normal') AS ReputationLevel
FROM Users
WHERE Location IS NOT NULL;

--- Question 11
SELECT U.DisplayName, UserStats.TotalPosts, UserStats.AvgScore
FROM Users U
JOIN (
    SELECT OwnerUserId, COUNT(*) AS TotalPosts, AVG(CAST(Score AS FLOAT)) AS AvgScore
    FROM Posts
    GROUP BY OwnerUserId
) AS UserStats ON U.Id = UserStats.OwnerUserId
WHERE UserStats.TotalPosts > 3;

--- Question 12
SELECT UserId, Name AS BadgeName, COUNT(*) AS TimesEarned
FROM Badges
GROUP BY UserId, Name
HAVING COUNT(*) > 1;

--- Question 13
SELECT DisplayName, 
       ROUND(CAST(DATEDIFF(DAY, CreationDate, GETDATE()) AS FLOAT) / 365.25, 2) AS AccountAgeYears,
       ABS(DownVotes) AS AbsoluteDownVotes
FROM Users;

--- Question 14

