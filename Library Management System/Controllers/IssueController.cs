using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssueController : Controller
    {
        public string URI = "https://localhost:8081";
        public string primarykey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library Management System";
        public string ContainerName = "LibraryContainer";
        public Container container;
        public IssueController()
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
        public async Task<IActionResult> IssueBook([FromBody] Issue issueModel)
        {
            var book = container.GetItemLinqQueryable<BookEntity>(true)
                                 .Where(b => b.UId == issueModel.BookId && b.Active && !b.Archieve && !b.IsIssued)
                                 .AsEnumerable()
                                 .FirstOrDefault();

            if (book == null)
            {
                return NotFound("Book not found or already issued.");
            }

            var member = container.GetItemLinqQueryable<MemberEntity>(true)
                                   .Where(m => m.UId == issueModel.MemberId && m.Active && !m.Archieve)
                                   .AsEnumerable()
                                   .FirstOrDefault();

            if (member == null)
            {
                return NotFound("Member not found.");
            }

            var issue = new IssueEntity
            {
                Id = Guid.NewGuid().ToString(),
                UId = Guid.NewGuid().ToString(),
                DocumentType = "issue",
                BookId = issueModel.BookId,
                MemberId = issueModel.MemberId,
                IssueDate = DateTime.Now,
                IsReturned = false,
                CreatedBy = "Naveen",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Naveen",
                UpdatedOn = DateTime.Now,
                Version = 1,
                Active = true,
                Archieve = false
            };

            book.IsIssued = true;
            book.UpdatedOn = DateTime.Now;
            book.UpdatedBy = "Naveen";

            await container.ReplaceItemAsync(book, book.Id);
            await container.CreateItemAsync(issue);

            return Ok(issueModel);
        }
        [HttpPost]
        public async Task<IActionResult> ReturnBook([FromBody] Issue issueModel)
        {
            var issue = container.GetItemLinqQueryable<IssueEntity>(true)
                                  .Where(i => i.BookId == issueModel.BookId && i.MemberId == issueModel.MemberId && i.Active && !i.Archieve && !i.IsReturned)
                                  .AsEnumerable()
                                  .FirstOrDefault();

            if (issue == null)
            {
                return NotFound("Issue record not found or book already returned.");
            }

            issue.IsReturned = true;
            issue.UpdatedOn = DateTime.Now;
            issue.UpdatedBy = "Admin";
            await container.ReplaceItemAsync(issue, issue.Id);

            var book = container.GetItemLinqQueryable<BookEntity>(true)
                                 .Where(b => b.UId == issueModel.BookId && b.Active && !b.Archived && b.IsIssued)
                                 .AsEnumerable()
                                 .FirstOrDefault();

            if (book == null)
            {
                return NotFound("Book not found or already returned.");
            }

            book.IsIssued = false;
            book.UpdatedOn = DateTime.Now;
            book.UpdatedBy = "Naveen";
            await container.ReplaceItemAsync(book, book.Id);

            return Ok(issueModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetIssuedBooksByMember(string memberId)
        {
            var issues = container.GetItemLinqQueryable<IssueEntity>(true)
                                   .Where(i => i.MemberId == memberId && i.Active && !i.Archieve && !i.IsReturned)
                                   .ToList();

            var issueModels = issues.Select(issue => new Issue
            {
                UId = issue.UId,
                BookId = issue.BookId,
                MemberId = issue.MemberId,
                IssueDate = issue.IssueDate,
                ReturnDate = issue.ReturnDate,
                IsReturned = issue.IsReturned
            }).ToList();

            return Ok(issueModels);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIssues()
        {
            var issues = container.GetItemLinqQueryable<IssueEntity>(true)
                                   .Where(i => i.Active && !i.Archieve)
                                   .ToList();

            var issueModels = issues.Select(issue => new Issue
            {
                UId = issue.UId,
                BookId = issue.BookId,
                MemberId = issue.MemberId,
                IssueDate = issue.IssueDate,
                ReturnDate = issue.ReturnDate,
                IsReturned = issue.IsReturned
            }).ToList();

            return Ok(issueModels);
        }




    }
}
