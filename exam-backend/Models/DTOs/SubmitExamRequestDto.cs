namespace ExamBackend.Models.DTOs;

public class SubmitExamRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Dictionary<int, int> Answers { get; set; } = new();
}


