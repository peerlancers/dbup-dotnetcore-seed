using DbUp.Builder;
using DbUp.Helpers;

using System.Reflection;

namespace DbUp.Migration
{
    public class Migrator : IMigrator
    {
        // Idempotency indicators must be a unique string 
        // to ensure we don't accidentally execute scripts that has similar string in the name.
        private string idempotentScriptIndicator = "idempotent";

        private string connectionString;

        private UpgradeEngineBuilder builder;

        public Migrator(IDatabaseConnectionSettings settings)
        {
            connectionString = buildPostgresConnectionString(settings);
            builder = DeployChanges.To.PostgresqlDatabase(connectionString);
        }

        /// <summary>
        /// Starts the migration process
        /// </summary>
        public void Start()
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);
            RunNonIdempotentMigrationScripts();
            RunIdempotentMigrationScripts();
        }

        /// <summary>
        /// Executes unsafe scripts that should only be run once.
        /// </summary>
        private void RunNonIdempotentMigrationScripts()
        {
            var upgrader = builder
                .WithScriptsEmbeddedInAssembly(
                    Assembly.GetExecutingAssembly(),
                    fileName => !fileName.ContainsKeyword(idempotentScriptIndicator))
                .Build();

            upgrader.PerformUpgrade();
        }

        /// <summary>
        /// Executes safe scripts that can be run repeatedly.
        /// </summary>
        private void RunIdempotentMigrationScripts()
        {
            var upgrader = builder
                .WithScriptsEmbeddedInAssembly(
                    Assembly.GetExecutingAssembly(),
                    fileName => fileName.ContainsKeyword(idempotentScriptIndicator))
                .JournalTo(new NullJournal())
                .Build();

            upgrader.PerformUpgrade();
        }

        /// <summary>
        /// Builds a valid Postgres connection string.
        /// </summary>
        /// <param name="settings">Connection settings</param>
        /// <returns>Connection string</returns>
        private static string buildPostgresConnectionString(IDatabaseConnectionSettings settings)
        {
            return $"User ID={settings.User};Password={settings.Password};Host={settings.Host};Port={settings.Port};Database={settings.Name};Pooling={settings.Pooling};";
        }
    }
}
