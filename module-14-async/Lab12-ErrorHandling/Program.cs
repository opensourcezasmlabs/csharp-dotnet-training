namespace Lab12_ErrorHandling
{
    class Program
    {
        static async Task Main()
        {
            //await BasicHandler();
            await ExtensionHanlder();
        }

        static async Task BasicHandler()
        {
            var task = SomeAsyncOperation();

            try
            {
                var result = await task;
                Console.WriteLine($"Result: {result}");
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.Flatten().ToString());
            }
        }
        static async Task<int> SomeAsyncOperation()
        {
            if (DateTime.Now.Second % 2 == 0)
                throw new Exception("The time is not right Exception");

            await Task.Delay(1000);
            return 42;
        }

        static async Task ExtensionHanlder()
        {
            await SomeAsyncOperation().HandleExceptionsAsync();
        }
    }
}