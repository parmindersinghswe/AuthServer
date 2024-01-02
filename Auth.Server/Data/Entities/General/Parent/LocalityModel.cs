using System.ComponentModel;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class LocalityModel : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DistrictId { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required, DefaultValue(false)]
        public bool IsCity { get; set; }
        [Required, DefaultValue(false), JsonIgnore]
        public bool IsDeleted { get; set; }
        public string SarpanchMayerName { get; set; }
        [MaxLength(13)]
        public string ContactNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
