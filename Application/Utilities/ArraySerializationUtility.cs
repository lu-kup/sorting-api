using Domain.Models.Validation;

namespace Application.Utilities;

public static class ArraySerializationUtility
{
    public static string Serialize(int[] array)
        => string.Join(' ', array);

    public static int[] Deserialize(string numberLine)
    {
        NumberLineValidation.Validate(numberLine);

        var stringArray = numberLine.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var intArray = Array.ConvertAll(stringArray, int.Parse);

        return intArray;
    }
}
