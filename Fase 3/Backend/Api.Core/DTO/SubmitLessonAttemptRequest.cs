namespace Api.Core.DTO
{
    public class SubmitLessonAttemptRequest
    {
        public long StudentId { get; set; }
        public List<SubmitLessonQuizResult> Quizzes { get; set; } = new();
    }

    public class SubmitLessonQuizResult
    {
        public long EvaluationId { get; set; }
        public List<SubmitAnswerItem> Answers { get; set; } = new();
    }

    public class SubmitAnswerItem
    {
        public long QuestionId { get; set; }
        public long? OptionId { get; set; }
        public string? OpenText { get; set; }
    }

    public class SubmitLessonAttemptResponse
    {
        public long AttemptId { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public long LessonId { get; set; }
        public decimal Score { get; set; }
        public bool Passed { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class LessonAttemptSummary
    {
        public long AttemptId { get; set; }
        public decimal Score { get; set; }
        public bool Passed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
