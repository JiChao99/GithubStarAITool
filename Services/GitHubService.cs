using GithubStarAITool.Models;
using System.Net.Http.Json;

namespace GithubStarAITool.Services;

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;
    private readonly string _token;

    public GitHubService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _token = configuration["GitHubService:Token"];
        _httpClient.BaseAddress = new Uri("https://api.github.com/");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GithubStarAITool");
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
    }

    public async Task<PaginatedList<GitHubRepo>> GetStarredReposAsync(string username, int page = 1, int pageSize = 50)
    {
        try
        {
            // Request one extra item to determine if there's a next page
            var response = await _httpClient.GetAsync($"users/{username}/starred?page={page}&per_page={pageSize}");
            response.EnsureSuccessStatusCode();

            var repos = await response.Content.ReadFromJsonAsync<List<GitHubRepo>>();

            if (repos == null)
                return new PaginatedList<GitHubRepo>(new List<GitHubRepo>(), page, pageSize, false);

            // Remove the extra item before returning if we got more than requested
            var itemsToReturn = repos;
            var hasNextPage = repos.Count == pageSize;

            // Handle rate limit
            if (response.Headers.TryGetValues("X-RateLimit-Remaining", out var values) && int.TryParse(values.FirstOrDefault(), out var remaining) && remaining == 0)
            {
                if (response.Headers.TryGetValues("X-RateLimit-Reset", out var resetValues) && long.TryParse(resetValues.FirstOrDefault(), out var reset))
                {
                    var delay = TimeSpan.FromSeconds(reset - DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    await Task.Delay(delay);
                }
            }

            return new PaginatedList<GitHubRepo>(itemsToReturn, page, pageSize, hasNextPage);
        }
        catch (HttpRequestException ex)
        {
            return new PaginatedList<GitHubRepo>(new List<GitHubRepo>(), page, pageSize, false);
        }
    }

    public async Task<List<GitHubRepo>> GetAllStarredReposAsync(string username)
    {
        var allRepos = new List<GitHubRepo>();
        var page = 1;
        const int pageSize = 100;
        
        while (true)
        {
            var result = await GetStarredReposAsync(username, page, pageSize);
            allRepos.AddRange(result.Items);
            
            if (!result.HasNextPage)
                break;
                
            page++;
        }
        
        return allRepos;
    }
}
