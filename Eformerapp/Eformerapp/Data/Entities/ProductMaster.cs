using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eformerapp.Data.Entities
{
    public class ProductMaster
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation Properties
        public UnitMaster Unit { get; set; }
        public CategoryMaster Category { get; set; }
    }
}
