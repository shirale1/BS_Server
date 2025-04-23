using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

[Index("Email", Name = "UQ__Users__A9D10534D61EB005", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    public bool IsAdmin { get; set; }

    [StringLength(50)]
    public string Gender { get; set; } = null!;

    [StringLength(50)]
    public string Phone { get; set; } = null!;

    [InverseProperty("BabysiterNavigation")]
    public virtual Babysiter? Babysiter { get; set; }

    [InverseProperty("ParentNavigation")]
    public virtual Parent? Parent { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [InverseProperty("User")]
    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    [InverseProperty("User")]
    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
