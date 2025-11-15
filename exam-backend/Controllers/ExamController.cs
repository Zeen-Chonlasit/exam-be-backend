using Microsoft.AspNetCore.Mvc;
using ExamBackend.Business;
using ExamBackend.Models.DTOs;

namespace ExamBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamService _examService;

    public ExamController(IExamService examService)
    {
        _examService = examService;
    }

    [HttpGet("questions")]
    [ProducesResponseType(typeof(List<QuestionDto>), 200)]
    public ActionResult<List<QuestionDto>> GetQuestions()
    {
        var questions = _examService.GetAllQuestions();
        return Ok(questions);
    }

    [HttpPost("submit")]
    [ProducesResponseType(typeof(SubmitExamResponseDto), 200)]
    [ProducesResponseType(400)]
    public ActionResult<SubmitExamResponseDto> SubmitExam([FromBody] SubmitExamRequestDto request)
    {
        try
        {
            var result = _examService.SubmitExam(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "เกิดข้อผิดพลาดในการส่งข้อสอบ", error = ex.Message });
        }
    }

    [HttpGet("result/{id}")]
    [ProducesResponseType(typeof(ExamResultDto), 200)]
    [ProducesResponseType(404)]
    public ActionResult<ExamResultDto> GetResult(int id)
    {
        try
        {
            var result = _examService.GetExamResult(id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "เกิดข้อผิดพลาดในการดึงผลการสอบ", error = ex.Message });
        }
    }

    [HttpDelete("clear")]
    [ProducesResponseType(200)]
    public IActionResult ClearResults()
    {
        try
        {
            _examService.ClearAllResults();
            return Ok(new { message = "ข้อมูลถูกล้างเรียบร้อย" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "เกิดข้อผิดพลาดในการล้างข้อมูล", error = ex.Message });
        }
    }
}
