namespace Lab8_TaskCompletions
{
    class Program
    {
        static string[] urls = new string[] {
            "https://learn.microsoft.com/en-us/dotnet/",
            "https://learn.microsoft.com/en-us/azure/",
            "https://learn.microsoft.com/en-us/aspnet/",
            "https://learn.microsoft.com/en-us/dotnet/maui/",
            "https://learn.microsoft.com/en-us/power-apps/",
            "https://learn.microsoft.com/en-us/windows/"
        };
        static async Task Main(string[] args)
        {
            var tasks = new List<Task>();
            foreach (var url in urls)
            {
                var task = DownloadAsync(url);
                tasks.Add(task);
                Console.WriteLine($"Task ID: {task.Id} URL: {url}");
            }

            while (tasks.Any())
            {
                var completedTask = await Task.WhenAny(tasks);
                tasks.Remove(completedTask);
                Console.WriteLine($"Task Completed: {completedTask.Id}");
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
        public static async Task DownloadAsync(string url)
        {
            Console.WriteLine($"Downloading URL: {url}");
            var client = new HttpClient();
            var data = await client.GetStringAsync(url);
            Console.WriteLine($"Download completed: {url} Data Length: {data.Length}");
        }
    }
}
