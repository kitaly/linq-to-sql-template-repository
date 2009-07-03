using System.Data.Linq;
using Codevil.TemplateRepository.Handlers;

namespace Codevil.TemplateRepository.Factories
{
    public abstract class DataContextFactory : IDataContextFactory
    {
        public abstract DataContext Create();

        public virtual Transaction CreateTransaction()
        {
            return new Transaction(this.Create());
        }
    }
}
