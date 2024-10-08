using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Models.Validation;

public static class NumberLineValidation
{
    private const string InvalidCharsMatchPattern = @"[^0-9 ]";
    private const string InvalidStringMessage = "Provided string value is null or empty.";
    private const string InvalidNumberLineMessage = 
        "Provided number line contains invalid characters. Only positive integers separated by spaces allowed.";

    private static readonly Regex _invalidCharsRegex = new(InvalidCharsMatchPattern, RegexOptions.Compiled);

    public static void Validate(string numberLine)
    {
        if (string.IsNullOrWhiteSpace(numberLine))
        {
            throw new ValidationException(InvalidStringMessage);
        }
        if (_invalidCharsRegex.IsMatch(numberLine))
        {
            throw new ValidationException(InvalidNumberLineMessage);
        }
    }
}
