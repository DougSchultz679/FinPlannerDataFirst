namespace FinPlannerDataFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Budget
    {
        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int HouseholdId { get; set; }

        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
        public virtual Household Household { get; set; }
    }
}
