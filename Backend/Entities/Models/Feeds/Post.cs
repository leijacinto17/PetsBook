using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Feeds
{
    [Table("Post")]
    public class Post
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        public virtual required User User { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; } = [];
        public virtual ICollection<Attachment> Attachments { get; set; } = [];
    }
}
