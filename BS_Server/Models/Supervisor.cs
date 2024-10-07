using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class Supervisor
{
    [Key]
    public int SupervisorId { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(50)]
    public string? Pass { get; set; }

    [StringLength(100)]
    public string? City { get; set; }
}
