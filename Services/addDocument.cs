using TheFiler.BlazorApp1.Data;

// Utilizes the App's services namespace
namespace TheFiler.BlazorApp1.Services
{
    public class AddDocument
    {
        // Creates a private field that stores a reference to the documentFolder Object
        private readonly documentFolder _documentFolder;
        private readonly string documentsPath;

        // This class is initalized when AddDocument is created 
        public AddDocument(documentFolder documentFolder)
        {
            _documentFolder = documentFolder;
            documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "MockFiles");
        }

        // A public method that takes a document object that sets the new doc's Id, Path, etc
        public Document AddNewDocument(Document doc)
        {

            doc.Id = _documentFolder.Documents.Count + 1; // Assign a new ID based on the current count
            doc.Path = Path.Combine(documentsPath, doc.Name ?? "UnnamedDocument"); // Set the document path
            doc.CreatedAt = DateTime.Now; // Set the creation date
            if (!string.IsNullOrWhiteSpace(doc.Path))
            {
                File.WriteAllText(doc.Path, doc.Content ?? string.Empty);
            }

            _documentFolder.Documents.Add(doc);
            return doc; 
        }
    }
}