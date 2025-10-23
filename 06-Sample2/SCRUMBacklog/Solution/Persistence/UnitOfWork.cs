using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public IEffortRepository      EffortRepository      { get; }
    public ICommentRepository     CommentRepository     { get; }
    public IBacklogItemRepository BacklogItemRepository { get; }
    public ITeamMemberRepository  TeamMemberRepository  { get; }

    public UnitOfWork(ApplicationDbContext context,
        IEffortRepository                  effortRepository,
        ICommentRepository                 commentRepository,
        IBacklogItemRepository             backlogItemRepository,
        ITeamMemberRepository              teamMemberRepository
    ) : base(context)
    {
        EffortRepository      = effortRepository;
        CommentRepository     = commentRepository;
        BacklogItemRepository = backlogItemRepository;
        TeamMemberRepository  = teamMemberRepository;
    }
}