using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class Parent
{
    [Key]
    public int ParentId { get; set; }

    public int KidsN { get; set; }

    public bool Pets { get; set; }

    [ForeignKey("ParentId")]
    [InverseProperty("Parent")]
    public virtual User ParentNavigation { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<WaitingLb> WaitingLbs { get; set; } = new List<WaitingLb>();

    [InverseProperty("Parent")]
    public virtual ICollection<WaitingLp> WaitingLps { get; set; } = new List<WaitingLp>();
}
