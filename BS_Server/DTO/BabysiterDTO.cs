﻿using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class BabysiterDTO
    {
        public int BabysiterId { get; set; }

        public string? UserName { get; set; }

        public string? Pass { get; set; }

        public int? Age { get; set; }

        public int? ExperienceY { get; set; }

        public bool? License { get; set; }

        public string? Email { get; set; }

        public string? City { get; set; }

        
    }
}
