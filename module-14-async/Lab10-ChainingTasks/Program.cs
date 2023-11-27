namespace Lab10_ChainingTasks
{
    internal class Program
    {
        public static async Task Main()
        {
            Task<DayOfWeek> taskA = Task.Run(() => DateTime.Today.DayOfWeek);

            await taskA.ContinueWith(antecedent => Console.WriteLine($"Today is {antecedent.Result}."));

            var task = Task.Run(
                () =>
                {
                    DateTime date = DateTime.Now;
                    return date.Hour > 17
                       ? "evening"
                       : date.Hour > 12
                           ? "afternoon"
                           : "morning";
                });

            await task.ContinueWith(
                antecedent =>
                {
                    Console.WriteLine($"Good {antecedent.Result}!");
                    Console.WriteLine($"And how are you this fine {antecedent.Result}?");
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
