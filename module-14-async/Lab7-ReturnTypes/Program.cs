namespace Lab7_ReturnTypes
{
    class Program
    {
        static Random s_rnd = new();
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Application Launched.");
            await DisplayCurrentInfoAsync();
            Console.WriteLine($"Leisure Hours: {await GetLeisureHoursAsync()}");
            Console.WriteLine($"Dice Rolls: {await GetDiceRollAsync()}");
        }
        async static Task DisplayCurrentInfoAsync()
        {
            await WaitAndApologizeAsync();

            Console.WriteLine($"Today is {DateTime.Now:D}");
            Console.WriteLine($"The current time is {DateTime.Now.TimeOfDay:t}");
            Console.WriteLine("The current temperature is 76 degrees.");
        }
        async static Task WaitAndApologizeAsync()
        {
            await Task.Delay(2000);

            Console.WriteLine("Sorry for the delay...\n");
        }
        async static Task<int> GetLeisureHoursAsync()
        {
            DayOfWeek today = await Task.FromResult(DateTime.Now.DayOfWeek);

            int leisureHours =
                today is DayOfWeek.Saturday || today is DayOfWeek.Sunday
                ? 16 : 5;

            return leisureHours;
        }
        async static ValueTask<int> GetDiceRollAsync()
        {
            Console.WriteLine("Shaking dice...");

            int roll1 = await RollAsync(s_rnd);
            int roll2 = await RollAsync(s_rnd);

            return roll1 + roll2;
        }
        static async ValueTask<int> RollAsync(Random s_rnd)
        {
            await Task.Delay(500);

            int diceRoll = s_rnd.Next(1, 7);
            return diceRoll;
        }
    }
}