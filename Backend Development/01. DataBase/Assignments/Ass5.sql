-- Question1
-- I choose CASCADE because a hotel is deleted,
-- all its rooms should also be deleted because they cannot exist without the hotel.
FOREIGN KEY (hotel_id) REFERENCES Hotels(hotel_id) ON DELETE CASCADE

-- Question2
-- I choose SET NULL because if a department is deleted,
-- the employees can still exist but without a room.
FOREIGN KEY (room_id) REFERENCES Departments(room_id) ON DELETE SET NULL

-- Question3
-- I choose CASCADE because if a staff member ID is updated,
-- all related service records should also be Updated to maintain referential integrity.
FOREIGN KEY (staff_id) REFERENCES Staff(staff_id) ON UPDATE CASCADE

-- StackOverflow DB
-- Question 1
-- Display all users along with all post types
SELECT u.user_id, u.display_name, pt.post_type
-- or 
-- SELECT u.*, pt.*
FROM Users u
CROSS JOIN PostTypes pt;

-- Question 2
-- Retrieve all posts along with their owner's display name and reputation. Only include posts that have an owner.
SELECT p.*, u.display_name, u.reputation
FROM Posts p
JOIN Users u ON p.owner_user_id = u.user_id;

-- Question 3
-- Show all comments with their associated post titles.
SELECT c.Text, c.Score, p.Title AS PostTitle
FROM Comments c
INNER JOIN Posts p ON c.PostId = p.Id;

-- Question 4
-- List all users and their badges (if any). Include users even if they don't have badges.
SELECT u.user_id, u.display_name, b.Name AS BadgeName
FROM Users u
LEFT JOIN Badges b ON u.user_id = b.UserId;

-- Question 5
-- Display all posts along with their comments (if any). Include posts that have no comments.
SELECT p.Title, p.Score AS PostScore, c.Text AS CommentText, c.Score AS CommentScore
FROM Posts p
LEFT JOIN Comments c ON p.Id = c.PostId;

-- Question 6
-- Show all votes along with their corresponding posts. Include all votes even if post info is missing.
SELECT v.*, p.Title AS PostTitle, p.Score AS PostScore
FROM Votes v
LEFT JOIN Posts p ON v.PostId = p.Id;

-- Question 7
-- Find all answers along with their parent question.
SELECT a.*, q.Title AS QuestionTitle, q.Score AS QuestionScore
FROM Posts a
JOIN Posts q ON a.ParentId = q.Id
WHERE a.PostTypeId = 2 AND q.PostTypeId = 1; -- da 3la 2ftrad 2nha Answers

-- Question 8
-- Display all related posts using the PostLinks table.
SELECT p1.Title AS OriginalPostTitle, p2.Title AS RelatedPostTitle, pl.LinkTypeId
FROM PostLinks pl
INNER JOIN Posts p1 ON pl.PostId = p1.Id
INNER JOIN Posts p2 ON pl.RelatedPostId = p2.Id;

-- Question 9
-- Show posts with their authors and the post type name.
SELECT p.Title, u.DisplayName AS AuthorName, u.Reputation AS AuthorReputation, pt.Name AS PostType
FROM Posts p
INNER JOIN Users u ON p.OwnerUserId = u.Id
INNER JOIN PostTypes pt ON p.PostTypeId = pt.Id;

-- Question 10
-- Retrieve all comments along with post title, post author, and commenter's display name.
SELECT c.Text AS CommentText, p.Title AS PostTitle, 
       u1.DisplayName AS PostAuthor, u2.DisplayName AS CommenterName
FROM Comments c
INNER JOIN Posts p ON c.PostId = p.Id
INNER JOIN Users u1 ON p.OwnerUserId = u1.Id
INNER JOIN Users u2 ON c.UserId = u2.Id;

-- Question 11
-- Display all votes with post information and vote type name.
SELECT v.*, p.Title AS PostTitle, vt.Name AS VoteTypeName
FROM Votes v
LEFT JOIN Posts p ON v.PostId = p.Id
LEFT JOIN VoteTypes vt ON v.VoteTypeId = vt.Id;

-- Question 12
-- Show all users along with their posts and comments on those posts.
SELECT u.DisplayName AS UserName, p.Title AS PostTitle, c.Text AS CommentText
FROM Users u
LEFT JOIN Posts p ON u.Id = p.OwnerUserId
LEFT JOIN Comments c ON p.Id = c.PostId;

-- Question 13
-- Retrieve posts with their authors, post types, and any badges the author has earned.
SELECT p.Title AS PostTitle, u.DisplayName AS AuthorName, pt.Name AS PostTypeName, b.Name AS BadgeName
FROM Posts p
INNER JOIN Users u ON p.OwnerUserId = u.Id
INNER JOIN PostTypes pt ON p.PostTypeId = pt.Id
LEFT JOIN Badges b ON u.Id = b.UserId;
