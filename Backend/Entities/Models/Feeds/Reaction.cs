using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Models.Feeds
{
    [Table("Reaction")]
    public class Reaction
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int PostId { get; set; }
        public ReactionType ReactionType { get; set; }
        public DateTimeOffset LikeAt { get; set; }

        public virtual required User User { get; set; }
        public virtual required Post Post { get; set; }
    }
}
