using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SocialFeed
{
    [Table("Attachment")]
    public class Attachment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public required string PublicId { get; set; }
        public required string FileUrl { get; set; }

        public virtual Post? Post { get; set; }
    }
}
