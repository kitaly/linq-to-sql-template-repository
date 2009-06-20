using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Migrations;

namespace Codevil.TemplateRepository.Test.Data
{
    public class DatabaseDependentTest
    {
        public DatabaseDependentTest()
        {
            Migrator.Down();
            Migrator.Up();
        }
    }
}
