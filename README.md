# GitHub Star AI Tool

## Overview
GitHub Star AI Tool is a Blazor WebAssembly application that helps users find similar GitHub repositories based on their starred repositories. It leverages vector embeddings and cosine similarity to provide recommendations.

## Features
- Search for starred repositories of a GitHub user.
- Store repository vectors in IndexedDB for offline access.
- Find similar repositories based on a search text.
- Load more repositories and handle pagination.

## Installation
1. Clone the repository:
   
2. Install the required .NET SDK (version 9.0 or later).
3. Restore the dependencies:
   
4. Build the project:
   
5. Run the project:
   

## Configuration
The application uses an `appsettings.json` file for configuration. Update the file with your GitHub and embedding service credentials.

### appsettings.json

- `IndexedDB`: Configuration for IndexedDB.
  - `DbName`: The name of the IndexedDB database.
  - `StoreName`: The name of the object store in IndexedDB.
- `VectorSimilarity`: Configuration for vector similarity.
  - `TopK`: The number of top similar repositories to return.
- `GitHubService`: Configuration for GitHub API.
  - `Token`: The GitHub API token for authentication.
- `EmbeddingService`: Configuration for the embedding service API.
  - `BaseAddress`: The base address of the embedding service API.
  - `ApiKey`: The API key for the embedding service.

## Usage
1. Open the application in your browser.
2. Enter a GitHub username and click "Load All Repos" to fetch the starred repositories.
3. Enter a search text and click "Search Similar" to find similar repositories.

## Dependencies
- .NET 9.0 or later
- Blazor WebAssembly
- IndexedDB
- GitHub API
- Embedding Service API
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
