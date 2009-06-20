using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Data;
using System.Data.Linq;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Model.Factories
{
    public class RowFactory : Codevil.TemplateRepository.Factories.RowFactory<BankDataContext>
    {
        //public object Create(Type rowType)
        //{
        //    switch (rowType.Name)
        //    {
        //        case "ACCOUNT":
        //            return new ACCOUNT();
        //        case "PERSON":
        //            return new PERSON();
        //        default:
        //            throw new ArgumentException("Invalid type: " + rowType.Name, "rowType");
        //    }
        //}

        public override object CreateTable(Type rowType, BankDataContext context)
        {
            switch (rowType.Name)
            {
                case "ACCOUNT":
                    return context.ACCOUNTs;
                case "PERSON":
                    return context.PEOPLE;
                default:
                    throw new ArgumentException("Invalid type: " + rowType.Name, "rowType");
            }
        }
    }
}
