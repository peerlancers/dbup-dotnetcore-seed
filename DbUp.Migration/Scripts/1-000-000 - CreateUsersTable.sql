CREATE TABLE IF NOT EXISTS users (
	id UUID NOT NULL,
	username TEXT,
	created_on TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	updated_on TIMESTAMP WITHOUT TIME ZONE,
	first_name TEXT,
	last_name TEXT,
	email TEXT,
	company_id TEXT,
	company_name TEXT,
	status INTEGER NOT NULL,
	-- Specify keys
	CONSTRAINT user_pkey PRIMARY KEY (id),
	CONSTRAINT user_username_ukey UNIQUE (username)
);
-- Create indexes
CREATE INDEX IF NOT EXISTS user_username_idx ON users USING btree (LOWER(username) varchar_ops ASC NULLS LAST);