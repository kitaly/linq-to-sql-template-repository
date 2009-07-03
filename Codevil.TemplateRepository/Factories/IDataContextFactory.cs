using System.Data.Linq;
using Codevil.TemplateRepository.Handlers;

namespace Codevil.TemplateRepository.Factories
{
    public interface IDataContextFactory
    {
        DataContext Create();
        Transaction CreateTransaction();
    }
}
