using System;
using System.Transactions;
using Microsoft.AspNetCore.Identity;

namespace MacsASPNETCore.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}