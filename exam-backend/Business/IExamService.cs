using ExamBackend.Models.DTOs;

namespace ExamBackend.Business;

public interface IExamService
{
    List<QuestionDto> GetAllQuestions();
    SubmitExamResponseDto SubmitExam(SubmitExamRequestDto request);
    ExamResultDto GetExamResult(int id);
    void ClearAllResults();
}


