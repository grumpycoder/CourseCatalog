using System;
using System.Collections.Generic;

namespace CourseCatalog.Domain.Entities
{
    public class User
    {
        public User(string username, string emailAddress, string firstName, string lastName, string fullName, Guid identityGuid)
        {
            Username = username;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            IdentityGuid = identityGuid;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Guid IdentityGuid { get; set; }

        public List<UserGroup> Groups { get; set; }

        public void Update(string username, string emailAddress, string firstName, string lastName, string fullName, Guid identityGuid)
        {
            IdentityGuid = identityGuid;
            Username = username;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
        }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserGroup> Users { get; set; }
    }

    public class UserGroup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }

    }
}
