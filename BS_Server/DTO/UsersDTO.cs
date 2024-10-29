using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class UsersDTO
    {
        public UsersDTO(Models.User modeluser)
        {
            this.UserName = modeluser.UserName;
            this.Password = modeluser.Password;
            this.City = modeluser.City;
            this.UserType= modeluser.UserType;
            this.Email = modeluser.Email;
            this.Id = modeluser.Id;
        }
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
