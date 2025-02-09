using System.Net.Http.Json;
using GithubStarAITool.Models;

namespace GithubStarAITool.Services;

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.github.com/");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GithubStarAITool");
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<PaginatedList<GitHubRepo>> GetStarredReposAsync(string username, int page = 1, int pageSize = 10)
    {
        try
        {
            var repos = await _httpClient.GetFromJsonAsync<List<GitHubRepo>>($"users/{username}/starred?page={page}&per_page={pageSize}");
            
            // Get total count from GitHub API response headers
            //var linkHeader = _httpClient.DefaultRequestHeaders.GetValues("Link").FirstOrDefault();
            //var totalCount = GetTotalCountFromHeader(linkHeader);

            return new PaginatedList<GitHubRepo>(repos ?? new List<GitHubRepo>(), 100, page, pageSize);
        }
        catch (HttpRequestException)
        {
            return new PaginatedList<GitHubRepo>(new List<GitHubRepo>(), 0, page, pageSize);
        }
    }

    private int GetTotalCountFromHeader(string? linkHeader)
    {
        if (string.IsNullOrEmpty(linkHeader)) return 0;
        
        var lastLink = linkHeader.Split(',')
            .FirstOrDefault(l => l.Contains("rel=\"last\""));
            
        if (lastLink == null) return 0;

        var pageQuery = lastLink.Split('?')[1].Split('&')
            .FirstOrDefault(q => q.StartsWith("page="));
            
        if (pageQuery == null) return 0;

        if (int.TryParse(pageQuery.Replace("page=", ""), out int lastPage))
        {
            return lastPage * 10;
        }

        return 0;
    }
}
