namespace SmartRecruiting.Application.Interfaces {
    public interface IPasswordGenerationService {
        void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool ComparePassword(string password, byte[] passwordhash, byte[] passwordSalt);
    }
}