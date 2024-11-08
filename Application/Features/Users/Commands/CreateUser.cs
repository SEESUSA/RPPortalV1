using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateUser : IRequest<int>
    {
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }


        internal class CreateUserHandler : IRequestHandler<CreateUser, int>
        {
            private readonly IRppDbContext _context;

            public CreateUserHandler(IRppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                var user = new User();
                user.EmailAddress = request.EmailAddress;
                user.Name = request.Name;
                user.Password = request.Password;
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.UserID;
            }


        }
    }
}
