﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MacsASPNETCore.Models;
using Microsoft.AspNetCore.Identity;

namespace MacsASPNETCore.ViewModels
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        
    }
}