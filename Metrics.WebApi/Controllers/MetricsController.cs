using Meet.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.WebApi.Controllers;

[ApiController]
[Route("metrics")]
public class MetricsController : ControllerBase
{
    [HttpPost("data")]
    [EndpointSummary("Получить данные для сводки")]
    [EndpointDescription(
        "Получение данных для отображения сводки. Вывод производится по ключам."
    )]
    [ProducesResponseType(typeof(SummaryResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetSummaryData([FromBody] SummaryRequest summaryRequest)
    {
        var response = new SummaryResponse
        {
            MetricResponses =
            [
                new MetricResponse
                {
                    Key = "issues-created-count",
                    Name = "Количество созданных задач",
                    Value = 127,
                    ChangeInProcentage = 12.4,
                    IsUp = true
                },

                new MetricResponse
                {
                    Key = "issues-completed-late-percentage",
                    Name = "Процент задач, выполненных после срока",
                    Value = 8.7,
                    ChangeInProcentage = -2.1,
                    IsUp = false
                },

                new MetricResponse
                {
                    Key = "issues-by-status",
                    Name = "Количество задач по статусам",
                    Value = new Dictionary<string, int>
                    {
                        ["Open"] = 15,
                        ["InProgress"] = 9,
                        ["Review"] = 4,
                        ["Done"] = 99
                    },
                    ChangeInProcentage = 5.3,
                    IsUp = true
                },

                new MetricResponse
                {
                    Key = "completed-issues-by-priority",
                    Name = "Выполненные задачи по приоритетам",
                    Value = new Dictionary<string, int>
                    {
                        ["Low"] = 24,
                        ["Medium"] = 41,
                        ["High"] = 17,
                        ["Critical"] = 3
                    },
                    ChangeInProcentage = 7.8,
                    IsUp = true
                }
            ]
        };
        return Ok(response);
    }
}