using Core.Entities.SocialFeed;
using Core.Interfaces;

namespace Infrastructure.Data.Persistence;

public class AttachmentRepository(DataContext context)
    : GenericRepository<Attachment>(context), IAttachmentRepository
{
}