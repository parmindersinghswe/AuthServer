using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Assets", Schema = "GEN")]
    public class Asset : AssetModel
    {

        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public AssetType AssetType { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }

        public Member ProfilePictureMember { get; set; }

        public static Asset GetNew()
        {
            return new Asset()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
