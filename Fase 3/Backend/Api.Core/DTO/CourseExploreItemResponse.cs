namespace Api.Core.DTO
{
    public class CourseExploreItemResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string? SchoolName { get; set; }
        public string? TeacherName { get; set; }

        public int LessonsCount { get; set; }
        public bool IsPublished { get; set; }
    }
}
