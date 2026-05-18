using AutoMapper;
using DTOs;
using MassTransit;
using Team.Application.Mappers.Fabrics;
using Team.Domain.Entities;
using Team.Domain.Interfaces;

namespace Team.Application.RabbitMq;

public class RegisterConsumer : IConsumer<RegisterIntoTeamDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterConsumer(IUserRepository userRepository, IMapper mapper,  IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        
    }
    public async Task Consume(ConsumeContext<RegisterIntoTeamDto> context)
    {
        var user = context.Message.CreateUser();
        
        if (user != null && !(await _userRepository.IsExist(user.Id)))
        {
            await _userRepository.CreateUser(user!);
            await context.RespondAsync(_mapper.Map<UserCheckAuthDto>(user, opt =>
            {
                opt.Items["roleName"] = "teamleader";
            }));
        }
        else await context.RespondAsync(new UserCheckAuthDto());
        await _unitOfWork.CommitAsync();
    }
}