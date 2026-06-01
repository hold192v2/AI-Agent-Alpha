using System.ComponentModel;

namespace Meet.Application.Dtos;

public record SummaryRequest(
    [Description("Ключи метрик")]
    List<string> MetricsKeys,
    [Description("Id команды. Если Id не указан, то аналитика собирается по всем командам пользователя")]
    Guid? TeamId = null);