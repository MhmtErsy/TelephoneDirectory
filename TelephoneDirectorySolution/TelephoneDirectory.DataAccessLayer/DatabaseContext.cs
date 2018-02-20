using System.Data.Entity;
using TelephoneDirectory.Entities;

namespace TelephoneDirectory.DataAccessLayer
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                   .HasOptional(c => c.Director)
                   .WithMany()
                   .HasForeignKey(c => c.DirectorId);

            
        }
    }
}