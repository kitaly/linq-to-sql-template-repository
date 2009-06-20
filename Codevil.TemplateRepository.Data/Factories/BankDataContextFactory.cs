using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Entities;

namespace Codevil.TemplateRepository.Data.Factories
{
    public class BankDataContextFactory : DataContextFactory
    {
        public override DataContext Create()
        {
            return new BankDataContext(@"Data Source=MARVIN\SQLEXPRESS;Initial Catalog=bank;Integrated Security=True");
        }
    }
}
