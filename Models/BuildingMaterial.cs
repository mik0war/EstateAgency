using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Models
{
    [Table("building_material")]
    public class BuildingMaterial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("material_code")]
        public int MaterialCode { get; set; }

        [Required]
        [StringLength(50)]
        [Column("material_name")]
        public string MaterialName { get; set; }
    }
}
