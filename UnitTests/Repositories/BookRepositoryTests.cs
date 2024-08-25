using Microsoft.EntityFrameworkCore;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace UnitTests.Repositories;

public class BookRepositoryTests
{
    private readonly DbContextOptions<BookContext> _options;

    public BookRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Theory]
    [InlineData("harry")]
    [InlineData("HARRY")]
    public async Task FindByTitleOrAuthorAsync_WithTitleMatchingSearchTerm_ReturnsCorrectEntity(string searchTerm)
    {
        // Arrange
        var matchingBookCreateDTO = new BookCreateDTO()
        {
            Author = "JK Rowling",
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = "978-0-313-32067-5",
            PublicationYear = 1997
        };

        var unmatchedBookCreateDTO = new BookCreateDTO()
        {
            Author = "J. R. R. Tolkien",
            Title = "Lord of the Rings",
            ISBN = "9788845292613",
            PublicationYear = 1954
        };

        var matchingBook = new Book(matchingBookCreateDTO);
        var unmatchedBook = new Book(unmatchedBookCreateDTO);

        using (var context = new BookContext(_options))
        {
            await context.Books.AddAsync(matchingBook);
            await context.Books.AddAsync(unmatchedBook);
            await context.SaveChangesAsync();
        }

        // Act
        IEnumerable<Book> results;

        using (var context = new BookContext(_options))
        {
            var repository = new BookRepository(context);
            results = await repository.FindByTitleOrAuthorAsync(searchTerm);
        }

        // Assert
        Assert.Single<Book>(results);
        Assert.Equal(matchingBook.Title, results.First().Title);
        Assert.Equal(matchingBook.Author, results.First().Author);
    }

    [Theory]
    [InlineData("rowling")]
    [InlineData("ROWLING")]
    public async Task FindByTitleOrAuthorAsync_WithAuthorMatchingSearchTerm_ReturnsCorrectEntity(string searchTerm)
    {
        // Arrange
        var matchingBookCreateDTO = new BookCreateDTO()
        {
            Author = "JK Rowling",
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = "978-0-313-32067-5",
            PublicationYear = 1997
        };

        var unmatchedBookCreateDTO = new BookCreateDTO()
        {
            Author = "J. R. R. Tolkien",
            Title = "Lord of the Rings",
            ISBN = "9788845292613",
            PublicationYear = 1954
        };

        var matchingBook = new Book(matchingBookCreateDTO);
        var unmatchedBook = new Book(unmatchedBookCreateDTO);

        using (var context = new BookContext(_options))
        {
            await context.Books.AddAsync(matchingBook);
            await context.Books.AddAsync(unmatchedBook);
            await context.SaveChangesAsync();
        }

        // Act
        IEnumerable<Book> results;

        using (var context = new BookContext(_options))
        {
            var repository = new BookRepository(context);
            results = await repository.FindByTitleOrAuthorAsync(searchTerm);
        }

        // Assert
        Assert.Single<Book>(results);
        Assert.Equal(matchingBook.Title, results.First().Title);
        Assert.Equal(matchingBook.Author, results.First().Author);
    }

    [Theory]
    [InlineData("Kudirka")]
    [InlineData("Brolis")]
    public async Task FindByTitleOrAuthorAsync_WithNoMatchingData_ReturnsEmptyCollection(string searchTerm)
    {
        // Arrange
        var matchingBookCreateDTO = new BookCreateDTO()
        {
            Author = "JK Rowling",
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = "978-0-313-32067-5",
            PublicationYear = 1997
        };

        var unmatchedBookCreateDTO = new BookCreateDTO()
        {
            Author = "J. R. R. Tolkien",
            Title = "Lord of the Rings",
            ISBN = "9788845292613",
            PublicationYear = 1954
        };

        var matchingBook = new Book(matchingBookCreateDTO);
        var unmatchedBook = new Book(unmatchedBookCreateDTO);

        using (var context = new BookContext(_options))
        {
            await context.Books.AddAsync(matchingBook);
            await context.Books.AddAsync(unmatchedBook);
            await context.SaveChangesAsync();
        }

        // Act
        IEnumerable<Book> results;

        using (var context = new BookContext(_options))
        {
            var repository = new BookRepository(context);
            results = await repository.FindByTitleOrAuthorAsync(searchTerm);
        }

        // Assert
        Assert.Empty(results);
    }
}
