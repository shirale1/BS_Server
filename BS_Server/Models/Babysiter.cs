﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class Babysiter
{
    [Key]
    public int BabysiterId { get; set; }

    public DateOnly BirthDate { get; set; }

    public int ExperienceY { get; set; }

    public bool License { get; set; }

    public int Payment { get; set; }

    [ForeignKey("BabysiterId")]
    [InverseProperty("Babysiter")]
    public virtual User BabysiterNavigation { get; set; } = null!;
}
