using GithubStarAITool.Models;

namespace GithubStarAITool.Services;

public interface IVectorSimilarityService
{
    Task<List<(GitHubRepo Repo, float Similarity)>> FindSimilarRepositoriesAsync(string searchText, int topK = 10);
}
