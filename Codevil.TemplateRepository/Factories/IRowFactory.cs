using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Codevil.TemplateRepository.Factories
{
    public interface IRowFactory
    {
        object Create(Type rowType);
        object CreateTable(Type rowType, DataContext context);
    }
}
