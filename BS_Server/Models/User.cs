using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? UserType { get; set; }

    [InverseProperty("BabysiterNavigation")]
    public virtual Babysiter? Babysiter { get; set; }

    [InverseProperty("ParentNavigation")]
    public virtual Parent? Parent { get; set; }
}
