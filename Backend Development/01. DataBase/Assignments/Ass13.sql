-- Question 1
BEGIN TRANSACTION;

SELECT * FROM AccountBalance WHERE AccountId IN (101, 102);

UPDATE AccountBalance SET Balance = Balance - 500 WHERE AccountId = 101;
UPDATE AccountBalance SET Balance = Balance + 500 WHERE AccountId = 102;

SELECT * FROM AccountBalance WHERE AccountId IN (101, 102);

COMMIT TRANSACTION;

-- Question 2
BEGIN TRANSACTION;

SELECT * FROM AccountBalance WHERE AccountId IN (101, 102);

UPDATE AccountBalance SET Balance = Balance - 1000 WHERE AccountId = 101;
UPDATE AccountBalance SET Balance = Balance + 1000 WHERE AccountId = 102;

SELECT * FROM AccountBalance WHERE AccountId IN (101, 102);

ROLLBACK TRANSACTION;

SELECT * FROM AccountBalance WHERE AccountId IN (101, 102);

-- Question 3
BEGIN TRANSACTION;

DECLARE @Balance DECIMAL(18,2);
SELECT @Balance = Balance FROM AccountBalance WHERE AccountId = 101;

IF @Balance >= 2000
BEGIN
    UPDATE AccountBalance SET Balance = Balance - 2000 WHERE AccountId = 101;
    UPDATE AccountBalance SET Balance = Balance + 2000 WHERE AccountId = 102;
    PRINT 'Transfer successful.';
    COMMIT;
END
ELSE
BEGIN
    PRINT 'Insufficient funds.';
    ROLLBACK;
END

-- Question 4
BEGIN TRY
    BEGIN TRANSACTION;

    UPDATE AccountBalance SET Balance = Balance - 500 WHERE AccountId = 101;
    UPDATE AccountBalance SET Balance = Balance + 500 WHERE AccountId = 102;

    COMMIT TRANSACTION;
    PRINT 'Transfer completed successfully.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH

-- Question 5
BEGIN TRANSACTION;

-- Step 1
UPDATE AccountBalance SET Balance = Balance - 300 WHERE AccountId = 101;
SAVE TRANSACTION Savepoint1;

-- Step 2 (simulate error)
UPDATE AccountBalance SET Balance = Balance + 300 WHERE AccountId = 102;
IF @@ERROR <> 0
BEGIN
    ROLLBACK TRANSACTION Savepoint1;
    PRINT 'Rolled back to Savepoint1.';
END
ELSE
BEGIN
    COMMIT TRANSACTION;
END

-- Question 6
PRINT 'Initial TRANCOUNT: ' + CAST(@@TRANCOUNT AS VARCHAR);
BEGIN TRANSACTION T1;
PRINT 'After T1: ' + CAST(@@TRANCOUNT AS VARCHAR);

    BEGIN TRANSACTION T2;
    PRINT 'After T2: ' + CAST(@@TRANCOUNT AS VARCHAR);

    COMMIT TRANSACTION T2;
    PRINT 'After commit T2: ' + CAST(@@TRANCOUNT AS VARCHAR);

COMMIT TRANSACTION T1;
PRINT 'After commit T1: ' + CAST(@@TRANCOUNT AS VARCHAR);

-- Question 7
BEGIN TRANSACTION;

UPDATE AccountBalance SET Balance = Balance - 1000 WHERE AccountId = 101;
-- Simulate failure
-- RAISERROR('Simulated failure', 16, 1);
UPDATE AccountBalance SET Balance = Balance + 1000 WHERE AccountId = 102;

-- If no error:
COMMIT;
-- If error occurs before commit, ROLLBACK ensures atomicity.

-- Question 8
BEGIN TRANSACTION;

DECLARE @TotalBefore DECIMAL(18,2);
SELECT @TotalBefore = SUM(Balance) FROM AccountBalance;

-- Transfer
UPDATE AccountBalance SET Balance = Balance - 750 WHERE AccountId = 101;
UPDATE AccountBalance SET Balance = Balance + 750 WHERE AccountId = 102;

DECLARE @TotalAfter DECIMAL(18,2);
SELECT @TotalAfter = SUM(Balance) FROM AccountBalance;

IF @TotalBefore = @TotalAfter
    COMMIT;
ELSE
    ROLLBACK;

-- Question 9
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
BEGIN TRANSACTION;
UPDATE AccountBalance SET Balance = 99999 WHERE AccountId = 101;

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT * FROM AccountBalance WHERE AccountId = 101; 

-- Question 10
BEGIN TRANSACTION;
UPDATE AccountBalance SET Balance = Balance - 200 WHERE AccountId = 101;
COMMIT; -- After commit, data is durable even after crash.

-- Question 11
-- ??

-- Question 12
BEGIN TRANSACTION;

