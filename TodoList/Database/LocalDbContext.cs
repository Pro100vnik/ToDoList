using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Database
{
    public class LocalDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public LocalDbContext(string connectionString) : base(connectionString)
        {

        }

        public static LocalDbContext FactoryMethod(string connectionString)
        {
            return new LocalDbContext(connectionString);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
            .Remove<PluralizingTableNameConvention>();
        }
    }
}
