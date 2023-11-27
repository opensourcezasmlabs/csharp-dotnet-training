namespace Lab4_HelloAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var contentLengthTask = GetUrlContentLengthAsync();
            Console.WriteLine("Main Function resumed");
            var length = await contentLengthTask;
            Console.WriteLine($"Length: {length}");
        }

        public static async Task<int> GetUrlContentLengthAsync()
        {
            var client = new HttpClient();

            Task<string> getStringTask =
                client.GetStringAsync("https://learn.microsoft.com/en-us/dotnet/");

            DoIndependentWork();

            string contents = await getStringTask;

            return contents.Length;
        }
        static void DoIndependentWork()
        {
            Console.WriteLine("Working...");
        }
    }
}