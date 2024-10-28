using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class StatusTableDTO
    {
        public int StatusId { get; set; }

        public string? StatusDescription { get; set; }

        public virtual ICollection<WaitingLb> WaitingLbs { get; set; } = new List<WaitingLb>();

        public virtual ICollection<WaitingLp> WaitingLps { get; set; } = new List<WaitingLp>();
    }
}
