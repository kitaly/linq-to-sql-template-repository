using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Codevil.TemplateRepository.Factories
{
    public abstract class RowFactory <TDataContext> : IRowFactory<TDataContext>
        where TDataContext : DataContext
    {
        #region IRowFactory<TDataContext> Members

        public virtual object Create(Type rowType)
        {
            return Activator.CreateInstance(rowType);
        }

        public abstract object CreateTable(Type rowType, TDataContext context);

        #endregion
    }
}
