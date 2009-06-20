using System.Data.Linq;
using Codevil.TemplateRepository.Controllers;

namespace Codevil.TemplateRepository.Factories
{
    public interface IDataContextFactory
    {
        DataContext Create();
        UnitOfWork CreateUnitOfWork();
    }
}
