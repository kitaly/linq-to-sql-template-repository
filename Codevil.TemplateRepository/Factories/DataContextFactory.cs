using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Controllers;

namespace Codevil.TemplateRepository.Factories
{
    public abstract class DataContextFactory : IDataContextFactory
    {
        public abstract DataContext Create();

        public virtual UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(this.Create());
        }
    }
}
