using System;

namespace NFWCommonsLibrary.ErrorHandling
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        public ConsoleErrorHandler()
        {

        }

        public void HandleError(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
