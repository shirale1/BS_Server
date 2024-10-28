using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }

        public string? UserName { get; set; }


        public string? Password { get; set; }


        public string? Email { get; set; }


        public string? City { get; set; }

        public string? UserType { get; set; }


        public virtual Babysiter? Babysiter { get; set; }

        public virtual Parent? Parent { get; set; }
    }
}
