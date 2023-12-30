using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Auth.Server.Data.Entities.Custom;
using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Members", Schema = "GEN")]
    public class Member : MemberModel
    {
        public int? ProfilePictureId { get; set; }
        public int? OtherInfo { get; set; }

        public int? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        [Required, DefaultValue(false)]
        public bool IsApproaved { get; set; }

        [Required, DefaultValue(false)]
        public bool IsBlocked { get; set; }

        [Required, DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public AspnetUser AspnetUser { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }
        public ICollection<Member> CreatedMembers { get; set; }
        public ICollection<Member> ModifiedMembers { get; set; }
        public Asset ProfilePicture { get; set; }
        public string ProfilePicturePath { get; set; }
        public ICollection<AssetType> CreatedAssetTypes { get; set; }
        public ICollection<AssetType> ModifiedAssetTypes { get; set; }
        public ICollection<Asset> CreatedAssets { get; set; }
        public ICollection<Asset> ModifiedAssets { get; set; }
        public ICollection<Country> CreatedCountries { get; set; }
        public ICollection<Country> ModifiedCountries { get; set; }
        public ICollection<State> CreatedStates { get; set; }
        public ICollection<State> ModifiedStates { get; set; }
        public ICollection<District> CreatedDistricts { get; set; }
        public ICollection<District> ModifiedDistricts { get; set; }
        public ICollection<Locality> CreatedLocalities { get; set; }
        public ICollection<Locality> ModifiedLocalities { get; set; }
        public ICollection<Contact> CreatedContacts { get; set; }
        public ICollection<Contact> ModifiedContacts { get; set; }
        public ICollection<OtherInformation> CreatedInformations { get; set; }
        public ICollection<OtherInformation> ModifiedInformations { get; set; }

        public static Member GetNew()
        {
            return new Member()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
