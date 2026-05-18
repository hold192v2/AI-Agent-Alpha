using DTOs;
using Team.Domain.Entities;

namespace Team.Application.Mappers.Fabrics;

public static class UserFactory
{
    public static User CreateUser(this RegisterIntoTeamDto dto)
    {
        return new User(
            surname: dto.Surname,
            name: dto.Name,
            patronymic: dto.Patronymic,
            email: dto.Email,
            id: dto.Id
        );
    }
}