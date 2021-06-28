using Microsoft.AspNetCore.Identity;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.JWT
{
    public interface IJwtService
    {
        Task<string> GenerateToken(User user, UserManager<User> userManager);
    }
}
