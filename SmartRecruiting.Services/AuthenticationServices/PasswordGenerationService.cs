using SmartRecruiting.Application.Interfaces;

namespace SmartRecruiting.Services.AuthenticationServices {
    public class PasswordGenerationService : IPasswordGenerationService {
        public bool ComparePassword(string password, byte[] passwordhash, byte[] passwordSalt) {
            throw new System.NotImplementedException();
        }

        public void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            throw new System.NotImplementedException();
        }
    }
}