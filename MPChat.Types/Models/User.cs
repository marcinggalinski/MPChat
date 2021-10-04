using System.Collections.Generic;

namespace MPChat.Types.Models
{
    public class User : Recipient
    {
        public string EmailAddress { get; set; }
        
        public IEnumerable<GroupMembers> GroupMembers { get; set; }
    }
}
