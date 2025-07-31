// Configures the Namespace
namespace TheFiler.BlazorApp1.Data
{

    // Document object that will be used in the API and UI components
    public class Document
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

