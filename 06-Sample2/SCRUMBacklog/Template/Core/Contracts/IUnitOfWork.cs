namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ICommentRepository     CommentRepository     { get; }
    public IEffortRepository      EffortRepository      { get; }
    public IBacklogItemRepository BacklogItemRepository { get; }
    public ITeamMemberRepository  TeamMemberRepository  { get; }
}