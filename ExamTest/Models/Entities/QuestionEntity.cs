namespace ExamBackend.Models.Entities;

public class QuestionEntity
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<OptionEntity> Options { get; set; } = new();
    public int CorrectOptionId { get; set; }
}


