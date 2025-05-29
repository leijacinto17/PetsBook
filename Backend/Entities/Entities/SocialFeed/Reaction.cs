using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Identity;
using Core.Enums;

namespace Core.Entities.SocialFeed
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
