using System.ComponentModel.DataAnnotations;

namespace BS_Server.DTO
{
    public class RecommendationDTO
    {
        public int RecommendationId { get; set; }

        public int? UserId { get; set; }

        public string? RecommendationText { get; set; }

        public RecommendationDTO() { }

        public RecommendationDTO(Models.Recommendation recommendation)
        {
            this.RecommendationId = recommendation.RecommendationId;
            this.RecommendationText = recommendation.RecommendationText;
            this.UserId = recommendation.UserId;
        }

        public Models.Recommendation GetModel()
        {
            return new Models.Recommendation()
            {
                RecommendationId = this.RecommendationId,
                UserId = this.UserId,
                RecommendationText = this.RecommendationText,
            };
        }
    }
}
