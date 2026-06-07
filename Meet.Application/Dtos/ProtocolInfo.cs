using System.ComponentModel;

namespace Meet.Application.Dtos;

public record ProtocolInfo(
    [Description("Id Проотокола")] Guid Id, 
    [Description("Название протокола")] string Name, 
    [Description("Краткое описание протокола")] string Description, 
    [Description("Дата создания протокола")] DateTime CreatedAt);