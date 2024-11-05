using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

[Table("CategoriesOfCar")]
public partial class CategoriesOfCar
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string CategoryName { get; set; }

    [InverseProperty("CategoriesOfCar")]
    public virtual ICollection<CarInfo> CarInfos { get; set; } = new List<CarInfo>();
}
