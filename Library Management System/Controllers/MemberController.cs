using Azure;
using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management_System.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : Controller
    {
        public string URI = "https://localhost:8081";
        public string primarykey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library Management System";
        public string ContainerName = "LibraryContainer";
        public Container container;
        public MemberController() 
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
        public async Task<Member> AddMember(Member member)
        {
            //here created obj of entity and mapped all the fields from model to entity
            MemberEntity member1 = new MemberEntity();
            member1.Name = member.Name;
            member1.DateOfBirth = member.DateOfBirth;
            member1.Email = member.Email;
            //here values assigned to the necessary fields
            member1.Id = Guid.NewGuid().ToString();
            member1.UId = member1.Id;
            member1.DocumentType = "member";
            member1.Version = 1;
            member1.UpdatedBy = "naveen";
            member1.UpdatedOn = DateTime.Now;
            member1.CreatedOn = DateTime.Now;
            member1.CpdatedBy = "";
            member1.Active = true;
            member1.Archieve = false;
            //data added to the database
            MemberEntity response = await container.CreateItemAsync(member1);
            Member member2 = new Member();
            member2.MemberUId = response.UId;
            member2.Name = response.Name;
            member2.DateOfBirth = response.DateOfBirth;
            member2.Email = response.Email;
            // here return the model
            return member2;

        }
        [HttpGet]
        public async Task<List<Member>> GetallMembers()
        {
            var AllMembers = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.Active == true && q.Archieve == false && q.DocumentType == "member").ToList();
            List<Member> members = new List<Member>();
            foreach (var member in AllMembers)
            {
                Member member1 = new Member();
                member1.MemberUId = member.UId;
                member1.Name = member.Name;
                member1.DateOfBirth = member.DateOfBirth;
                member1.Email = member.Email;
                members.Add(member1);
            }
            return members;
        }
        [HttpGet]
        public async Task<Member> GetMemberByUId(String UID)
        {
            var result = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == UID && q.Active == true && q.Archieve == false).FirstOrDefault();

            Member member1 = new Member();
            member1.MemberUId = result.UId;
            member1.Name = result.Name;
            member1.DateOfBirth = result.DateOfBirth;
            member1.Email = result.Email;
            return member1;
        }
        [HttpPost]
        public async Task<Member> UpdateMember(Member member)
        {
            var existingmember = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == member.MemberUId && q.Active == true && q.Archieve == false).FirstOrDefault();
            existingmember.Active = false;
            existingmember.Active = true;
            await container.ReplaceItemAsync(existingmember, existingmember.UId);
            existingmember.Id = Guid.NewGuid().ToString();
            existingmember.Active = true;
            existingmember.Archieve = false;
            existingmember.Version = existingmember.Version + 1;
            existingmember.UpdatedOn = DateTime.Now;
            existingmember.UpdatedBy = "naveen";
            existingmember.Name = member.Name;
            existingmember.DateOfBirth = member.DateOfBirth;
            existingmember.Email = member.Email;
            existingmember = await container.CreateItemAsync(existingmember);
            Member response = new Member();
            response.MemberUId = member.MemberUId;
            response.Name = member.Name;
            response.DateOfBirth = member.DateOfBirth;
            response.Email = member.Email;
            return response;
        }

    }
}
