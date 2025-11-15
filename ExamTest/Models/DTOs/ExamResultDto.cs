namespace ExamBackend.Models.DTOs;

public class ExamResultDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime ExamDate { get; set; }
    public List<AnswerDetailDto> Answers { get; set; } = new();
}


