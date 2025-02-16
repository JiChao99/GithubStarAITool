using GithubStarAITool.Models;
// No changes needed in this file
namespace GithubStarAITool.Services;

public class VectorSimilarityService : IVectorSimilarityService
{
    private readonly IEmbeddingService _embeddingService;
    private readonly IIndexedDBService _indexedDBService;

    public VectorSimilarityService(IEmbeddingService embeddingService, IIndexedDBService indexedDBService)
    {
        _embeddingService = embeddingService;
        _indexedDBService = indexedDBService;
    }

    public async Task<List<(GitHubRepo Repo, float Similarity)>> FindSimilarRepositoriesAsync(string searchText, int topK = 10)
    {
        var searchVector = await _embeddingService.GetEmbeddingAsync(searchText);
        var allRepoVectors = await _indexedDBService.GetAllRepositoryVectorsAsync();

        return allRepoVectors
            .Select(rv => (
                Repo: rv.Repository,
                Similarity: CosineSimilarity(searchVector, rv.Vector)))
            .OrderByDescending(x => x.Similarity)
            .Take(topK)
            .ToList();
    }

    private float CosineSimilarity(List<float> v1, List<float> v2)
    {
        float dotProduct = 0;
        float norm1 = 0;
        float norm2 = 0;

        for (int i = 0; i < v1.Count; i++)
        {
            dotProduct += v1[i] * v2[i];
            norm1 += v1[i] * v1[i];
            norm2 += v2[i] * v2[i];
        }

        return dotProduct / (float)(Math.Sqrt(norm1) * Math.Sqrt(norm2));
    }
}
