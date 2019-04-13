# DbUp - Migration Seed Project

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

This is a starter solution for creating database migration using [DbUp](https://dbup.readthedocs.io/en/latest/) and embedded SQL scripts in .NET Core. You can use it to quickly create a database migration process by simply adding migrations scripts and providing database connection settings.

## Getting Started
### Environment Variables
 `DB_HOST` - hostname of the database
 `DB_NAME` - database name
 `DB_USER` - user name
 `DB_PASSWORD` - user password
 `DB_PORT` - database port
 `DB_POOLING` - database pooling (true or false)
 
### Visual Studio
1. Clone the `dbup-seed` repository.
2. Set your database connection parameters in the `environmentVariables` in the `DbUp.Migration.Runner` project.
3. Build the solution using Visual Studio
4. Run the `DbUp.Migration.Runner`.

### Using Power Shell
1. `> git clone https://github.com/peerlancers/dbup-seed.git`
2. `> cd .\dbup-seed\`
4. `> dotnet build`
5. `> dotnet publish DbUp.Migration.Runner/DbUp.Migration.Runner.csproj -c release -o out`
6. `> cd DbUp.Migration.Runner\out`
7. Set environment variables using `> $env:VAR_NAME = "VALUE"` 
8. `> dotnet .\DbUp.Migration.Runner.dll`

### Using Bash
1. `> git clone https://github.com/peerlancers/dbup-seed.git`
2. `> cd dbup-seed`
3. `> bash build.sh`
4. Set environment variables using `> export VAR_NAME="VALUE"` 
5. `> bash run.sh`

## SQL Scripts

This solution is intended as forward-only migration and down migration scripts are intentionally discouraged in this design.
Running the scripts sequentially is critical to ensure the integrity of the database. We can accomplish this using file naming conventions. 
### Naming Script Files
An opinionated standard we used is `prefix - description.sql`
`0-000-000` up to `0-999-999` prefixes can be used for database initialization and user configurations.
`1-000-000` up to `2-999-999` prefixes can be used for table creations and modifications.
`3-000-000` up to `3-999-999` prefixes can be used for views.
`4-000-000` up to `n` prefixes can be used for functions and stored procedures.

You can use whatever naming convention/standard you want as long as you keep in mind the sequence of the scripts.

### Script Idempotency
Our migration solution should be able to identify script files that are safe to run, which we call idempotent files. We can tag a file as idempotent by adding a unique identifer in the file name. By default you can use `idempotent` as part of the file name (which you can change to your preference), to tell the migrator that it is safe to run the script on every migration.

#### Examples of idempotent scripts
The script below can be run repeatedly and would return the same result.
```
CREATE OR REPLACE VIEW pending_users AS SELECT * FROM users WHERE status = 0
```

#### Examples of non-idempotent scripts
The script below will throw an error when run twice as `users` table would already exist on the second run.
```
CREATE TABLE users (id UUID NOT NULL, name TEXT, CONSTRAINT user_pkey PRIMARY KEY (id));
```

