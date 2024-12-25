using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstateAgency.Models
{

    [Table("estate_type")]
    public class EstateType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("type_code")]
        public int TypeCode { get; set; }

        [Required]
        [StringLength(50)]
        [Column("type_name")]
        public string TypeName { get; set; }
    }
}
