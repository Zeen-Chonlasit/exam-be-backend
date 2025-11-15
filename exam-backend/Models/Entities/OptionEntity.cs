namespace ExamBackend.Models.Entities;

public class OptionEntity
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; } = string.Empty;
}


