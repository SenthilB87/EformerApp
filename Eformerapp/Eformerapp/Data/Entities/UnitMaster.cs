using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eformerapp.Data.Entities
{
    public class UnitMaster
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
