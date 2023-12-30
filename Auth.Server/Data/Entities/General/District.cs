using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Districts", Schema = "GEN")]
    public class District : DistrictModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public State State { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }
        public ICollection<Locality> Localities { get; set; }
        public static District GetNew()
        {
            return new District()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
