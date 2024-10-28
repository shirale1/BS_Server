using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class WaitingLbDTO
    {
       
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int? BabysiterId { get; set; }

        public int? StatusCode { get; set; }


        public virtual Babysiter? Babysiter { get; set; }

        public virtual Parent? Parent { get; set; }

        public virtual StatusTable? StatusCodeNavigation { get; set; }
    }
}
