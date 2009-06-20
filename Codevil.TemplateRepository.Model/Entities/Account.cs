using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Entities
{
    public class Account : Entity
    {
        public long Number { get; set; }
        public short Agency { get; set; }
        public int OwnerId { get; set; }
        private Person owner;
        public Person Owner
        {
            get
            {
                if (owner == null)
                {
                    this.owner = this.PeopleRepository.FindSingle(p => p.Id == this.OwnerId);
                }

                return this.owner;
            }
            set
            {
                this.OwnerId = value.Id;
                this.owner = value;
            }
        }
        public IRepository<ACCOUNT, Account, BankDataContext> AccountsRepository { get; set; }
        public IRepository<PERSON, Person, BankDataContext> PeopleRepository { get; set; }

        public Account()
            : base()
        {
            this.AccountsRepository = new AccountsRepository();
            this.PeopleRepository = new PeopleRepository();
        }

        public Account(ACCOUNT row)
            : this()
        {
            this.Id = row.Id;
            this.Number = row.Number;
            this.OwnerId = row.OwnerId;
            this.Agency = row.Agency;
        }

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj is Account)
            {
                Account that = obj as Account;

                areEqual =
                    this.Id == that.Id &&
                    this.Agency == that.Agency &&
                    this.Number == that.Number &&
                    this.OwnerId == that.OwnerId;
            }

            return areEqual;
        }
    }
}
