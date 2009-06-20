using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Collections;
using System.Reflection;

namespace Codevil.TemplateRepository.Factories
{
    public class RowFactory : IRowFactory
    {
        public virtual object Create(Type rowType)
        {
            object row = null;

            row = Activator.CreateInstance(rowType);
            
            return row;
        }

        public virtual object CreateTable(Type rowType, DataContext context)
        {
            object table = null;

            Hashtable pluralInflections = this.MapPluralInflections();

            string pluralizedName = rowType.Name + "s";

            if (pluralInflections.ContainsKey(rowType.Name))
            {
                pluralizedName = pluralInflections[rowType.Name].ToString();
            }

            PropertyInfo property = context.GetType().GetProperty(pluralizedName);

            if (property == null)
            {
                throw new MissingMemberException(context.GetType().FullName, pluralizedName);
            }

            table = property.GetValue(context, null);

            return table;
        }

        public virtual Hashtable MapPluralInflections()
        {
            return new Hashtable();
        }
    }
}
