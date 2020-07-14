using System;

namespace NFWCommonsLibrary.ErrorHandling
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
