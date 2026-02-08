--- Question 1
SELECT * 
FROM posts 
WHERE user_id = 5 AND score > 50 
ORDER BY score DESC;

--- A , B
CREATE INDEX idx_user_score_covering 
ON posts (owner_id, score) 
INCLUDE (title);

--- C
SELECT title, score 
FROM posts 
WHERE owner_id = 5 AND score > 50 
ORDER BY score DESC;

--- D [i searched for it]
-- For PostgreSQL
SELECT * FROM pg_indexes WHERE indexname = 'idx_user_score_covering';
-- For SQL Server
EXEC sp_helpindex 'posts';

--- Question 2

--- A , B
CREATE INDEX idx_high_value_posts 
ON posts (score, title) 
WHERE score > 100 AND title IS NOT NULL;

--- C
SELECT title, score 
FROM posts 
WHERE score > 150 AND title IS NOT NULL;

--- D
--- 3lshan hya bt Reduced Storage kman El database only updates the index when a post's score crosses the 100-point threshold
--- and Higher Performance l2n el index byst5dm less space w bykml 2l7d el rows elly bttby2 el condition.
