using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class BabysiterDTO:UsersDTO
    {
        public BabysiterDTO() { }
     
        public BabysiterDTO(Models.Babysiter modelbabysiter) : base(modelbabysiter.BabysiterNavigation) //יוצר טיפוס מסוג דיטיאו מהמודל בייביסיטר
        {
            this.BirthDate = modelbabysiter.BirthDate;
            this.ExperienceY=modelbabysiter.ExperienceY;
            this.Payment = modelbabysiter.Payment;
            this.License=modelbabysiter.License;
        }
       

        public Models.Babysiter GetModel()
        {
            Models.Babysiter b=new Models.Babysiter();
            b.BabysiterNavigation = base.GetModel();
            b.BirthDate=this.BirthDate;
            b.ExperienceY= this.ExperienceY;
            b.Payment = this.Payment;
            b.License=this.License;
            return b;
        }

        public DateOnly BirthDate { get; set; }

        public int ExperienceY { get; set; }
        public bool License { get; set; }
        public int Payment { get; set; }

  

        
    }
}
