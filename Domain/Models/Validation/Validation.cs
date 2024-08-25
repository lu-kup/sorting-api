using System.ComponentModel.DataAnnotations;
using System;
using System.Text.RegularExpressions;

namespace Domain.Models.Validation;

public static class BookValidation
{
    private const string MatchPatternISBN = @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$";
    private const string InvalidIsbnMessage = "Provided ISBN code is invalid.";
    private const string InvalidPublicationYearMessage = "Provided publication year is invalid.";
    private const string InvalidStringMessage = "Provided string value is null or empty.";

    private static readonly Regex _regexISBN = new(MatchPatternISBN, RegexOptions.Compiled);

    public static void ValidateISBN(string isbn)
    {
        if (!_regexISBN.IsMatch(isbn))
        {
            throw new ValidationException(InvalidIsbnMessage);
        }
    }

    public static void ValidatePublicationYear(int publicationYear)
    {
        if (publicationYear > DateTime.UtcNow.Year || publicationYear < 1000)
        {
            throw new ValidationException(InvalidPublicationYearMessage);
        }
    }

    public static void ValidateIsNullOrWhiteSpace(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException(InvalidStringMessage);
        }
    }
}
