namespace GithubStarAITool.Models;

public class RepositoryVector
{
    public required GitHubRepo Repository { get; set; }
    public required List<float> Vector { get; set; }
    
    // Add a property to expose the ID directly for IndexedDB
    public int Id => Repository.Id;
}
