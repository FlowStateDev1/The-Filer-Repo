using System;
using System.IO;
using TheFiler.BlazorApp1.Data;

namespace TheFiler.BlazorApp1.Services;

public class ViewFile
{
    // Defines a field for the injected HttpClient || Used inside the class to make requests
    private readonly HttpClient _httpClient;

    // A Constructor that automatically passes into the HttpClient 
    public ViewFile(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Method to fetch a document by ID || Returns the documents content if found, otherwise throws an exception || Takes an ID as a parameter
    public async Task<Document?> GetDocumentContentAsync(int id)
    {
        // Makes a GET request to the API endpoint with the specified ID
        var response = await _httpClient.GetAsync($"https://localhost:7070/api/documents/{id}");

        // Ensures the response is successful || Silently handles status codes that arent 2XX
        response.EnsureSuccessStatusCode();

        // Stores the GET resquest's content in a document variable with the subsequent error catches
        var document = await response.Content.ReadFromJsonAsync<Document>();
        if (document == null)
            throw new Exception("Document not found.");

        if (!File.Exists(document.Path))
            throw new FileNotFoundException("File not found", document.Path);

        document.Content = await File.ReadAllTextAsync(document.Path);
        return document;
    }
}