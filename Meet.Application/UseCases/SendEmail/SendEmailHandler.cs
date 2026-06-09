using MediatR;
using Meeting.Domain.Interfaces;
using MailKit.Net.Smtp;
using MassTransit;
using Meet.Application.Dtos;
using MimeKit;

namespace Meet.Application.UseCases.SendEmail;

public class SendEmailHandler: IRequestHandler<SendEmailRequest, HandleResponse.Response<string>>
{
    private readonly IKonturTalkApiClient _konturTalkApiClient;
    private readonly IProtocolRepository _protocolRepository;
    private readonly IUserMeetingRepository _userMeetingRepository;
    private readonly IRequestClient<EmailByUsersIdRequest> _usersClient;

    public SendEmailHandler(IKonturTalkApiClient konturTalkApiClient)
    {
        _konturTalkApiClient = konturTalkApiClient;
    }
    
    public async Task<HandleResponse.Response<string>> Handle(SendEmailRequest request, CancellationToken cancellationToken)
    {
        var meetingIds = await _protocolRepository.GetMeetingIdsByProtocolId(request.ProtocolId);
        var userIds = new List<Guid>();
        foreach (var id in meetingIds)
        {
            var newUserIds = await _userMeetingRepository.GetUserIdsByMeetingId(id);
            userIds.AddRange(newUserIds);
        }
        
        var emailResponse = await _usersClient.GetResponse<EmailByUsersIdResponse>
            (new EmailByUsersIdRequest { UsersId = userIds });
        var userEmails = emailResponse.Message.UsersEmail;
        
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress("Meeting Bot", request.UserEmail));

        foreach (var email in userEmails)
        {
            message.To.Add(new MailboxAddress("", email));
        }

        message.Subject = "Протокол";
        message.Body = new TextPart("plain") {Text = request.ProtocolDesc};

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync(request.UserEmail, request.UserEmail);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        
        return new HandleResponse.Response<string>("Completed", 200);
    }
}