using Auth.Server.Data.Entities.Custom;
using Auth.Server.Data.Entities.General;
using Auth.Server.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Data.Context
{

    public class ApplicationDbContext : IdentityDbContext<AspnetUser, AspNetRole, int, IdentityUserClaim<int>, AspNetUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IApplicationDbContext //IdentityDbContext<AspnetUser, IdentityRole<uint>, uint>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        #region===[ Custom Tables ]====================================
        public DbSet<AspnetUser> AspnetUsers { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<AspNetClaim> AspNetClaims { get; set; }
       // public DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        #endregion

        #region ===[ General Tables ]===================================
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<OtherInformation> OtherInformations { get; set; }
        public DbSet<State> States { get; set; }
      
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configure();
            modelBuilder.SeedData();
        }

        private static DbContextOptions<ApplicationDbContext> GetOptions()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = @"../mydatabase.db"
            };
            var connectionString = connectionStringBuilder.ToString();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite(connectionString);

            return builder.Options;
        }
    }
}
