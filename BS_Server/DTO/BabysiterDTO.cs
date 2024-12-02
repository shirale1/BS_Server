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
            this.Age=modelbabysiter.Age;
            this.ExperienceY=modelbabysiter.ExperienceY;
            this.License=modelbabysiter.License;
        }
       

        public Models.Babysiter GetModel()
        {
            Models.Babysiter b=new Models.Babysiter();
            b.BabysiterNavigation = base.GetModel();
            b.Age=this.Age;
            b.ExperienceY= this.ExperienceY;
            b.License=this.License;
            return b;
        }

        public int? Age { get; set; }

        public int? ExperienceY { get; set; }

        public bool? License { get; set; }

  

        
    }
}
