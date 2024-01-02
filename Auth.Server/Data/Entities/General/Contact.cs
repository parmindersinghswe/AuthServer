using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Contacts", Schema = "GEN")]
    public class Contact : ContactModel
    {

        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Address Address { get; set; }

        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }

        public OtherInformation OtherInformation { get; set; }
        public static Contact GetNew()
        {
            return new Contact()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
