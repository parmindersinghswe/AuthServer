using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("AssetTypes", Schema = "GEN")]
    public class AssetType : AssetTypeModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }

        public IEnumerable<Asset> Assets { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }
        public static AssetType GetNew()
        {
            return new AssetType()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
