using Newtonsoft.Json;

namespace Library_Management_System.Models
{
    public class Member
    {
        [JsonProperty(PropertyName = "memberUId", NullValueHandling = NullValueHandling.Ignore)]
        public string MemberUId { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

    }
}
