using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands
{
    public class DeleteUser : IRequest<int>
    {
        public int UserID { get; set; }
        internal class DeleteUserHandler : IRequestHandler<DeleteUser, int>
        {
            private readonly IRppDbContext _context;
            public DeleteUserHandler(IRppDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteUser request, CancellationToken cancellationToken)
            {
                var user = await _context.User.Where(x => x.UserID == request.UserID).FirstOrDefaultAsync();
                if (user == null)
                {
                    return default;
                }
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return request.UserID;
            }
        }
    }
}
