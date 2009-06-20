using RikMigrations;

namespace Codevil.TemplateRepository.Migrations
{
    public static class Migrator
    {
        private static bool migrated = false;

        public static void Up()
        {
            if (!migrated)
            {
                DbProvider.DefaultConnectionString = @"Data Source=MARVIN\SQLEXPRESS;Initial Catalog=bank;Integrated Security=True";
                MigrationManager.UpgradeMax(typeof(Migrator).Assembly);
                migrated = true;
            }
        }

        public static void Down()
        {
            if (migrated)
            {
                DbProvider.DefaultConnectionString = @"Data Source=MARVIN\SQLEXPRESS;Initial Catalog=bank;Integrated Security=True";
                MigrationManager.MigrateTo(typeof(Migrator).Assembly, MigrationData.ModuleName, 0);
                migrated = false;
            }
        }
    }
}
