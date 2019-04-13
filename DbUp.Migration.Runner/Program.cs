using Microsoft.Extensions.DependencyInjection;

namespace DbUp.Migration.Runner
{
    public static partial class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDatabaseConnectionSettings, EnvironmentSettings>();
            var settings = services.BuildServiceProvider().GetService<IDatabaseConnectionSettings>();
            services.AddSingleton<IMigrator>(provider => new Migrator(settings));

            var databaseMigrator = services.BuildServiceProvider().GetService<IMigrator>();

            databaseMigrator.Start();
        }
    }
}
