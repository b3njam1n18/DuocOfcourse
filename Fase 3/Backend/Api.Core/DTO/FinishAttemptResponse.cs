namespace Api.Core.DTO
{
    public class FinishAttemptResponse
    {
        public long AttemptId { get; set; }
        public decimal Score { get; set; }
        public decimal TotalPoints { get; set; }
        public decimal Percentage { get; set; }
        public bool Passed { get; set; }
    }
}
