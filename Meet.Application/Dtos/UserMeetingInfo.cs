using System.ComponentModel;

namespace Meet.Application.Dtos;

public record UserMeetingInfo(
    [Description("Id Пользователя")] Guid Id, 
    [Description("Фамилия пользователя")] string Surname, 
    [Description("Имя пользователя")] string Name, 
    [Description("Ссылка на фотография пользователя")] string PhotoUrl);