using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Models
{
    [Table("sale")]
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("sale_code")]
        public int SaleCode { get; set; }

        [ForeignKey("RealEstate")]
        [Column("estate_code")]
        public int EstateCode { get; set; }
        public virtual RealEstate RealEstate { get; set; }

        [Column("sale_date")]
        public DateTime SaleDate { get; set; }

        [ForeignKey("Realtor")]
        [Column("realtor_code")]
        public int RealtorCode { get; set; }
        public virtual Realtor Realtor { get; set; }

        [Column("price")]
        public double Price { get; set; }
    }
}
