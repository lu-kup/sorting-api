namespace Domain.Exceptions;

public class ArrayNotFoundException : Exception
{
    private const string NotFoundMessage = "Saved array was not found. Please use other endpoints to enter data.";

    public ArrayNotFoundException() : base(NotFoundMessage)
    {
    }
}
