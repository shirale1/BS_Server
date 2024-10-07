using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class Babysiter
{
    [Key]
    public int BabysiterId { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(50)]
    public string? Pass { get; set; }

    public int? Age { get; set; }

    public int? ExperienceY { get; set; }

    public bool? License { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [InverseProperty("Babysiter")]
    public virtual ICollection<WaitingLb> WaitingLbs { get; set; } = new List<WaitingLb>();

    [InverseProperty("Babysiter")]
    public virtual ICollection<WaitingLp> WaitingLps { get; set; } = new List<WaitingLp>();
}
