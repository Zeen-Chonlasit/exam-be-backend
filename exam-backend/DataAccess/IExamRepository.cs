using ExamBackend.Models.Entities;

namespace ExamBackend.DataAccess;

public interface IExamRepository
{
    List<QuestionEntity> GetAllQuestions();
    QuestionEntity? GetQuestionById(int questionId);
    ExamResultEntity SaveExamResult(ExamResultEntity examResult);
    ExamResultEntity? GetExamResultById(int id);
    List<ExamResultEntity> GetAllExamResults();
    void ClearAllExamResults();
}


