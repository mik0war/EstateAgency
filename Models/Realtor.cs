using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Models
{
    [Table("realtor")]
    public class Realtor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("realtor_code")]
        public int RealtorCode { get; set; }

        [Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Column("sur_name")]
        public string SurName { get; set; }

        [Phone]
        [StringLength(14)]
        [Column("phone")]
        public string Phone { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
