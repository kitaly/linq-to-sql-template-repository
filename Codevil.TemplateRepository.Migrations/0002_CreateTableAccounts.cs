using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RikMigrations;
using Codevil.TemplateRepository.Migrations;

[assembly: Migration(2, Type = typeof(CreateTableAccounts), ModuleName = MigrationData.ModuleName)]
namespace Codevil.TemplateRepository.Migrations
{
    public class CreateTableAccounts : IMigration
    {
        #region IMigration Members

        public void Down(DbProvider db)
        {
            db.DropTable("ACCOUNTS");
        }

        public void Up(DbProvider db)
        {
            Table users = db.AddTable("ACCOUNTS");
            users.AddColumn<int>("id").NotNull().PrimaryKey().AutoGenerate();
            users.AddColumn<long>("number").NotNull();
            users.AddColumn<short>("agency").NotNull();
            users.AddColumn<int>("person_id").NotNull().References("PEOPLE", "id");
            users.Save();
        }

        #endregion
    }
}
