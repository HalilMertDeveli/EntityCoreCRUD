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
            AuthorContext authorContext = new AuthorContext();

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


            //try
            //{
            //    CrudOperationAdd.AddNewAuthor(author, authorContext);//it is working
            //    CrudOperationAdd.AddNewAuthorInfo(authorInfo, authorContext);
            //    CrudOperationAdd.AddNewBook(book, authorContext);
            //    authorContext.SaveChanges();

            //}
            //catch
            //{


            //}

            //Console.WriteLine("New records has been added");
            #endregion
            #region DeleteOperaiton
            CrudOperationDelete crudOperationDeleteInstance = new CrudOperationDelete();
            //crudOperationDeleteInstance.DeleteFromAuthor(authorContext, 15, "GG");//working
            //crudOperationDeleteInstance.DeleteFromBooks(authorContext, 13, "GG");//working
            // crudOperationDeleteInstance.DeleteFromAuthorInfo(authorContext, 13, "F. Scott Fitzgerald was an American novelist...");
            #endregion

            CrudOperationUpdate crudOperationUpdateInstance = new CrudOperationUpdate();
            //crudOperationUpdateInstance.UpdateAuthor(authorContext, 1, "HMDSS", "Develi", "TR");//working
            //crudOperationUpdateInstance.UpdateBook(authorContext, 1, "Irade Terbiyesi"); // working
            //crudOperationUpdateInstance.UpdateAuthorInfo(authorContext, 1, "Yenikapı");//working

            Console.WriteLine("1 -1 and 1 - to many example for week 6, Add operation is done" +
                "Delete operation is done" +
                "ss");



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
public class CrudOperationDelete
{
    public void DeleteFromAuthor(AuthorContext context, int entityId = 0, string stringEntityName = "")//this is for author
    {
        Author? authorForDelete = context.AuthorDbSet.FirstOrDefault(authorInstance => authorInstance.AuthorName == stringEntityName && authorInstance.AuthorId == entityId);
        if (authorForDelete != null)
        {
            context.AuthorDbSet.Remove(authorForDelete);
            context.SaveChanges();
        }
    }
    public void DeleteFromBooks(AuthorContext context, int entityId = 0, string stringEntityName = "")//this is for books
    {
        Book? bookForDelete = context.BookDbSet.FirstOrDefault(bookInstance => bookInstance.BookID == entityId && bookInstance.BookName == stringEntityName);
        if (bookForDelete != null)
        {
            context.BookDbSet.Remove(bookForDelete);
            context.SaveChanges();
        }
    }
    public void DeleteFromAuthorInfo(AuthorContext context, int entityId = 0, string stringEntityName = "")//this is for author
    {
        AuthorInfo? authorInfo = context.AuthorInfoDbSet.FirstOrDefault(authorInfoInstance => authorInfoInstance.AuthorId == entityId && authorInfoInstance.AuthorInformation == stringEntityName);
        if (authorInfo != null)
        {
            context.AuthorInfoDbSet.Remove(authorInfo);
            context.SaveChanges();
        }
    }

}
public class CrudOperationUpdate
{
    public void UpdateAuthor(AuthorContext context, int authorId = 0, string newAuthorName = "", string newAuthorLastName = "", string newAuthorNation = "")
    {
        Author? authorForUpdate = context.AuthorDbSet.FirstOrDefault(authorInstnce => authorInstnce.AuthorId == authorId);

        if (authorForUpdate !=  null)
        {
            authorForUpdate.AuthorName = newAuthorName;
            authorForUpdate.AuthorLastName = newAuthorLastName;
            authorForUpdate.AuthorNationality = newAuthorNation;
            context.Entry(authorForUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
    public void UpdateBook(AuthorContext context,int bookId=0,string newBookName = "")
    {
        Book? bookForUpdate = context.BookDbSet.FirstOrDefault(bookInstance => bookInstance.BookID == bookId);
        if (bookForUpdate != null)
        {
            bookForUpdate.BookName = newBookName;
            context.Entry(bookForUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
    public void UpdateAuthorInfo(AuthorContext context,int authorId=0,string newAuthorInfo="")
    {
        AuthorInfo? authorInfoForUpdate = context.AuthorInfoDbSet.FirstOrDefault(authorInfoDbSet => authorInfoDbSet.AuthorId == authorId);
        if (authorInfoForUpdate != null)
        {
            authorInfoForUpdate.AuthorInformation = newAuthorInfo;
            context.Entry(authorInfoForUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}