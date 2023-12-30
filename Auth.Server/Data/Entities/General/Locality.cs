using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Locals", Schema = "GEN")]
    public class Locality : LocalityModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsApproaved { get; set; }
        public bool IsBlocked { get; set; }
        public District District { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public static Locality GetNew()
        {
            return new Locality()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