SAVE TRANSACTION Step1;
UPDATE AccountBalance SET Balance = Balance - 100 WHERE AccountId = 101;

SAVE TRANSACTION Step2;
UPDATE AccountBalance SET Balance = Balance + 100 WHERE AccountId = 102;

SAVE TRANSACTION Step3;
UPDATE AccountBalance SET LastUpdated = GETDATE() WHERE AccountId IN (101, 102);

COMMIT;

-- Question 13
DECLARE @RetryCount INT = 0;
WHILE @RetryCount < 3
BEGIN
    BEGIN TRY
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
        BEGIN TRANSACTION;

        UPDATE AccountBalance SET Balance = Balance - 200 WHERE AccountId = 101;
        WAITFOR DELAY '00:00:02'; -- Simulate deadlock scenario
        UPDATE AccountBalance SET Balance = Balance + 200 WHERE AccountId = 102;

        COMMIT;
        BREAK;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() = 1205 -- Deadlock error
        BEGIN
            ROLLBACK;
            SET @RetryCount = @RetryCount + 1;
            WAITFOR DELAY '00:00:01';
        END
        ELSE
        BEGIN
            ROLLBACK;
            PRINT 'Error: ' + ERROR_MESSAGE();
            BREAK;
        END
    END CATCH
END

-- Question 14
PRINT 'Start: ' + CAST(@@TRANCOUNT AS VARCHAR);
BEGIN TRANSACTION;
PRINT 'After begin: ' + CAST(@@TRANCOUNT AS VARCHAR);

    BEGIN TRANSACTION;
    PRINT 'Nested begin: ' + CAST(@@TRANCOUNT AS VARCHAR);
    COMMIT; -- Not full commit, just reduces count

PRINT 'After inner commit: ' + CAST(@@TRANCOUNT AS VARCHAR);
COMMIT;
PRINT 'After outer commit: ' + CAST(@@TRANCOUNT AS VARCHAR);

-- Question 15
BEGIN TRANSACTION;

DECLARE @OldBalance DECIMAL(18,2), @NewBalance DECIMAL(18,2);
SELECT @OldBalance = Balance FROM AccountBalance WHERE AccountId = 101;

UPDATE AccountBalance SET Balance = Balance - 50 WHERE AccountId = 101;

SELECT @NewBalance = Balance FROM AccountBalance WHERE AccountId = 101;

INSERT INTO AuditTrail (TableName, Operation, RecordId, OldValue, NewValue)
VALUES ('AccountBalance', 'UPDATE', 101, CAST(@OldBalance AS VARCHAR), CAST(@NewBalance AS VARCHAR));

COMMIT;

-- Question 16
-- Transaction 1: Commit
BEGIN TRANSACTION;
UPDATE AccountBalance SET Balance = Balance - 10 WHERE AccountId = 101;
COMMIT;

-- Transaction 2: Rollback
BEGIN TRANSACTION;
UPDATE AccountBalance SET Balance = Balance - 10 WHERE AccountId = 101;
ROLLBACK;

-- Check balances
SELECT * FROM AccountBalance WHERE AccountId = 101;

-- Question 17
BEGIN TRANSACTION;

DECLARE @WithdrawalAmount DECIMAL(18,2) = 6000;

IF @WithdrawalAmount > 5000
BEGIN
    PRINT 'Withdrawal limit exceeded.';
    ROLLBACK;
END
ELSE
BEGIN
    UPDATE AccountBalance SET Balance = Balance - @WithdrawalAmount WHERE AccountId = 101;
    COMMIT;
END

-- Question 18
BEGIN TRANSACTION;

SELECT * FROM AccountBalance WITH (UPDLOCK) WHERE AccountId = 101;

UPDATE AccountBalance SET Balance = Balance - 400 WHERE AccountId = 101;
UPDATE AccountBalance SET Balance = Balance + 400 WHERE AccountId = 102;

COMMIT;

-- Question 19
BEGIN TRY
    BEGIN TRANSACTION;

    UPDATE AccountBalance SET Balance = Balance - 1000 WHERE AccountId = 101;
    -- Simulate constraint error:
    -- INSERT INTO AccountBalance VALUES (101, 'Duplicate', 100, GETDATE());

    COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
    IF ERROR_NUMBER() = 547 -- Constraint violation
        PRINT 'Constraint error: ' + ERROR_MESSAGE();
    ELSE IF ERROR_NUMBER() = 50000 -- Custom insufficient funds error
        PRINT 'Insufficient funds.';
    ELSE
        PRINT 'General error: ' + ERROR_MESSAGE();
END CATCH

-- Question 20
SELECT 
    session_id,
    transaction_id,
    transaction_begin_time,
    transaction_type,
    transaction_state
FROM sys.dm_tran_active_transactions
WHERE transaction_type != 1; 


