namespace MPChat.Types.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int RecipientId { get; set; }
        public string Text { get; set; }
        
        public Recipient Recipient { get; set; }
    }
}
