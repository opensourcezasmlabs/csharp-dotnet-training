using System.Diagnostics;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Environment.ProcessId: {Environment.ProcessId}");
            Console.WriteLine($"Environment.CurrentManagedThreadId: {Environment.CurrentManagedThreadId}");

            var thread = new Thread(() =>
            {
                Console.WriteLine($"Environment.ProcessId: {Environment.ProcessId}");
                Console.WriteLine($"Environment.CurrentManagedThreadId: {Environment.CurrentManagedThreadId}");

                for (int i = 0; i < 10; i += 2)
                {
                    Console.WriteLine($"Even Thread: i : {i}");
                    Thread.Sleep(100);
                }

                Console.WriteLine("Even Thread completed");
            });

            var thread2 = new Thread(() =>
            {
                Console.WriteLine($"Environment.ProcessId: {Environment.ProcessId}");
                Console.WriteLine($"Environment.CurrentManagedThreadId: {Environment.CurrentManagedThreadId}");

                for (int i = 1; i < 10; i += 2)
                {
                    Console.WriteLine($"Odd Thread: i : {i}");
                    Thread.Sleep(100);
                }

                Console.WriteLine("Odd Thread completed");
            });
            thread.Start();
            thread2.Start();

            Console.WriteLine($"Main Thread: {Environment.CurrentManagedThreadId} Completed. Press Enter to Exit.");
            Console.ReadLine();
        }
    }
}
