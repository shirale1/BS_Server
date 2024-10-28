using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class ParentDTO
    {
        public int ParentId { get; set; }

        public int? KidsN { get; set; }

        public bool? Pets { get; set; }

    }
}
