using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Map;
using System.Data.Entity;

namespace SpaUserControl.Infraestructure.Data
{
    public class DbDataContext : DbContext
    {
        public DbDataContext()
            :base("AppConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<User> Users{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
