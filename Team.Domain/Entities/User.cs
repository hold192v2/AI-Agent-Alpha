namespace Team.Domain.Entities;

public sealed class User
{
    public User(string surname, string name, string patronymic, 
        string email, Guid? id = null, string? atlassianAccountId = null, 
        string? githubAccountId = null, string? photoUrl = null)
    {
        Id = id ?? Guid.NewGuid();
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Role = "teamleader";
        Email = email;
        AtlassianAccountId = atlassianAccountId;
        GithubAccountId = githubAccountId;
        PhotoUrl = photoUrl;
        CreatedAt = DateTime.UtcNow;
    }
    //EF Core
    private User()
    {
        
    }
    public Guid Id { get; private set; }
    public string Surname { get; private set; }
    public string Name { get; private set; }
    public string Patronymic { get; private set; }
    public string Role { get; private set; }
    public string Email { get; private set; }
    public string? AtlassianAccountId { get; private set; }
    public string? GithubAccountId { get; private set; }
    public string? PhotoUrl { get; private set; }
    public DateTime? CreatedAt { get; private set; }
}