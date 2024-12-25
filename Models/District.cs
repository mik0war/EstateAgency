using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstateAgency.Models
{

    [Table("district")]
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("district_code")]
        public int DistrictCode { get; set; }

        [Required]
        [StringLength(50)]
        [Column("district_name")]
        public string DistrictName { get; set; }
    }
}
