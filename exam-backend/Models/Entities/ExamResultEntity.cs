namespace ExamBackend.Models.Entities;

public class ExamResultEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime ExamDate { get; set; } = DateTime.Now;
    public List<ExamAnswerEntity> Answers { get; set; } = new();
}


