using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Codevil.TemplateRepository.Factories
{
    public interface IDataContextFactory<TDataContext>
        where TDataContext : DataContext
    {
        TDataContext Create();
    }
}
