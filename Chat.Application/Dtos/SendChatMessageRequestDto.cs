using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chat.Application.Dtos;

public class SendChatMessageRequestDto
{
    [Required]
    [Description("ID чата. Если null, считать, что чат новый")]
    public Guid? ChatId { get; set; }
    [Required]
    [Description("Наполнение запроса")]
    public string Text { get; set; } = null!;
    [Description("Роль, от которой необходимо выстраивать диалог")]
    public string? Role { get; set; }
    [Description("ID команды, необходимый для добавления контекста в ИИ")]
    public Guid? TeamId { get; set; }
}