using Auth.Server.Data.Entities.Custom;
using Auth.Server.Data.Entities.General;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Data.Context
{
    public interface IApplicationDbContext
    {
        #region===[ Custom Tables ]====================================
        DbSet<AspnetUser> AspnetUsers { get; set; }
        DbSet<AspNetRole> AspNetRoles { get; set; }
        DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        DbSet<AspNetClaim> AspNetClaims { get; set; }
        //DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        #endregion

        #region ===[ General Tables ]===================================
        DbSet<Address> Addresses { get; set; }
        DbSet<Asset> Assets { get; set; }
        DbSet<AssetType> AssetTypes { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<District> Districts { get; set; }
        DbSet<Locality> Localities { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<OtherInformation> OtherInformations { get; set; }
        DbSet<State> States { get; set; }

        #endregion


        #region===[Identity]==================================
        int SaveChanges();
        #endregion
    }
}
