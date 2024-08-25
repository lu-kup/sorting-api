using Domain.Models.Entities;
using Domain.Models.DTO;

namespace UnitTests.Mocks;

public static class BookMocks
{
    public static Book Create()
    {
        var mockBookCreateDTO = new BookCreateDTO()
        {
            Author = "JK Rowling",
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = "978-0-313-32067-5",
            PublicationYear = 1997
        };

        return new Book(mockBookCreateDTO);
    }
}
