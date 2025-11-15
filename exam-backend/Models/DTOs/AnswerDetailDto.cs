namespace ExamBackend.Models.DTOs;

public class AnswerDetailDto
{
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
    public bool IsCorrect { get; set; }
    public int CorrectOptionId { get; set; }
}


