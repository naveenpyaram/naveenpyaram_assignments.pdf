using Newtonsoft.Json;

namespace Library_Management_System.Models
{
    public class Issue
    {

        [JsonProperty(PropertyName = "issueUId", NullValueHandling = NullValueHandling.Ignore)]
        public string IssueUId { get; set; }


        [JsonProperty(PropertyName = "bookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }


        [JsonProperty(PropertyName = "memberId", NullValueHandling = NullValueHandling.Ignore)]
        public string MemberId { get; set; }


        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }


        [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ReturnDate { get; set; }

        [JsonProperty(PropertyName = "isreturned", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean isReturned { get; set; }
        public bool IsReturned { get; internal set; }
        public object UId { get; internal set; }
    }
}
