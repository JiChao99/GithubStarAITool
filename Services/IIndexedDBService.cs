using GithubStarAITool.Models;

namespace GithubStarAITool.Services;

public interface IIndexedDBService
{
    Task InitializeAsync();
    Task StoreRepositoryVectorAsync(RepositoryVector repoVector);
    Task<List<RepositoryVector>> GetAllRepositoryVectorsAsync();
    Task ClearAllDataAsync();
    Task<RepositoryVector?> GetRepositoryVectorByIdAsync(int id);
}
