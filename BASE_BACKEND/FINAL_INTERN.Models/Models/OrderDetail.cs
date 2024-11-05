﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

public partial class OrderDetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Order_ID")]
    public int? OrderId { get; set; }

    [Column("Account_ID")]
    public int? AccountId { get; set; }

    [Column("Car_ID")]
    public int? CarId { get; set; }

    [StringLength(100)]
    public string NameOfCar { get; set; }

    [Column("price")]
    public int? Price { get; set; }

    [Column("total")]
    public double? Total { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("OrderDetails")]
    public virtual Account Account { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("OrderDetails")]
    public virtual CarInfo Car { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; }
}
