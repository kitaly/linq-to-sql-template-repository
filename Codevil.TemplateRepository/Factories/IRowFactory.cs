using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Codevil.TemplateRepository.Factories
{
    public interface IRowFactory<TDataContext>
        where TDataContext : DataContext
    {
        object Create(Type rowType);
        object CreateTable(Type rowType, TDataContext context);
    }
}
