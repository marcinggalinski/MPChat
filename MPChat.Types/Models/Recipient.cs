using System.Collections.Generic;

namespace MPChat.Types.Models
{
    public abstract class Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Message> Messages { get; set; }
    }
}
