CREATE OR REPLACE VIEW pending_users AS 
SELECT 
    *
FROM
    users
WHERE
    status = 0