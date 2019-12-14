namespace SmartRecruiting.Domain.Entities {
    public class User : BaseEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string AuthToken { get; set; }

        //TODO: Add other fields, integrate security roles and privileges management.
    }
}