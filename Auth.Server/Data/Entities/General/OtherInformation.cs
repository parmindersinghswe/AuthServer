using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("OtherInformation", Schema = "GEN")]
    public class OtherInformation : OtherInformationModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Contact Contact { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }
        public static OtherInformation GetNew()
        {
            return new OtherInformation()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
