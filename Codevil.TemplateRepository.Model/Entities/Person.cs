using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Entities
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public IList<Account> accounts;
        public IList<Account> Accounts
        {
            get
            {
                if (this.accounts == null)
                {
                    this.accounts = this.AccountsRepository.Find(a => a.OwnerId == this.Id);
                }

                return this.accounts;
            }
        }
        public IRepository<PERSON, Person, BankDataContext> PeopleRepository { get; set; }
        public IRepository<ACCOUNT, Account, BankDataContext> AccountsRepository { get; set; }

        public Person()
            : base()
        {
            this.AccountsRepository = new AccountsRepository();
            this.PeopleRepository = new PeopleRepository();
        }

        public Person(PERSON row)
            : this()
        {
            this.Name = row.Name;
            this.Document = row.Document;
            this.Email = row.Email;
        }
    }
}
