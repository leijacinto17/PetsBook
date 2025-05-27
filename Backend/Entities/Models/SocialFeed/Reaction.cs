using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Models.SocialFeed
{
    [Table("Reaction")]
    public class Reaction : BaseEntity
    {
        public string? UserId { get; set; }
        public int? PostId { get; set; }

        public ReactionType ReactionType { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
