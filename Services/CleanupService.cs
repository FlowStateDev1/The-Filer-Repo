namespace TheFiler.BlazorApp1.Services
{
    public class CleanupService : IHostedService
    {
        private readonly IHostApplicationLifetime _lifetime;

        public CleanupService(IHostApplicationLifetime lifetime)
        {
            _lifetime = lifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStopping.Register(CleanUp);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStopping.Register(CleanUp);
            return Task.CompletedTask;
        }

        // Cleans up the MockFile folder from any added files to preserve examples
        private void CleanUp()
        {
            var folder = "MockFiles";

            // The preserved files hashset allows fast list checks  
            var preservedFiles = new HashSet<string>
            {
                "Example(1).txt",
                "Example(2).txt",
                "Example(3).txt"
            };

            if (Directory.Exists(folder))
            {
                var allFiles = Directory.GetFiles(folder);
                foreach (var filePath in allFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    if (!preservedFiles.Contains(fileName))
                    {
                        File.Delete(filePath);
                        Console.WriteLine($"Removing:{fileName}");
                    }
                }
            }

        }
    }
}
