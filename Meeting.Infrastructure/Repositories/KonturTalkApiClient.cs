using System.Net.Http.Headers;
using System.Text.Json;
using Meeting.Domain.Interfaces;
using Meeting.Domain.KonturEntities;

namespace Meeting.Infrastructure.Repositories;

public class KonturTalkApiClient: IKonturTalkApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private IKonturTalkApiClient _konturTalkApiClientImplementation;
    private IKonturTalkApiClient _konturTalkApiClientImplementation1;

    public KonturTalkApiClient()
    {
        const string baseUrl = $"https://pblyrh6n.ktalk.ru";
        const string apiKey = "icVKoK5gz6V4C8ZJPvRuQ1BvSvI5rw8w";
        
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<TalkUser>> GetUsersByMeetingId(Guid meetingId)
    {
        try
        {
            var users = new List<TalkUser>();
            var requestUrl = $"/api/ConferenceReports/{meetingId}/participants";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var participants = JsonSerializer.Deserialize<List<Participant>>(jsonResponse, _jsonOptions);

            foreach (var participant in participants)
            {
                var userKey = participant.ParticipantId;
                requestUrl = $"/api/users/{userKey}";
                response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                
                jsonResponse = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<TalkUser>(jsonResponse, _jsonOptions);
                users.Add(user);
            }
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<EmailCalendarResult> GetMeetingsByUserEmail(string userEmail)
    {
        try
        {
            var requestUrl = $"/api/EmailCalendar/{userEmail}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var emailCalendarResult = JsonSerializer.Deserialize<EmailCalendarResult>(jsonResponse, _jsonOptions);
            return emailCalendarResult;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<EmailCalendarItem> FindMeetingByUserEmail(string userEmail, string id)
    {
        try
        {
            var emailCalendarResult = await GetMeetingsByUserEmail(userEmail);
            var emailCalendarItem = emailCalendarResult.Items.FirstOrDefault(i => i.Id == id);
            return emailCalendarItem;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}