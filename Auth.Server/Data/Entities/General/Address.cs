using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Addresses", Schema = "GEN")]
    public class Address : AddressModel
    {
        public Locality Locality { get; set; }
        public Contact Contact { get; set; }
        public static Address GetNew()
        {
            return new Address()
            {
                ////  CreatedOn = DateTime.Now,
                // ModifiedOn = DateTime.Now
            };
        }
    }
}
