namespace GithubStarAITool.Services;

public interface IEmbeddingService
{
    Task<List<float>> GetEmbeddingAsync(string text);
}
