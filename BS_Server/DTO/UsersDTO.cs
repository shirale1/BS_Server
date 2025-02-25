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
            this.Address = modeluser.Address;
            this.Email = modeluser.Email;
            this.Id = modeluser.Id;
            this.IsAdmin=modeluser.IsAdmin;
            this.Gender=modeluser.Gender;
            this.FirstName=modeluser.FirstName;
            this.LastName=modeluser.LastName;
            this.Phone=modeluser.Phone;
        }
        public int Id { get; set; }

        public string FirstName { get; set; } 
        
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool IsAdmin { get; set; }

        public string Gender { get; set; }
        public string? ProfileImagePath { get; set; }
        public string Phone {  get; set; }

       public Models.User GetModel()
       {
            Models.User modeluser = new Models.User();
            modeluser.UserName = this.UserName;
            modeluser.Password = this.Password;
            modeluser.Address = this.Address;
            modeluser.Email = this.Email;
            modeluser.Id = this.Id;
            modeluser.IsAdmin = this.IsAdmin;
            modeluser.Gender = this.Gender;
            modeluser.FirstName = this.FirstName;
            modeluser.LastName = this.LastName;
            modeluser.Phone = this.Phone;
            return modeluser;
        }
    }
}
