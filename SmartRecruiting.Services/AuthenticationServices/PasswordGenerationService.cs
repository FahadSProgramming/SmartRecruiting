using System.Security.Cryptography;
using System.Text;
using SmartRecruiting.Application.Interfaces;

namespace SmartRecruiting.Services.AuthenticationServices {
    public class PasswordGenerationService : IPasswordGenerationService {
        public bool ComparePassword(string password, byte[] passwordHash, byte[] passwordSalt) {
            using(var hmac = new HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using(var hmac = new HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
            }
        }
    }
}