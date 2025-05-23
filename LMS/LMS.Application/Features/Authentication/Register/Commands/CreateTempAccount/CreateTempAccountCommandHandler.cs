using LMS.Application.Abstractions.Services.Authentication;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount
{
    public class CreateTempAccountCommandHandler : IRequestHandler<CreateTempAccountCommand, Result>
    {
        private readonly IAuthenticationHelper _authenticationHelper;



        public CreateTempAccountCommandHandler(
            IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }


        public async Task<Result> Handle(CreateTempAccountCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationHelper.CreateAndSaveAccountAsync(request);
        }
    }
}
