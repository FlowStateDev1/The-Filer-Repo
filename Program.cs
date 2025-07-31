using BlazorApp1.Components;
using TheFiler.BlazorApp1.Data;
using TheFiler.BlazorApp1.Services;

var builder = WebApplication.CreateBuilder(args);

// Adds Razor-side Api support 
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure the HTTPS client for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7070/") });

// Register the Api Services
builder.Services.AddSingleton<documentFolder>();
builder.Services.AddScoped<AddDocument>();
builder.Services.AddScoped<ViewFile>();
builder.Services.AddHostedService<CleanupService>();

//Builds the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Ensures all http requests are secure (http --> https)
app.UseHttpsRedirection();
app.UseRouting();
app.UseAntiforgery();

// Serves static files from wwwroot
app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Endpoint that returns the contents of the document folder || (3 Example Documents inlucded in source code)
app.MapGet("/api/documents", (documentFolder folder) =>
{
    return folder.Documents;
});

// Endpoint for viewing a specified document by ID || Catches errors while fetching desired document || FirstOrDefault is used to find the first document that matches the ID
app.MapGet("/api/documents/{id}", (int id, documentFolder folder) =>
{
    var document = folder.Documents.FirstOrDefault(d => d.Id == id);
    if (document == null)
    {
        return Results.NotFound("Document not found.");
    }
    return Results.Ok(document);
});

// Endpoint for adding a new document into the MockFiles folder
app.MapPost("/api/documents", (Document doc, AddDocument service) =>
{
    var newDoc = service.AddNewDocument(doc);
    return Results.Ok(newDoc);
});

// Endpoint for deleting a document using its id
app.MapDelete("/api/documents/{id}", (int id) =>
{
    var folder = app.Services.GetRequiredService<documentFolder>();
    var document = folder.Documents.FirstOrDefault(d => d.Id == id);

    if (document == null)
    {
        return Results.NotFound("Document not found.");
    }

    // Removes doc from the MockFiles folder by its path
    var path = Path.Combine("MockFiles", document.Name!);
    if (File.Exists(path))
    {
        File.Delete(path);
    }

    folder.Documents.Remove(document);
    return Results.Ok("Document deleted successfully.");
});

app.Run();
