namespace API_Gateway.Extentions.Interfaces;

public interface IAuthFacade
{
    Task<object?> GetMeAsync(Guid userId);
}