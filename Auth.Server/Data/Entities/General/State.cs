using Auth.Server.Data.Entities.General.Parent;

namespace Auth.Server.Data.Entities.General
{
    ////[Table("States", Schema = "GEN")]
    public class State : StateModel
    {
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Country Country { get; set; }
        public Member CreatedBy { get; set; }
        public Member ModifiedBy { get; set; }

        public ICollection<District> Districts { get; set; }
        public static State GetNew()
        {
            return new State()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
