using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class BabysiterDTO
    {
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
}
