using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

public partial class Order
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Account_ID")]
    public int? AccountId { get; set; }

    [StringLength(100)]
    public string NameOfCustomer { get; set; }

    [Column("date")]
    [StringLength(40)]
    [Unicode(false)]
    public string Date { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Orders")]
    public virtual Account Account { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
