using MediatR;
using Pulse.Users.Core.Cache;
using Pulse.Users.Core.Interfaces.Caching;
using System;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsVm>, ICacheableMediatorQuery
    {
        public Guid UserId { get; set; }

        public string CacheKey => CacheContracts.GetUserKey(UserId);
    }
}
