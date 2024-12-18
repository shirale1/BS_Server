using BS_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class ParentDTO:UsersDTO
    {
        public ParentDTO() { }

        public ParentDTO(Models.Parent modelParent):base(modelParent.ParentNavigation) 
        {
            this.KidsN = modelParent.KidsN;
            this.Pets = modelParent.Pets;
        }

        public Models.Parent GetModel()
        {
            Models.Parent p = new Models.Parent();
            p.ParentNavigation = base.GetModel();
            p.KidsN = this.KidsN;
            p.Pets = this.Pets;
            p.ParentId = this.Id;
            return p;
        }
        public int KidsN { get; set; }

        public bool Pets { get; set; }

    }
}
