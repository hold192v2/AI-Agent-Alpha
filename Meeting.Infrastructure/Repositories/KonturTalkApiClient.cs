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

    public async Task<EmailCalendarResult> GetMeetingsByUserEmail(string userEmail, DateTime start, DateTime? end, int? take)
    {
        try
        {
            var requestUrl = "";
            if (end.HasValue)
                requestUrl = $"/api/EmailCalendar/{userEmail}?start={start}&end={end}";
            else if (take.HasValue)
                requestUrl = $"/api/EmailCalendar/{userEmail}?start={start}&take={take}";
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

    public async Task<EmailCalendarItem> FindMeetingByUserEmailAndId(string userEmail, string id)
    {
        try
        {
            for (var i = 0; i > -31; i--)
            {
                var startDate = DateTime.UtcNow.Date.AddDays(i);
                var endDate = startDate.AddDays(i).AddTicks(-1);
                var emailCalendarResult = await GetMeetingsByUserEmail(userEmail, startDate, endDate, null);
                var emailCalendarItem = emailCalendarResult.Items.FirstOrDefault(i => i.Id == id);
                if (emailCalendarItem != null)
                    return emailCalendarItem;
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}