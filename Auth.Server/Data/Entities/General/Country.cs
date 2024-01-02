using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    //[Table("Countries", Schema = "GEN")]
    public class Country : CountryModel
    {

        //[ForeignKey("CreatedByUser")]
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        //[ForeignKey("ModifiedByUser")]
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }

        public ICollection<State> States { get; set; }
        public static Country GetNew()
        {
            return new Country()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsDeleted = false
            };
        }
    }
}
