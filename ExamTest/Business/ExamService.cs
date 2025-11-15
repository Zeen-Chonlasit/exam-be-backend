using ExamBackend.DataAccess;
using ExamBackend.Models.DTOs;
using ExamBackend.Models.Entities;

namespace ExamBackend.Business;

public class ExamService : IExamService
{
    private readonly IExamRepository _repository;

    public ExamService(IExamRepository repository)
    {
        _repository = repository;
    }

    public List<QuestionDto> GetAllQuestions()
    {
        var questions = _repository.GetAllQuestions();
        return questions.Select(q => new QuestionDto
        {
            Id = q.Id,
            QuestionText = q.QuestionText,
            Options = q.Options.Select(o => new OptionDto
            {
                Id = o.Id,
                OptionText = o.OptionText
            }).ToList()
        }).ToList();
    }

    public SubmitExamResponseDto SubmitExam(SubmitExamRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
        {
            throw new ArgumentException("ชื่อและนามสกุลต้องกรอก");
        }

        var questions = _repository.GetAllQuestions();
        int score = 0;
        int totalQuestions = questions.Count;
        var examAnswers = new List<ExamAnswerEntity>();

        foreach (var question in questions)
        {
            bool isCorrect = false;
            int selectedOptionId = 0;

            if (request.Answers.TryGetValue(question.Id, out selectedOptionId))
            {
                if (selectedOptionId == question.CorrectOptionId)
                {
                    score++;
                    isCorrect = true;
                }
            }

            examAnswers.Add(new ExamAnswerEntity
            {
                QuestionId = question.Id,
                SelectedOptionId = selectedOptionId,
                IsCorrect = isCorrect
            });
        }

        var examResult = new ExamResultEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Score = score,
            TotalQuestions = totalQuestions,
            Answers = examAnswers
        };

        var savedResult = _repository.SaveExamResult(examResult);

        return new SubmitExamResponseDto
        {
            Id = savedResult.Id,
            FirstName = savedResult.FirstName,
            LastName = savedResult.LastName,
            Score = savedResult.Score,
            TotalQuestions = savedResult.TotalQuestions
        };
    }

    public ExamResultDto GetExamResult(int id)
    {
        var result = _repository.GetExamResultById(id);
        if (result == null)
        {
            throw new KeyNotFoundException($"ไม่พบผลการสอบ ID: {id}");
        }

        var questions = _repository.GetAllQuestions();

        var answerDetails = result.Answers.Select(a =>
        {
            var question = questions.FirstOrDefault(q => q.Id == a.QuestionId);
            return new AnswerDetailDto
            {
                QuestionId = a.QuestionId,
                SelectedOptionId = a.SelectedOptionId,
                IsCorrect = a.IsCorrect,
                CorrectOptionId = question?.CorrectOptionId ?? 0
            };
        }).ToList();

        return new ExamResultDto
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Score = result.Score,
            TotalQuestions = result.TotalQuestions,
            ExamDate = result.ExamDate,
            Answers = answerDetails
        };
    }

    public void ClearAllResults()
    {
        _repository.ClearAllExamResults();
    }
}


