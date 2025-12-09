using System.Collections.Generic;

namespace Api.Core.DTO
{
    public class AttemptResultResponse
    {
        public long AttemptId { get; set; }
        public decimal Score { get; set; }
        public decimal TotalPoints { get; set; }
        public decimal Percentage { get; set; }
        public bool Passed { get; set; }

        public List<QuestionResult> Questions { get; set; } = new();
    }

    
}
