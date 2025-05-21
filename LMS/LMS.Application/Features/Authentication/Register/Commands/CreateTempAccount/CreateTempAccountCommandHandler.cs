using AutoMapper;
using LMS.Application.Abstractions.Services.Authentication;
using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial.Levels;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

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
