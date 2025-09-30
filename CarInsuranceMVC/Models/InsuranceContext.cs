using System.Data.Entity;

namespace CarInsuranceMVC.Models
{
    public class InsuranceContext : DbContext
    {
        // Use a DB file inside App_Data (so you can email the .mdf/.ldf)
        public InsuranceContext() : base("DefaultConnection") { }

        public DbSet<Insuree> Insurees { get; set; }
    }
}

