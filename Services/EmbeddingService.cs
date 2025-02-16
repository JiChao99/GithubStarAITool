using System.Net.Http.Json;
using GithubStarAITool.Models;

namespace GithubStarAITool.Services;

public class EmbeddingService : IEmbeddingService
{
    private readonly HttpClient _client;

    public EmbeddingService(IConfiguration configuration)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(configuration["EmbeddingService:BaseAddress"])
        };
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration["EmbeddingService:ApiKey"]);
    }

    public async Task<List<float>> GetEmbeddingAsync(string text)
    {
        var request = new
        {
            model = "BAAI/bge-m3",
            input = text,
            encoding_format = "float"
        };

        var response = await _client.PostAsJsonAsync("v1/embeddings", request);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<EmbeddingResponse>();
        return result?.Data.FirstOrDefault()?.Embedding ?? [];
    }
}
