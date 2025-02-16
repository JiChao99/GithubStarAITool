using GithubStarAITool.Models;

namespace GithubStarAITool.Services;

public interface IGitHubService
{
    Task<PaginatedList<GitHubRepo>> GetStarredReposAsync(string username, int page = 1, int pageSize = 50);

    Task<List<GitHubRepo>> GetAllStarredReposAsync(string username);
}
