using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Models.SocialFeed
{
    [Table("Reaction")]
    public class Reaction
    {

        public int Id { get; set; }

        // Nullable foreign keys because of SetNull delete behavior
        public string? UserId { get; set; }
        public int? PostId { get; set; }

        public ReactionType ReactionType { get; set; }
        public DateTimeOffset LikeAt { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
