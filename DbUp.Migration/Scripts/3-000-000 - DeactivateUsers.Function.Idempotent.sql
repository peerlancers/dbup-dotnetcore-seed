CREATE OR REPLACE FUNCTION deactivate_users(users uuid[]) 
RETURNS void AS $$
BEGIN
    UPDATE 
		users 
	SET 
		status = 3
	WHERE 
		id IN (SELECT id FROM unnest(users) AS id) AND status < 2;
END;
$$ LANGUAGE plpgsql;