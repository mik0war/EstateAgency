using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstateAgency.Models
{
    [Table("real_estate")]
    public class RealEstate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("estate_code")]
        public int? EstateCode { get; set; }

        [ForeignKey("District")]
        [Column("district")]
        public int? DistrictCode { get; set; }
        public virtual District? District { get; set; }

        [StringLength(150)]
        [Column("address")]
        public string? Address { get; set; }

        [Column("estate_floor")]
        public int? Floor { get; set; }

        [Column("rooms_number")]
        public int? RoomsNumber { get; set; }

        [ForeignKey("EstateType")]
        [Column("estate_type")]
        public int? EstateTypeCode { get; set; }
        public virtual EstateType? EstateType { get; set; }

        [Column("status")]
        public bool? Status { get; set; }

        [Column("price")]
        public double? Price { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [ForeignKey("BuildingMaterial")]
        [Column("building_material")]
        public int? BuildingMaterialCode { get; set; }
        public virtual BuildingMaterial? BuildingMaterial { get; set; }

        [Column("area")]
        public double? Area { get; set; }

        [Column("advertisement_date")]
        public DateTime AdvertisementDate { get; set; }

        public void UpdateFields(RealEstate updatedRealEstate)
        {
            if (updatedRealEstate.EstateCode != null)
                EstateCode = updatedRealEstate.EstateCode;

            if (updatedRealEstate.DistrictCode!= null)
                DistrictCode = updatedRealEstate.DistrictCode;

            if (updatedRealEstate.Address != null)
                Address = updatedRealEstate.Address;

            if (updatedRealEstate.Floor != null)
                Floor = updatedRealEstate.Floor;

            if (updatedRealEstate.RoomsNumber != null)
                RoomsNumber = updatedRealEstate.RoomsNumber;

            if (updatedRealEstate.EstateTypeCode != null)
                EstateTypeCode = updatedRealEstate.EstateTypeCode;

            if (updatedRealEstate.Status != null)
                Status = updatedRealEstate.Status;

            if (updatedRealEstate.Price != null)
                Price = updatedRealEstate.Price;

            if (updatedRealEstate.Description != null)
                Description = updatedRealEstate.Description;

            if (updatedRealEstate.BuildingMaterialCode != null)
                BuildingMaterialCode = updatedRealEstate.BuildingMaterialCode;

            if (updatedRealEstate.Area != null)
                Area = updatedRealEstate.Area;

        }
    }


}
