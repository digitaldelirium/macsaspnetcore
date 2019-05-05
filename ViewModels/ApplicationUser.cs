﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using macsaspnetcore.Models;
using Microsoft.AspNetCore.Identity;

namespace macsaspnetcore.ViewModels
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {

    }
}
