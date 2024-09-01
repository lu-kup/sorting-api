namespace Domain.Exceptions;

public class ArrayNotFoundException : Exception
{
    private const string NotFoundMessage = "Saved array was not found. Please use the other endpoints to enter data.";

    public ArrayNotFoundException() : base(NotFoundMessage)
    {
    }
}
