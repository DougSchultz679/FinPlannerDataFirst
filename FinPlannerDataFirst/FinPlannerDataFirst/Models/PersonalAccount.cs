namespace FinPlannerDataFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PersonalAccount
    {
        public PersonalAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal ReconciledBalance { get; set; }

        [StringLength(128)]
        public string CreatedById { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
