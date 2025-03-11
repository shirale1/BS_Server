using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

[Table("Rating")]
public partial class Rating
{
    [Key]
    public int RatingId { get; set; }

    public int? UserId { get; set; }

    public int? RatingValue { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Ratings")]
    public virtual User? User { get; set; }
}
