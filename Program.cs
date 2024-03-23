using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_for_six_week
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region InstanceOfClass
            Author author = new Author
            {
                AuthorName = "GG",
                AuthorLastName = "Fitzgerald",
                AuthorNationality = "American",
                AuthorInfo = new AuthorInfo
                {
                    AuthorInformation = "F. Scott Fitzgerald was an American novelist...",
                    AwardsReceived = 3,
                    FamousWork = "The Great Gatsby"
                }
            };


            Book book = new Book
            {
                BookName = "GG",
                AuthorId = 1,// Bu kitabı yazan yazarın AuthorId'si
                Author = author
            };



            author.Books = new List<Book> { book };

            AuthorInfo authorInfo = new AuthorInfo
            {

                AuthorInformation = "GG",
                AwardsReceived = 3,
                FamousWork = "The Great Gatsby"
            };
            #endregion
            #region AddOperation

            AuthorContext authorContext = new AuthorContext();

            try
            {
                CrudOperationAdd.AddNewAuthor(author, authorContext);//it is working
                CrudOperationAdd.AddNewAuthorInfo(authorInfo, authorContext);
                CrudOperationAdd.AddNewBook(book, authorContext);
                authorContext.SaveChanges();

            }
            catch
            {


            }

            Console.WriteLine("New records has been added");
            #endregion

            Console.WriteLine("1 -1 and 1 - to many example for week 6");
            Console.ReadLine();
        }
    }
}

public class Book
{
    [Key]
    public int BookID { get; set; }
    public string BookName { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }

    // Author sınıfı için referans
    public Author Author { get; set; }
}

public class Author
{
    [Key]
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorLastName { get; set; }
    public string AuthorNationality { get; set; }

    public ICollection<Book> Books { get; set; }

    public AuthorInfo AuthorInfo { get; set; }
}

public class AuthorInfo
{
    [Key]
    public int AuthorId { get; set; }
    public string AuthorInformation { get; set; }
    public int AwardsReceived { get; set; }
    public string FamousWork { get; set; }
}

public class AuthorContext : DbContext
{
    public DbSet<Book> BookDbSet { get; set; }
    public DbSet<Author> AuthorDbSet { get; set; }
    public DbSet<AuthorInfo> AuthorInfoDbSet { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Server=localhost;Database=AuthorDatabase;Trusted_Connection=True;TrustServerCertificate=True");

        base.OnConfiguring(optionsBuilder);
    }
}
public class CrudOperationAdd
{
    public static void AddNewAuthor(Author author, AuthorContext authorContext)
    {
        try
        {
            authorContext.AuthorDbSet.Add(author);
            authorContext.SaveChanges();
        }
        catch
        {

        }

    }
    public static void AddNewBook(Book book, AuthorContext authorContext)
    {
        try
        {
            authorContext.BookDbSet.Add(book);
            authorContext.SaveChanges();
        }
        catch
        {

        }

    }
    public static async void AddNewAuthorInfo(AuthorInfo author, AuthorContext authorContext)
    {
        try
        {
            await authorContext.AuthorInfoDbSet.AddAsync(author);
            await authorContext.SaveChangesAsync();
        }
        catch
        {

        }

    }
}