using AutoMapper;
using DTOs;
using MassTransit;
using Team.Domain.Interfaces;

namespace Team.Application.RabbitMq;

public class AuthCheckConsumer : IConsumer<UserCheckAuthRequestDto>
{
    private readonly IUserRepository _userRepositoryRepository;
    private readonly IMapper _mapper;

    public AuthCheckConsumer(IUserRepository userRepositoryRepository, IMapper mapper)
    {
        _userRepositoryRepository = userRepositoryRepository;
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<UserCheckAuthRequestDto> context)
    {
        var id = context.Message.UserId;
        var user = await _userRepositoryRepository.GetUserByUserId(id);
        if (user is not null)
            await context.RespondAsync(_mapper.Map<UserCheckAuthDto>(user));
        else await context.RespondAsync(new UserCheckAuthDto());
    }
}