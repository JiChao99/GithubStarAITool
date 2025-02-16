using GithubStarAITool.Models;
using Microsoft.JSInterop;

namespace GithubStarAITool.Services;

public class IndexedDBService : IIndexedDBService
{
    private readonly IJSRuntime _jsRuntime;
    private bool _initialized;

    public IndexedDBService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        if (!_initialized)
        {
            await _jsRuntime.InvokeVoidAsync("initializeIndexedDB");
            _initialized = true;
        }
    }

    public async Task StoreRepositoryVectorAsync(RepositoryVector repoVector)
    {
        if (!_initialized)
        {
            await InitializeAsync();
        }
        await _jsRuntime.InvokeVoidAsync("storeRepositoryVector", repoVector);
    }

    public async Task<List<RepositoryVector>> GetAllRepositoryVectorsAsync()
    {
        if(!_initialized)
        {
            await InitializeAsync();
        }
        return await _jsRuntime.InvokeAsync<List<RepositoryVector>>("getAllRepositoryVectors");
    }

    public async Task ClearAllDataAsync()
    {
        await _jsRuntime.InvokeVoidAsync("clearAllRepositoryData");
    }

    public async Task<RepositoryVector?> GetRepositoryVectorByIdAsync(int id)
    {
        if (!_initialized)
        {
            await InitializeAsync();
        }
        return await _jsRuntime.InvokeAsync<RepositoryVector?>("getRepositoryVectorById", id);
    }
}
