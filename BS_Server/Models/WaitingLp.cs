using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

[Table("WaitingLP")]
public partial class WaitingLp
{
    [Key]
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int? BabysiterId { get; set; }

    public int? StatusCode { get; set; }

    [ForeignKey("BabysiterId")]
    [InverseProperty("WaitingLps")]
    public virtual Babysiter? Babysiter { get; set; }

    [ForeignKey("ParentId")]
    [InverseProperty("WaitingLps")]
    public virtual Parent? Parent { get; set; }

    [ForeignKey("StatusCode")]
    [InverseProperty("WaitingLps")]
    public virtual StatusTable? StatusCodeNavigation { get; set; }
}
