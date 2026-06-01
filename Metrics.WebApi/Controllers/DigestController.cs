using Meet.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.WebApi.Controllers;

[ApiController]
[Route("digest")]
public class DigestController : ControllerBase
{
    [HttpPost("data")]
    [EndpointSummary("Получить данные для дайджеста")]
    [EndpointDescription(
        "Получение данных для отображения дайджеста. Вывод производится по ключам."
    )]
    [ProducesResponseType(typeof(DigestResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetDigestData([FromBody] DigestRequest summaryRequest)
    {
        var response = new DigestResponse
        {
            StartDate = new DateOnly(2025, 3, 1),
            EndDate = new DateOnly(2025, 4, 30),
            Lanes =
            [
                new RoadmapLaneDto
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Разработать MVP для проекта \"Агент поддержки\"",
                    Items =
                    [
                        new RoadmapItemDto
                        {
                            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                            Title = "Провести анализ метрик",
                            StartDate = new DateOnly(2025, 3, 10),
                            EndDate = new DateOnly(2025, 3, 20),
                            CompletedAt = null,
                            Status = RoadmapStatus.InProgress,
                            Priority = PriorityLevel.Low,
                            JiraLink = "https://jira.company.ru/browse/MEET-101",
                            Assignee = new UserShortDto
                            {
                                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                                Name = "Борис",
                                Surname = "Иванов",
                                AvatarUrl = "https://i.pravatar.cc/150?img=1"
                            }
                        }
                    ]
                },

                new RoadmapLaneDto
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "Интеграция с Jira",
                    Items =
                    [
                        new RoadmapItemDto
                        {
                            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                            Title = "Настроить синхронизацию задач",
                            StartDate = new DateOnly(2025, 4, 1),
                            EndDate = new DateOnly(2025, 4, 10),
                            CompletedAt = new DateOnly(2025, 4, 9),
                            Status = RoadmapStatus.Done,
                            Priority = PriorityLevel.High,
                            JiraLink = "https://jira.company.ru/browse/MEET-102",
                            Assignee = new UserShortDto
                            {
                                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                                Name = "Анна",
                                Surname = "Петрова",
                                AvatarUrl = "https://i.pravatar.cc/150?img=2"
                            }
                        }
                    ]
                }
            ]
        };

        return Ok(response);
    }
}