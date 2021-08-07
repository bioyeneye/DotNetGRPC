using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcNotificationService.Protos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Google.Rpc.Context.AttributeContext.Types;

namespace GrpcNotificationService.Services
{
    public class AuthenticationService : Authenticate.AuthenticateBase
    {
        public override async Task<GrpcNotificationService.Protos.AuthorizeResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("a sample secret key for my jwt token");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);

            //context.ResponseTrailers.Add("Authorization", jwtToken);
            return await Task.FromResult(new GrpcNotificationService.Protos.AuthorizeResponse
            {
                Message = jwtToken
            });
        }
    }
}
