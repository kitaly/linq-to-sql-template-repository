﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Data.Factories;
using System.Data.Linq;
using Codevil.TemplateRepository.Model.Factories;
using Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public class AccountsRepository : Repository<ACCOUNT, Account, BankDataContext>
    {
        public AccountsRepository()
            : base(new BankDataContextFactory(), new EntityFactory(), new RowFactory())
        {
        }

        protected override ACCOUNT FindEntity(Account entity, BankDataContext context)
        {
            return FindSingle(a => a.Id == entity.Id, context);
        }

        protected override void Create(Account entity, BankDataContext context)
        {
            ACCOUNT row = new ACCOUNT();
            this.MapToRow(entity, row);
            Table<ACCOUNT> table = context.ACCOUNTs;
            table.InsertOnSubmit(row);
            context.SubmitChanges();
        }

        protected override void MapToRow(Account entity, ACCOUNT row)
        {
            row.Agency = entity.Agency;
            row.Number = entity.Number;
            row.OwnerId = entity.OwnerId;
        }

        protected override void MapToEntity(ACCOUNT row, Account entity)
        {
            entity.Id = row.Id;
        }
    }
}
