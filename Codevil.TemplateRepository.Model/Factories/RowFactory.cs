using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Data;
using System.Data.Linq;
using Codevil.TemplateRepository.Factories;
using System.Collections;

namespace Codevil.TemplateRepository.Model.Factories
{
    public class RowFactory : Codevil.TemplateRepository.Factories.RowFactory<BankDataContext>
    {
        public override System.Collections.Hashtable MapPluralInflections()
        {
            Hashtable hashtable = base.MapPluralInflections();

            hashtable.Add("PERSON", "PEOPLE");

            return hashtable;
        }
    }
}
