using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Codevil.TemplateRepository.Entities;

namespace Codevil.TemplateRepository.Factories
{
    public interface IDataContextFactory
    {
        DataContext Create();
        UnitOfWork CreateUnitOfWork();
    }
}
