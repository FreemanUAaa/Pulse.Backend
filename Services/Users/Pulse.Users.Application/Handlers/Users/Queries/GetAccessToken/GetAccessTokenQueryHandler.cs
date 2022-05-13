﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Helpers.Passwords;
using Pulse.Users.Core.Models;
using Pulse.Users.Core.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetAccessToken
{
    public class GetAccessTokenQueryHandler : IRequestHandler<GetAccessTokenQuery, string>
    {
        private readonly ILogger<GetAccessTokenQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly AuthOptions auth;

        public GetAccessTokenQueryHandler(IDatabaseContext database, IOptions<AuthOptions> auth, ILogger<GetAccessTokenQueryHandler> logger) =>
           (this.database, this.auth, this.logger) = (database, auth.Value, logger);

        public async Task<string> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
            ClaimsIdentity identity = GetIdentity(user);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.FailedToLogin);
            }

            string hash = Hasher.HashPassword(request.Password, user.PasswordSalt);

            if (user.PasswordHash != hash)
            {
                throw new Exception(ExceptionStrings.FailedToLogin);
            }

            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new(
                    issuer: auth.Issuer,
                    audience: auth.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(auth.Lifetime)),
                    signingCredentials: new SigningCredentials(auth.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            logger.LogInformation("The user has successfully entered");

            return token;
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
            {
                return null;
            }

            List<Claim> claims = new() { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()), };
            ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            return claimsIdentity;
        }
    }
}
