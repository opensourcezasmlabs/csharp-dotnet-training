using System.Diagnostics;
namespace Lab9_CancelTasks
{
    class Program
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();
        static readonly HttpClient s_client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };
        static readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://learn.microsoft.com/en-us/dotnet/",
            "https://learn.microsoft.com/en-us/azure/",
            "https://learn.microsoft.com/en-us/aspnet/",
            "https://learn.microsoft.com/en-us/dotnet/maui/",
            "https://learn.microsoft.com/en-us/power-apps/",
            "https://learn.microsoft.com/en-us/windows/"
        };
        static async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            int total = 0;
            foreach (string url in s_urlList)
            {
                int contentLength = await ProcessUrlAsync(url, s_client, s_cts.Token);
                total += contentLength;
            }
            stopwatch.Stop();
            Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        }
        static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            byte[] content = await response.Content.ReadAsByteArrayAsync(token);
            Console.WriteLine($"{url,-60} {content.Length,10:#,#}");
            return content.Length;
        }
static async Task Main()
{
    Console.WriteLine("Application started.");
    Console.WriteLine("Press the ENTER key to cancel...\n");
    Task cancelTask = Task.Run(() =>
    {
        while (Console.ReadKey().Key != ConsoleKey.Enter)
        {
            Console.WriteLine("Press the ENTER key to cancel...");
        }
        Console.WriteLine("\nENTER key pressed: cancelling downloads.\n");
        s_cts.Cancel();
    });
    Task sumPageSizesTask = SumPageSizesAsync();
    Task finishedTask = await Task.WhenAny(new[] { cancelTask, sumPageSizesTask });
    if (finishedTask == cancelTask)
    {
        try
        {
            await sumPageSizesTask;
            Console.WriteLine("Download task completed before cancel request was processed.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Download task has been cancelled.");
        }
    }
    Console.WriteLine("Application ending.");
}
    }
}