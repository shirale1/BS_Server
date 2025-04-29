using BS_Server.Models;

namespace BS_Server.DTO
{
    public class TipDTO
    {
        public int TipId { get; set; }

        public int? UserId { get; set; }
        public string? TipText { get; set; }

        public int? StatusId { get; set; }
        public virtual User? User { get; set; }



        public TipDTO() { }

        public TipDTO(Models.Tip tip)
        {
            this.TipId = tip.TipId;
            this.TipText = tip.TipText;
            this.UserId = tip.UserId;
            this.StatusId = tip.StatusId;
        }

        public Models.Tip GetModel()
        {
            return new Models.Tip()
            {
                TipId = this.TipId,
                UserId = this.UserId,
                TipText = this.TipText,
                StatusId=this.StatusId,
            };
        }
    }
}
