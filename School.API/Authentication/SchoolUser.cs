using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace School.API.Authentication
{
    public class SchoolUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SchoolUser(int userId, string userName, string firstName, string lastName)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
