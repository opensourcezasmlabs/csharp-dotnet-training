namespace Lab6_CPUBoundAsync
{
    class Program
    {
        static int[] counts = new int[] { 50000, 1000, 200, 300, 500 };
        static async Task Main(string[] args)
        {
            var tasks = new List<Task>();
            foreach (var count in counts)
            {
                var task = Task.Run(() => Process(count));
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
        public static void Process(int count)
        {
            Console.WriteLine($"Started: {count} on Thread ID: {Environment.CurrentManagedThreadId}");
            ulong sum = 0;
            for (uint i = 0; i < count; i++)
            {
                sum += i * i;
            }
            Console.WriteLine($"Completed {count} completed. Total: {sum} on Thread ID: {Environment.CurrentManagedThreadId}");
        }
    }
}