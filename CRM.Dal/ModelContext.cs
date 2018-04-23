using CRM.Model;
using System.Data.Entity;

namespace CRM.Dal
{
    public class ModelContext : DbContext
    {
        public ModelContext() : base("name = CRMConnStrConnection") { }
        public DbSet<User> Users { get; set; }
    }
}
