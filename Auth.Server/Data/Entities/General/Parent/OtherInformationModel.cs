using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class OtherInformationModel : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContactId { get; set; }

        [Required, MinLength(10), MaxLength(20)]
        public byte Age { get; set; }
        public byte Experience { get; set; }
        [Required, DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
