using Domain.Common;
using Domain.Enum;

namespace Domain.Entities;

public class User:BaseEntity
{
    public string FirstName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture{get;set;}
        public string Password { get; set; }
        public Customer Customer{get;set;}
        public Gender Gender{get;set;}
        public Managers Managers{get;set;}
        public string Token { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

}
