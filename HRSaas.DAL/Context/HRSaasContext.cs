using HRSaas.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRSaas.DAL.Context
{
    public class HRSaasContext:IdentityDbContext<Personal,AppRole,int>
    {
        public HRSaasContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Advance> Advances { get; set; }
        public DbSet<Package> Package { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=tcp:db-team3.database.windows.net,1433;Initial Catalog=DbTeam3;Persist Security Info=False;User ID=dbteam3;Password=team3team3_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False; MultipleActiveResultSets = True", b => b.MigrationsAssembly("HRSaas.DAL")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
