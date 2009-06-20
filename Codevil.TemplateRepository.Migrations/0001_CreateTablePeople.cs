using Codevil.TemplateRepository.Migrations;
using RikMigrations;

[assembly: Migration(1, Type = typeof(CreateTablePeople), ModuleName = MigrationData.ModuleName)]
namespace Codevil.TemplateRepository.Migrations
{
    public class CreateTablePeople : IMigration
    {
        #region IMigration Members

        public void Down(DbProvider db)
        {
            db.DropTable("PEOPLE");
        }

        public void Up(DbProvider db)
        {
            Table users = db.AddTable("PEOPLE");
            users.AddColumn<int>("id").NotNull().PrimaryKey().AutoGenerate();
            users.AddColumn<string>("name", 100).NotNull().NotUnicode();
            users.AddColumn<string>("document", 20).NotNull().NotUnicode();
            users.AddColumn<string>("email", 50).NotUnicode();
            users.Save();
        }

        #endregion
    }
}
