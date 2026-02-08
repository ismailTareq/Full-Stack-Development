--- Question 1
SELECT TOP 15 DisplayName, Reputation, Location
FROM Users
ORDER BY Reputation DESC;

--- Question 2
SELECT TOP 10 WITH TIES Title, Score, ViewCount
FROM Posts
ORDER BY Score DESC;

--- Question 3
SELECT DisplayName, Reputation
FROM Users
ORDER BY Reputation DESC
OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY;

--- Question 4
SELECT ROW_NUMBER() OVER (ORDER BY Score DESC) AS RowNum, Title, Score
FROM Posts
WHERE Title IS NOT NULL;

--- Question 5
SELECT RANK() OVER (ORDER BY Reputation DESC) AS UserRank, DisplayName, Reputation
FROM Users;

--- Question 6
SELECT DENSE_RANK() OVER (ORDER BY Score DESC) AS DenseRank, Title, Score
FROM Posts;

--- Question 7
SELECT NTILE(5) OVER (ORDER BY Reputation DESC) AS Quintile, DisplayName, Reputation
FROM Users;

--- Question 8
SELECT PostTypeId, 
       ROW_NUMBER() OVER (PARTITION BY PostTypeId ORDER BY Score DESC) AS RankWithinType, 
       Title, 
       Score
FROM Posts
ORDER BY PostTypeId, Score DESC;