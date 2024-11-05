using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

public partial class CarInfo
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Model { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Years { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [Required]
    [StringLength(50)]
    public string Transmission { get; set; }

    [Required]
    [StringLength(20)]
    public string FuelType { get; set; }

    public int? StockQuantit { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Column("img")]
    [StringLength(100)]
    public string Img { get; set; }

    [Column("alt")]
    [StringLength(50)]
    public string Alt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("CategoriesOfCar_ID")]
    public int? CategoriesOfCarId { get; set; }

    [ForeignKey("CategoriesOfCarId")]
    [InverseProperty("CarInfos")]
    public virtual CategoriesOfCar CategoriesOfCar { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
