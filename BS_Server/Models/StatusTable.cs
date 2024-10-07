using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

[Table("StatusTable")]
public partial class StatusTable
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(250)]
    public string? StatusDescription { get; set; }

    [InverseProperty("StatusCodeNavigation")]
    public virtual ICollection<WaitingLb> WaitingLbs { get; set; } = new List<WaitingLb>();

    [InverseProperty("StatusCodeNavigation")]
    public virtual ICollection<WaitingLp> WaitingLps { get; set; } = new List<WaitingLp>();
}
