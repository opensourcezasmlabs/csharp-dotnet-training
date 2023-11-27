using System.Diagnostics;

namespace Lab11_TaskCompletionSource
{
    class Program
    {
        static TaskCompletionSource<int> tcs1 = new TaskCompletionSource<int>();

        static async Task Main()
        {
            Console.WriteLine("App Launched");
            Task<int> t1 = tcs1.Task;

            Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Operation Launched");
                await Task.Delay(2000);
                tcs1.SetResult(19);
                Console.WriteLine("Operation Completed");
            });

            Stopwatch sw = Stopwatch.StartNew();
            int result = await t1;
            sw.Stop();

            Console.WriteLine("(ElapsedTime={0}): t1.Result={1}", sw.Elapsed, result);
        }
    }
}
