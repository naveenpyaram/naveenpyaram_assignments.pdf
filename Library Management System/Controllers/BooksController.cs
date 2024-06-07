using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using static System.Reflection.Metadata.BlobBuilder;


namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : Controller
    {
        public string URI = "https://localhost:8081";
        public string primarykey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library Management System";
        public string ContainerName = "LibraryContainer";
        public Container container;
        public BooksController()
        {
            container = GetContainer();
        }
        private Container GetContainer()
        {
            CosmosClient cosmosCleint = new CosmosClient(URI, primarykey);
            Database database = cosmosCleint.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

        [HttpPost]
        public async Task<Book> AddBooks(Book book)
        {
            //here created obj of entity and mapped all the fields from model to entity
            BookEntity student = new BookEntity();
            student.Title = book.Title;
            student.Author = book.Author;
            student.ISBN = book.ISBN;
            student.PublishedDate = book.PublishedDate;
            student.IsIssued = book.IsIssued;
            //here values assigned to the necessary fields
            student.Id = Guid.NewGuid().ToString();
            student.UId = student.Id;
            student.DocumentType = "book";
            student.Version = 1;
            student.UpdatedBy = "naveen";
            student.UpdatedOn = DateTime.Now;
            student.CreatedOn = DateTime.Now;
            student.CpdatedBy = "";
            student.Active = true;
            student.Archieve = false;
            //data added to the database
            BookEntity response = await container.CreateItemAsync(student);
            Book book1 = new Book();
            book1.BookUId = response.UId;
            book1.Title = response.Title;
            book1.Author = response.Author;
            book1.ISBN = response.ISBN;
            book1.PublishedDate = response.PublishedDate;
            book1.IsIssued = response.IsIssued;
            // here return the model
            return book1;

        }
        [HttpGet]
        public async Task<List<Book>> GetallBooks()
        {
            var AllBooks = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archieve == false && q.DocumentType == "book").ToList();
            List<Book> books = new List<Book>();
            foreach (var book in AllBooks)
            {
                Book book1 = new Book();
                book1.BookUId = book.UId;
                book1.Title = book.Title;
                book1.Author = book.Author;
                book1.ISBN = book.ISBN;
                book1.PublishedDate = book.PublishedDate;
                book1.IsIssued = book.IsIssued;
                books.Add(book1);
            }
            return books;
        }
        [HttpGet]
        public async Task<List<Book>> GetallIssuedBooks()
        {
            var AllBooks = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archieve == false && q.DocumentType == "book" && q.IsIssued == true).ToList();
            List<Book> books = new List<Book>();
            foreach (var book in AllBooks)
            {
                Book book1 = new Book();
                book1.BookUId = book.UId;
                book1.Title = book.Title;
                book1.Author = book.Author;
                book1.ISBN = book.ISBN;
                book1.PublishedDate = book.PublishedDate;
                book1.IsIssued = book.IsIssued;
                books.Add(book1);
            }
            return books;
        }
        [HttpGet]
        public async Task<List<Book>> GetallNotIssuedBooks()
        {
            var AllBooks = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archieve == false && q.DocumentType == "book" && q.IsIssued == false).ToList();
            List<Book> books = new List<Book>();
            foreach (var book in AllBooks)
            {
                Book book1 = new Book();
                book1.BookUId = book.UId;
                book1.Title = book.Title;
                book1.Author = book.Author;
                book1.ISBN = book.ISBN;
                book1.PublishedDate = book.PublishedDate;
                book1.IsIssued = book.IsIssued;
                books.Add(book1);
            }
            return books;
        }
        [HttpGet]
        public async Task<Book> GetBookByUId(String UID)
        {
            var result = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == UID && q.Active == true && q.Archieve == false).FirstOrDefault();
            Book book1 = new Book();
            book1.BookUId = result.UId;
            book1.Title = result.Title;
            book1.Author = result.Author;
            book1.ISBN = result.ISBN;
            book1.PublishedDate = result.PublishedDate;
            book1.IsIssued = result.IsIssued;
            return book1;
        }
        [HttpGet]
        public async Task<Book> GetBookByName(String Title)
        {
            var result = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Title == Title && q.Active == true && q.Archieve == false).FirstOrDefault();
            Book book1 = new Book();
            book1.BookUId = result.UId;
            book1.Title = result.Title;
            book1.Author = result.Author;
            book1.ISBN = result.ISBN;
            book1.PublishedDate = result.PublishedDate;
            book1.IsIssued = result.IsIssued;
            return book1;
        }
        [HttpPost]
        public async Task<Book> UpdateBook(Book book)
        {
            var existingbook = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == book.BookUId && q.Active == true && q.Archieve == false).FirstOrDefault();
            existingbook.Active = false;
            existingbook.Archieve = true;
            await container.ReplaceItemAsync(existingbook, existingbook.UId);
            existingbook.Id = Guid.NewGuid().ToString();
            existingbook.Active = true;
            existingbook.Archieve = false;
            existingbook.Version = existingbook.Version + 1;
            existingbook.UpdatedOn = DateTime.Now;
            existingbook.UpdatedBy = "naveen";
            existingbook.Author = book.Author;
            existingbook.ISBN = book.ISBN;
            existingbook.PublishedDate = book.PublishedDate;
            existingbook.Title = book.Title;
            existingbook.IsIssued = book.IsIssued;
            existingbook = await container.CreateItemAsync(existingbook);
            Book response = new Book();
            response.BookUId = book.BookUId;
            response.Title = book.Title;
            response.Author = book.Author;
            response.ISBN = book.ISBN;
            response.PublishedDate = book.PublishedDate;
            response.IsIssued = book.IsIssued;
            return response;
        }
   
    }
}
