using Application.Interfaces;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries
{
    public class GetAllUsers : IRequest<IEnumerable<User>>
    {
        internal class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<User>>
        {
            private readonly IRppDbContext _rppDbContext;

            public GetAllUsersHandler(IRppDbContext rppDbContext)
            {
                _rppDbContext = rppDbContext;
            }

            public async Task<IEnumerable<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
            {
                var result = await _rppDbContext.User
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
