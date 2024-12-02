using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class UsersDTO
    {
        public UsersDTO() { }
        public UsersDTO(Models.User modeluser)
        {
            this.UserName = modeluser.UserName;
            this.Password = modeluser.Password;
            this.City = modeluser.City;
            this.Email = modeluser.Email;
            this.Id = modeluser.Id;
        }
        public int Id { get; set; }

        public string? UserName { get; set; }


        public string? Password { get; set; }


        public string? Email { get; set; }


        public string? City { get; set; }

       public Models.User GetModel()
       {
            Models.User modeluser = new Models.User();
            modeluser.UserName = this.UserName;
            modeluser.Password = this.Password;
            modeluser.City = this.City;
            modeluser.Email = this.Email;
            modeluser.Id = this.Id;

            return modeluser;
        }


       
    }
}
