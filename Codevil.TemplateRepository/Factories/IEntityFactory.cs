using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Entities;

namespace Codevil.TemplateRepository.Factories
{
    public interface IEntityFactory
    {
        DataEntity Create(object row);
    }
}
