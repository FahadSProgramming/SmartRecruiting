using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartRecruiting.Application.Common;
using SmartRecruiting.Application.Interfaces;

namespace SmartRecruiting.Services.AuthenticationServices {
    public class JwtTokenGenerationService : IJwtTokenGenerationService {
        private readonly TokenParameters _parameters;
        public JwtTokenGenerationService(IOptions<TokenParameters> parameters) {
            _parameters = parameters.Value;
        }
        public AuthenticationResponse GenerateToken(TokenGenerationDto tokenGenerationDto) {

            try {
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Email, tokenGenerationDto.NameIdentifier),
                    new Claim(ClaimTypes.NameIdentifier, tokenGenerationDto.UserId.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(_parameters.ExpiryInDays),
                    SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_parameters.SecurityKey)),
                    SecurityAlgorithms.HmacSha512Signature
                    )
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var response = new AuthenticationResponse() {
                    Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor))
                };

                return response;
            } catch (Exception) {
                //log error
                throw;
            }

        }
    }
}