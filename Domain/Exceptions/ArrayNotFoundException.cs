namespace Domain.Exceptions;

public class ArrayNotFoundException : Exception
{
    private const string NotFoundMessage = "Array was not found.";

    public ArrayNotFoundException() : base(NotFoundMessage)
    {
    }
}
