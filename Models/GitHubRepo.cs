namespace GithubStarAITool.Models;

public class GitHubRepo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HtmlUrl { get; set; } = string.Empty;
    public int StargazersCount { get; set; }
    public string Language { get; set; } = string.Empty;
    public DateTimeOffset StarredAt { get; set; }
}
