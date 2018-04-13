using CRM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dal
{
    public class ModelContext : System.Data.Entity.DbContext
    {
        public ModelContext() : base("name = CRMConnStrConnection") { }
        public DbSet<User> Users { get; set; }
    }
}
