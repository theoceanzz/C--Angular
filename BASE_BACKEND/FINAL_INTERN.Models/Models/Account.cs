using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

public partial class Account
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(20)]
    public string Username { get; set; }

    [Required]
    [StringLength(20)]
    public string Password { get; set; }

    public int? Gender { get; set; }

    [StringLength(20)]
    public string FirstName { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    [StringLength(20)]
    public string Address { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Birthday { get; set; }

    [Column("img")]
    [StringLength(100)]
    public string Img { get; set; }

    [Column("alt")]
    [StringLength(50)]
    public string Alt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("Role_ID")]
    public int? RoleId { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("Account")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("RoleId")]
    [InverseProperty("Accounts")]
    public virtual Role Role { get; set; }
}
