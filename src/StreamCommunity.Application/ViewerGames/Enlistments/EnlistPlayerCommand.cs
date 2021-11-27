using MediatR;

namespace StreamCommunity.Application.Enlistments
{
    public sealed class EnlistPlayerCommand : IRequest
    {
        public EnlistPlayerCommand(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}
