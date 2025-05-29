using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Identity;

namespace Core.Entities.SocialFeed
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        public required string UserId { get; set; }
        public required string Content { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
