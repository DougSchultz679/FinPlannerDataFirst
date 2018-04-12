namespace FinPlannerDataFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Household
    {
        public Household()
        {
            Budgets = new HashSet<Budget>();
            Invites = new HashSet<Invite>();
            PersonalAccounts = new HashSet<PersonalAccount>();
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }
        public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
