using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Data
{
    public class DatabaseDependentTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Migrator.Down();
            Migrator.Up();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Migrator.Down();
            Migrator.Up();
        }
    }
}
