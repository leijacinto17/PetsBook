using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.SocialFeed
{
    [Table("Attachment")]
    public class Attachment : BaseEntity
    {
        public int PostId { get; set; }
        public required string PublicId { get; set; }
        public required string FileUrl { get; set; }

        public virtual Post? Post { get; set; }
    }
}
