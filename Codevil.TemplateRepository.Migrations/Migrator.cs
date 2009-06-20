using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RikMigrations;

namespace Codevil.TemplateRepository.Migrations
{
    public static class Migrator
    {
        private static bool migrated = false;

        public static void Migrate()
        {
            if (!migrated)
            {
                DbProvider.DefaultConnectionString = @"Data Source=MARVIN\SQLEXPRESS;Initial Catalog=bank;Integrated Security=True";
                MigrationManager.UpgradeMax(typeof(Migrator).Assembly);
                migrated = true;
            }
        }
    }
}
