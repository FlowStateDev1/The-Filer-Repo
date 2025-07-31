using TheFiler.BlazorApp1.Data;

namespace TheFiler.BlazorApp1.Services
{
    public class documentFolder
    {
        // Creates a private field that stores a document's respective path
        private readonly string documentsPath;
        
        // Creates a public class list of documents
        public List<Document> Documents { get; set; }
        
        
        public documentFolder()
        {

            documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "MockFiles");

            // The pre-configured example files
            Documents = new List<Document>
            {
            new Document { Id = 1, Name = "Example(1)", Path = Path.Combine(documentsPath!, "Example(1).txt") },
            new Document { Id = 2, Name = "Example(2)", Path = Path.Combine(documentsPath!, "Example(2).txt") },
            new Document { Id = 3, Name = "Example(3)", Path = Path.Combine(documentsPath!, "Example(3).txt") },
            };
        }
    }
}