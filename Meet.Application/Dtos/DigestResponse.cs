using System.ComponentModel;

namespace Meet.Application.Dtos;

public record DigestResponse
{
    [Description("Коллекция дорожек (строк) таймлайна.")]
    public required IReadOnlyCollection<RoadmapLaneDto> Lanes { get; init; }

    [Description("Начальная дата отображаемого периода.")]
    public required DateOnly StartDate { get; init; }

    [Description("Конечная дата отображаемого периода.")]
    public required DateOnly EndDate { get; init; }
}

public sealed record RoadmapLaneDto
{
    [Description("Id задачи")]
    public Guid Id { get; init; }

    [Description("Название задачи, отображаемое в левой колонке")]
    public required string Title { get; init; }

    [Description("Карточки, принадлежащие данной дорожке")]
    public required IReadOnlyCollection<RoadmapItemDto> Items { get; init; }
}

public sealed record RoadmapItemDto
{
    [Description("Id подзадачи")]
    public Guid Id { get; init; }

    [Description("Краткое название задачи")]
    public required string Title { get; init; }

    [Description("Дата начала выполнения или размещения на таймлайне")]
    public required DateOnly StartDate { get; init; }

    [Description("Дата завершения выполнения или размещения на таймлайне")]
    public required DateOnly EndDate { get; init; }

    [Description("Фактическая дата завершения задачи. Не заполнена для незавершённых задач")]
    public DateOnly? CompletedAt { get; init; }

    [Description("Текущий статус выполнения задачи")]
    public RoadmapStatus Status { get; init; }

    [Description("Приоритет задачи")]
    public PriorityLevel Priority { get; init; }

    [Description("Cсылка на задачу в Jira]")]
    public string? JiraLink { get; init; }

    [Description("Исполнитель, назначенный на задачу")]
    public UserShortDto? Assignee { get; init; }
}

public sealed record UserShortDto
{
    [Description("Id пользователя")]
    public Guid Id { get; init; }

    [Description("Имя пользователя")]
    public string Name { get; init; } = "";

    [Description("Фамилия пользователя")]
    public string Surname { get; init; } = "";

    [Description("Ссылка на аватар пользователя")]
    public string? AvatarUrl { get; init; }
}

public enum RoadmapStatus
{
    [Description("Черновик")]
    Draft,

    [Description("Запланирована")]
    Planned,

    [Description("В работе")]
    InProgress,

    [Description("Завершена")]
    Done,

    [Description("Отменена")]
    Cancelled
}

public enum PriorityLevel
{
    [Description("Низкий")]
    Low,

    [Description("Средний")]
    Medium,

    [Description("Высокий")]
    High,

    [Description("Критический")]
    Critical
}