using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class ContactModel : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AddressId { get; set; }

        [Required, MinLength(10), MaxLength(20)]
        public string MobileNumber1 { get; set; }
        [MinLength(10), MaxLength(20)]
        public string MobileNumber2 { get; set; }
        [MinLength(6), MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [MaxLength(250)]
        public string Facebook { get; set; }
        [MaxLength(250)]
        public string Twitter { get; set; }
        [MaxLength(250)]
        public string Instagram { get; set; }
        [Required, DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
