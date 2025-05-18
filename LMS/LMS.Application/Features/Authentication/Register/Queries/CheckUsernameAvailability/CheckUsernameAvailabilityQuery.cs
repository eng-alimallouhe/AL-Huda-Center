using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Register.Queries.CheckUsernameAvailability
{
    public record CheckUsernameAvailabilityQuery(string UserName) : IRequest<Result>;
}
