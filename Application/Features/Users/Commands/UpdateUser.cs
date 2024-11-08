

using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands
{
    public class UpdateUser : IRequest<int>
    {
        public int UserID { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }

        internal class UpdateUserHandler : IRequestHandler<UpdateUser, int>
        {
            private readonly IRppDbContext _context;

            public UpdateUserHandler(IRppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateUser request, CancellationToken cancellationToken)
            {
                var user = await _context.User
                    .AsNoTracking() // Optional: improves performance
                    .FirstOrDefaultAsync(x => x.UserID == request.UserID, cancellationToken);
                if (user != null)
                {
                    user.EmailAddress = request.EmailAddress;
                    user.Name = request.Name;
                    user.Password = request.Password;

                    _context.User.Update(user); // Explicitly mark as modified
                    await _context.SaveChangesAsync();
                    return user.UserID;
                }

                return default;
            }



        }
    }
}
