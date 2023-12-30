using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class AssetTypeModel : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required, DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
