CREATE TABLE IF NOT EXISTS companies (
	id UUID NOT NULL,
	name TEXT,
	created_on TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	last_updated_on TIMESTAMP WITHOUT TIME ZONE,
	description TEXT,
	-- Specify keys
	CONSTRAINT company_pkey PRIMARY KEY (id),
	CONSTRAINT company_name_ukey UNIQUE (name)
);
-- Create indexes
CREATE INDEX IF NOT EXISTS company_name_idx ON companies USING btree (LOWER(name) varchar_ops ASC NULLS LAST);