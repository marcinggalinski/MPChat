namespace MPChat.Types.Models
{
    public class GroupMembers
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
