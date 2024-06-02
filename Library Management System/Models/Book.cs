
using Newtonsoft.Json;

namespace Library_Management_System.Models
{
    public class Book
    {
        [JsonProperty(PropertyName = "bookUId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookUId { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "publishDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime PublishedDate { get; set; }

        [JsonProperty(PropertyName = "iSBN", NullValueHandling = NullValueHandling.Ignore)]
        public string ISBN { get; set; }

        [JsonProperty(PropertyName = "isIssued", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean IsIssued { get; set; }
       
       
    }
}
