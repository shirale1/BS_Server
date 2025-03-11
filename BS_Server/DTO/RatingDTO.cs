namespace BS_Server.DTO
{
    public class RatingDTO
    {
        public int RatingId { get; set; }

        public int? UserId { get; set; }

        public int? RatingValue { get; set; }

        public RatingDTO() { }

        public RatingDTO(Models.Rating r)
        {
            this.RatingId = r.RatingId;
            this.RatingValue = r.RatingValue;
            this.UserId = r.UserId;
        }

        public Models.Rating GetModel()
        {
            return new Models.Rating()
            {
                RatingId = this.RatingId,
                UserId = this.UserId,
                RatingValue = this.RatingValue,
            };
        }
    }
}

