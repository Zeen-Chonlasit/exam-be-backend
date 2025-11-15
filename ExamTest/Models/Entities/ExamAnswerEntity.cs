namespace ExamBackend.Models.Entities;

public class ExamAnswerEntity
{
    public int Id { get; set; }
    public int ExamResultId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
    public bool IsCorrect { get; set; }
}


