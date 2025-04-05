namespace Application.Common.Exceptions;

public class HandlerExecutionException : Exception
{
    public HandlerExecutionException(string handlerName, Exception innerException)
        : base($"Erro ao processar o handler: {handlerName}", innerException)
    {
    }
}
