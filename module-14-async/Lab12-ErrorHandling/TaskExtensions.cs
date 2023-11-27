namespace Lab12_ErrorHandling
{
    public static class TaskExtensions
    {
        public static async Task HandleExceptionsAsync(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        public static async Task<TResult> HandleExceptionsAsync<TResult>(this Task<TResult> task)
        {
            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                return default(TResult);
            }
        }

        public static async Task<TResult> HandleExceptionsAsync<TResult>(this Task<TResult> task, Func<Exception, TResult> exceptionHandler)
        {
            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                return exceptionHandler(ex);
            }
        }
    }
}
