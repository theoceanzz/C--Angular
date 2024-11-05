﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

public partial class Role
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(20)]
    public string NameOfRole { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
