using MediatR;

namespace StreamCommunity.Application.ViewerGames;

public class ChangeEnlistmentSortOrderCommand : IRequest
{
    public ChangeEnlistmentSortOrderCommand(int enlistmentId, SortDirection sortDirection)
    {
        EnlistmentId = enlistmentId;
        SortDirection = sortDirection;
    }

    public int EnlistmentId { get; }

    public SortDirection SortDirection { get; }
}