namespace DTOs;

public record  RegisterIntoTeamDto(Guid Id, string Name,  string Surname, string Patronymic, 
    string Email, int RoleId);