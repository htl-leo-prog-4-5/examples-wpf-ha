using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}