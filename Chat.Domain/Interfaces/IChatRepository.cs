using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces;

public interface IChatRepository
{
    Task<List<Entities.Chat>> GetChatsByUserId(Guid userId);
    Task<Entities.Chat> GetChatById(Guid? chatId);
    Task CreateChat(Entities.Chat chat);
}

// [JsonPropertyName("id")]
// public Guid Id { get; set; }
// [JsonPropertyName("userId")]
// public Guid UserId { get; set; }
// [JsonPropertyName("teamId")]
// public string? TeamId { get; set; }
// [JsonPropertyName("title")]
// public string Title { get; set; }
// [JsonPropertyName("role")]
// public string Role { get; set; }
// }