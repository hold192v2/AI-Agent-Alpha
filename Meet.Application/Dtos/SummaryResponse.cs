using System.ComponentModel;

namespace Meet.Application.Dtos;

public class SummaryResponse
{
    public List<MetricResponse> MetricResponses { get; set; } = null!;
}

public class MetricResponse
{
    [Description("Ключ метрики")]
    public string Key { get; set; } = null!;
    
    [Description("Название метрики на русском")]
    public string Name { get; set; } = null!;
    
    [Description("Значение метрики. При необходимости - int, double, Dictionary<string,int/double>")]
    public object Value { get; set; } = null!;
    
    [Description("Изменение метрики в сравнении с предыдущим вычислительным сроком")]
    public double ChangeInProcentage { get; set; }
    
    [Description("Флаг, который показывает, возросла ли метрика")]
    public bool IsUp { get; set; }
}