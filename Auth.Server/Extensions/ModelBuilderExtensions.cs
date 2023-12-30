using Auth.Server.Data.Entities.Custom;
using Auth.Server.Data.Entities.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Auth.Server.DataModels.DateModelConstants;

namespace Auth.Server.Extensions
{
    public static class ModelBuilderExtensions
    {
        delegate void EntityConfigurations(ModelBuilder modelBuilder);

        private static EntityConfigurations? configurations;

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            SeedUserData(modelBuilder);
            //SeedMemberData(modelBuilder);
            SeedRoleData(modelBuilder);
            SeedUserRoleData(modelBuilder);
            SeedClaimData(modelBuilder);
            SeedLocationsData(modelBuilder);
            configurations = null;
		}


        public static void Configure(this ModelBuilder modelBuilder)
        {
            configurations = new EntityConfigurations(AspNetRoleClaimsConfiguration);
            configurations += AspNetUserRoleConfiguration;
            configurations += MemberConfiguration;
            configurations += AspNetRoleClaimsConfiguration;
            //  configurations += MySqlConfiguration;
            configurations += AssetTypeConfiguration;
            configurations += AssetConfiguration;
            configurations += CountryConfiguration;
            configurations += StateConfiguration;
            configurations += DistrictConfiguration;
            configurations += LocalityConfiguration;
            configurations += AddressConfiguration;
            configurations += ContactConfiguration;
            configurations += OtherInfoConfiguration;
            configurations += ServiceConfiguration;
            configurations += ServiceAssetConfiguration;
            configurations(modelBuilder);
        }

