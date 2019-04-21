CREATE TABLE IF NOT EXISTS users (
	id UUID NOT NULL,
	username TEXT,
	created_on TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	last_updated_on TIMESTAMP WITHOUT TIME ZONE,
	first_name TEXT,
	last_name TEXT,
	email TEXT,
	company_id UUID,
	status INTEGER NOT NULL,
	-- Specify keys
	CONSTRAINT user_pkey PRIMARY KEY (id),
	CONSTRAINT user_username_ukey UNIQUE (username),
	CONSTRAINT user_company_fkey FOREIGN KEY (company_id) REFERENCES companies(id) ON DELETE NO ACTION
);
-- Create indexes
CREATE INDEX IF NOT EXISTS user_username_idx ON users USING btree (LOWER(username) varchar_ops ASC NULLS LAST);
