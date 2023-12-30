using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class CurrencyModel : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ForeignKey("Country")]
        public string Code { get; set; }
        [Required, DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
