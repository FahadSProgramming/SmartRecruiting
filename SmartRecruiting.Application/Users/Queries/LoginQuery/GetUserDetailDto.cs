using System;

namespace SmartRecruiting.Application.Users {
    public class GetUserDetailDto {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}