        #region ===[ Seed Initial Data ]====================================
        private static void SeedRoleData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>().HasData(new List<AspNetRole>
            {
              new AspNetRole {
                Id = 1,
                Name = Role.SuperAdmin.ToString(),
                NormalizedName =  Role.SuperAdmin.ToString().ToUpper()
              },
              new AspNetRole {
                Id = 2,
                Name =  Role.Admin.ToString(),
                NormalizedName =  Role.Admin.ToString().ToUpper()
              },
               new AspNetRole {
                Id = 3,
                Name =  Role.User.ToString(),
                NormalizedName =  Role.User.ToString().ToUpper()
              }
            });
        }
        private static void SeedClaimData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRoleClaim<int>>().HasData(new IdentityRoleClaim<int> { Id = 1, RoleId = 1, ClaimType = "Claims", ClaimValue = "Claims"});
        }
        private static void SeedUserData(ModelBuilder modelBuilder)
        {
            AspnetUser user = new()
            {
                Id = 1,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "myemail@mymail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<AspnetUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456");
            modelBuilder.Entity<AspnetUser>().HasData(user);
        }

        private static void SeedMemberData(ModelBuilder modelBuilder)
        {
            Member member = new()
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "SharePoint",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedById = 1,
                ModifiedById = 1,
                IsDeleted = false
            };
            modelBuilder.Entity<Member>().HasData(member);
        }
        private static void SeedUserRoleData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUserRole>().HasData(
            new AspNetUserRole
            {
                RoleId = 1,
                UserId = 1
            }//,
            //new AspNetUserRole
            //{
            //    RoleId = 2,
            //    UserId = 1
            //}
            );
        }

        private static void SeedLocationsData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Country>().HasData(SeedableData.Countries);
            //modelBuilder.Entity<State>().HasData(SeedableData.States);
            //modelBuilder.Entity<District>().HasData(SeedableData.Districts);
            //modelBuilder.Entity<Locality>().HasData(SeedableData.Localities);
        }

        #endregion

        #region===[ Database Configurations ]========================
        private static void MySqlConfiguration(ModelBuilder modelBuilder)
        {
            int stringMaxLength = 300 /* something like 100*/;
            // User IdentityRole and IdentityUser in case you haven't extended those classes
            //builder.Entity<Role>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
            //builder.Entity<Role>(x => x.Property(m => m.NormalizedName).HasMaxLength(stringMaxLength));
            //builder.Entity<User>(x => x.Property(m => m.NormalizedUserName).HasMaxLength(stringMaxLength));

            // We are using int here because of the change on the PK
            modelBuilder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            modelBuilder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(stringMaxLength));

            // We are using int here because of the change on the PK
            modelBuilder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            modelBuilder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
        }

        private static void AspNetUserRoleConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUserRole>().HasOne(x => x.Role).WithMany(y => y.UserRoles).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.RoleId);
            modelBuilder.Entity<AspNetUserRole>().HasOne(x => x.User).WithMany(y => y.UserRoles).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.UserId);
        }
        private static void MemberConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedMembers).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById);
            modelBuilder.Entity<Member>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedMembers).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById);
            modelBuilder.Entity<Member>().HasOne(x => x.AspnetUser).WithOne(y => y.Member).OnDelete(DeleteBehavior.NoAction).HasForeignKey<Member>(z => z.Id);
            modelBuilder.Entity<Member>().HasOne(x => x.ProfilePicture).WithOne(y => y.ProfilePictureMember).OnDelete(DeleteBehavior.NoAction).HasForeignKey<Member>(z => z.ProfilePictureId);
        }
        private static void AspNetRoleClaimsConfiguration(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AspNetRole>().HasMany(x => x.RoleClaims).WithOne().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.RoleId).IsRequired();
            //modelBuilder.Entity<IdentityRoleClaim<int>>().HasOne(x => x.Role).WithMany(y => y.RoleClaims).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.RoleId).IsRequired();
        }
        private static void AssetTypeConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetType>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedAssetTypes).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<AssetType>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedAssetTypes).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
        }
        private static void AssetConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().HasOne(x => x.AssetType).WithMany(y => y.Assets).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.TypeId);
            modelBuilder.Entity<Asset>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedAssets).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<Asset>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedAssets).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();

        }
        private static void CountryConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedCountries).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<Country>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedCountries).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();

        }
        private static void StateConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasOne(x => x.Country).WithMany(y => y.States).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.CountryId);
            modelBuilder.Entity<State>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedStates).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<State>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedStates).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
        }
        private static void DistrictConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>().HasOne(x => x.State).WithMany(y => y.Districts).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.StateId);
            modelBuilder.Entity<District>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedDistricts).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<District>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedDistricts).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
        }
        private static void LocalityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locality>().HasOne(x => x.District).WithMany(y => y.Localities).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.DistrictId);
            modelBuilder.Entity<Locality>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedLocalities).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<Locality>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedLocalities).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
        }
        private static void AddressConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasOne(x => x.Locality).WithMany(y => y.Addresses).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.LocalityId);
        }
        private static void ContactConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasOne(x => x.Address).WithOne(y => y.Contact).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Contact>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedContacts).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<Contact>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedContacts).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
        }
        private static void OtherInfoConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtherInformation>().HasOne(x => x.Contact).WithOne(y => y.OtherInformation).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OtherInformation>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedInformations).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            modelBuilder.Entity<OtherInformation>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedInformations).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
        }
        private static void ServiceConfiguration(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GeneralService>().HasOne(x => x.CreatedBy).WithMany(y => y.CreatedServices).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.CreatedById).IsRequired();
            //modelBuilder.Entity<GeneralService>().HasOne(x => x.ModifiedBy).WithMany(y => y.ModifiedServices).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModifiedById).IsRequired();
            //modelBuilder.Entity<GeneralService>().HasOne(x => x.Member).WithMany(y => y.Services).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.MemberId).IsRequired();
            //modelBuilder.Entity<GeneralService>().HasOne(x => x.Category).WithMany(y => y.Services).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.TypeId).IsRequired();
            //modelBuilder.Entity<GeneralService>().HasOne(x => x.ModuleType).WithMany(y => y.Services).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ModuleTypeId).IsRequired();
        }
        private static void ServiceAssetConfiguration(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ServiceAsset>().HasOne(x => x.Service).WithMany(y => y.Assets).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.ServiceId).IsRequired();
            //modelBuilder.Entity<ServiceAsset>().HasOne(x => x.Asset).WithMany(y => y.ServiceAssets).OnDelete(DeleteBehavior.NoAction).HasForeignKey(z => z.AssetId).IsRequired();
        }
        #endregion
    }
}
