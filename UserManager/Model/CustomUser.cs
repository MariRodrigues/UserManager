using Microsoft.AspNetCore.Identity;

namespace UserManager.Model
{
    public class CustomUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public CustomUser(string name, string email)
        {
            Name = name;
            Email = email;
            UserName = email;
            Status = true;
            CreatedOn = DateTime.Now;
        }

        public CustomUser()
        {
        }
    }
}
