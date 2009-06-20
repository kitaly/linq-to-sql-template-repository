using Codevil.TemplateRepository.Entities;

namespace Codevil.TemplateRepository.Factories
{
    public interface IEntityFactory
    {
        DataEntity Create(object row);
    }
}
