using Application.Interfaces;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries
{
    public class GetUserById : IRequest<User>
    {
        public int UserID { get; set; }
        internal class GetUserByIdIdHandler : IRequestHandler<GetUserById, User>
        {
            private readonly IRppDbContext _rppDbContext;

            public GetUserByIdIdHandler(IRppDbContext rppDbContext)
            {
                _rppDbContext = rppDbContext;
            }

            public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
            {
                var result = await _rppDbContext.User
                    .AsNoTracking() // Optional: improves performance
                    .FirstOrDefaultAsync(x => x.UserID == request.UserID, cancellationToken);

                if (result == null)
                {
                    throw new KeyNotFoundException($"User with ID {request.UserID} not found.");
                }

                return result;
            }

        }
    }
}
