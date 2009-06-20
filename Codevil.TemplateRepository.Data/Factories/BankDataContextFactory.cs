using System.Data.Linq;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Data.Factories
{
    public class BankDataContextFactory : DataContextFactory
    {
        public override DataContext Create()
        {
            return new BankDataContext(@"Data Source=MARVIN\SQLEXPRESS;Initial Catalog=bank;Integrated Security=True");
        }
    }
}
