using ExamBackend.Models.Entities;

namespace ExamBackend.DataAccess;

public class ExamRepository : IExamRepository
{
    private static List<QuestionEntity> _questions = new();
    private static List<ExamResultEntity> _examResults = new();
    private static int _nextResultId = 1;
    private static int _nextAnswerId = 1;
    private static readonly object _lock = new();

    public ExamRepository()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        if (_questions.Any())
        {
            return;
        }

        lock (_lock)
        {
            if (_questions.Any())
            {
                return;
            }

            var question1 = new QuestionEntity
            {
                Id = 1,
                QuestionText = "ข้อใดต่างจากข้ออื่น",
                Options = new List<OptionEntity>
                {
                    new OptionEntity { Id = 1, QuestionId = 1, OptionText = "3" },
                    new OptionEntity { Id = 2, QuestionId = 1, OptionText = "5" },
                    new OptionEntity { Id = 3, QuestionId = 1, OptionText = "9" },
                    new OptionEntity { Id = 4, QuestionId = 1, OptionText = "11" }
                },
                CorrectOptionId = 3
            };

            var question2 = new QuestionEntity
            {
                Id = 2,
                QuestionText = "X + 2 = 4 จงหาค่า X",
                Options = new List<OptionEntity>
                {
                    new OptionEntity { Id = 5, QuestionId = 2, OptionText = "1" },
                    new OptionEntity { Id = 6, QuestionId = 2, OptionText = "2" },
                    new OptionEntity { Id = 7, QuestionId = 2, OptionText = "3" },
                    new OptionEntity { Id = 8, QuestionId = 2, OptionText = "4" }
                },
                CorrectOptionId = 6
            };

            _questions.Add(question1);
            _questions.Add(question2);
        }
    }

    public List<QuestionEntity> GetAllQuestions()
    {
        return _questions.ToList();
    }

    public QuestionEntity? GetQuestionById(int questionId)
    {
        return _questions.FirstOrDefault(q => q.Id == questionId);
    }

    public ExamResultEntity SaveExamResult(ExamResultEntity examResult)
    {
        lock (_lock)
        {
            examResult.Id = _nextResultId++;
            examResult.ExamDate = DateTime.Now;

            foreach (var answer in examResult.Answers)
            {
                answer.Id = _nextAnswerId++;
                answer.ExamResultId = examResult.Id;
            }

            _examResults.Add(examResult);
            return examResult;
        }
    }

    public ExamResultEntity? GetExamResultById(int id)
    {
        return _examResults.FirstOrDefault(r => r.Id == id);
    }

    public List<ExamResultEntity> GetAllExamResults()
    {
        return _examResults.ToList();
    }

    public void ClearAllExamResults()
    {
        lock (_lock)
        {
            _examResults.Clear();
            _nextResultId = 1;
            _nextAnswerId = 1;
        }
    }
}


