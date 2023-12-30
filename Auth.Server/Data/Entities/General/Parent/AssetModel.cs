using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class AssetModel : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }
        public string Extention { get; set; }
        public string Path { get; set; }
        [Required, DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
