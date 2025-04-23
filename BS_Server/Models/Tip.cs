using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class Tip
{
    [Key]
    public int TipId { get; set; }

    public int? UserId { get; set; }

    [StringLength(500)]
    public string? TipText { get; set; }

    public int? StatusId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Tips")]
    public virtual User? User { get; set; }
}
