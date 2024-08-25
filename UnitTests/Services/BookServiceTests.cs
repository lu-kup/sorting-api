using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Application.Services;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Moq;
using UnitTests.Mocks;

namespace UnitTests.Services;

public class BookServiceTests
{
    private const int MockBookId = 123;

    private readonly Mock<IBookRepository> _mockBookRepository = new();
    private readonly Mock<ILogger<BookService>> _mockLogger = new();

    private readonly IBookService _bookService;

    public BookServiceTests()
    {
        _bookService = new BookService(_mockBookRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithNoBooksFound_Throws()
    {
        // Arrange
        _mockBookRepository.Setup(
            x => x.GetByIdAsync(MockBookId)).ReturnsAsync((Book?)null);

        // Act and assert
        await Assert.ThrowsAsync<BookNotFoundException>(
            async () => await _bookService.GetByIdAsync(MockBookId));
    }

    [Fact]
    public async Task CreateAsync_GivenValidData_CreatesBook()
    {
        // Arrange
        var mockBookCreateDTO = new BookCreateDTO()
        {
            Author = "JK Rowling",
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = "978-0-313-32067-5",
            PublicationYear = 1997
        };

        // Act
        var createdBook = await _bookService.CreateAsync(mockBookCreateDTO);

        // Assert
        Assert.NotNull(createdBook);
        Assert.Equal(mockBookCreateDTO.Title, createdBook.Title);
        Assert.Equal(mockBookCreateDTO.ISBN, createdBook.ISBN);
    }

    [Theory]
    [InlineData("132abc")]
    [InlineData("000-0000")]
    [InlineData("1256")]
    public async Task CreateAsync_GivenInvalidISBN_Throws(string mockISBN)
    {
        // Arrange
        var mockBookCreateDTO = new BookCreateDTO()
        {
            Author = "JK Rowling",
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = mockISBN,
            PublicationYear = 1997
        };

        // Act and assert
        await Assert.ThrowsAsync<ValidationException>(
            async () => await _bookService.CreateAsync(mockBookCreateDTO));
    }

    [Fact]
    public async Task UpdateAsync_GivenValidData_UpdatesBook()
    {
        // Arrange
        var book = BookMocks.Create();

        _mockBookRepository.Setup(
            x => x.GetByIdAsync(book.Id)).ReturnsAsync(book);

        var mockBookUpdateDTO = new BookUpdateDTO()
        {
            ISBN = "1230313320675",
            PublicationYear = 2000
        };

        // Act
        await _bookService.UpdateAsync(book.Id, mockBookUpdateDTO);

        // Assert
        Assert.Equal(mockBookUpdateDTO.ISBN, book.ISBN);
        Assert.Equal(mockBookUpdateDTO.PublicationYear, book.PublicationYear);

        _mockBookRepository.Verify(x => x.Save(), Times.AtLeastOnce);
    }

    [Theory]
    [InlineData(25)]
    [InlineData(-100)]
    [InlineData(2150)]
    public async Task UpdateAsync_GivenInvalidPublicationYear_Throws(int mockYear)
    {
        // Arrange
        var book = BookMocks.Create();

        _mockBookRepository.Setup(
            x => x.GetByIdAsync(book.Id)).ReturnsAsync(book);

        var mockBookUpdateDTO = new BookUpdateDTO()
        {
            Author = "JKR",
            PublicationYear = mockYear
        };

        // Act and assert
        await Assert.ThrowsAsync<ValidationException>(
            async () => await _bookService.UpdateAsync(book.Id, mockBookUpdateDTO));
    }
}
