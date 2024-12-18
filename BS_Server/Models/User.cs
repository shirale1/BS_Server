using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

[Index("Email", Name = "UQ__Users__A9D10534535EFF15", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    [InverseProperty("BabysiterNavigation")]
    public virtual Babysiter? Babysiter { get; set; }

    [InverseProperty("ParentNavigation")]
    public virtual Parent? Parent { get; set; }
}
