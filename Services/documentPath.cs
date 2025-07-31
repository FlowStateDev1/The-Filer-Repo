using TheFiler.BlazorApp1.Data;

namespace TheFiler.BlazorApp1.Services;
public static class documentPath
{
    static List<Document> Documents { get; } = new List<Document>();
    static documentPath()
    {
        // Build function to read the doc paths from the appsettings.json
        var config = new ConfigurationBuilder() 
            .AddJsonFile("appsettings.json")
            .Build();

        // Get the path from the appsettings.json file || Self Destructs code if the path is not found
        string documentsPath = config["DocumentSettings:DocumentsPath"] 
            ?? throw new Exception("The desired path for your document is missing in the configuration file."); 
    }
}
