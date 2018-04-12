namespace FinPlannerDataFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public Category()
        {
            BudgetItems = new HashSet<BudgetItem>();
            Transactions = new HashSet<Transaction>();
            Households = new HashSet<Household>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Household> Households { get; set; }
    }
}
