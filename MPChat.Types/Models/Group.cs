using System.Collections.Generic;

namespace MPChat.Types.Models
{
    public class Group : Recipient
    {
        public int Id { get; set; }
        
        public IEnumerable<GroupMembers> GroupMembers { get; set; }
    }
}
