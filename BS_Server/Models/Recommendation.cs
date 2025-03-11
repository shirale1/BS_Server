using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

[Table("Recommendation")]
public partial class Recommendation
{
    [Key]
    public int RecommendationId { get; set; }

    public int? UserId { get; set; }

    [StringLength(500)]
    public string? RecommendationText { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Recommendations")]
    public virtual User? User { get; set; }
}
