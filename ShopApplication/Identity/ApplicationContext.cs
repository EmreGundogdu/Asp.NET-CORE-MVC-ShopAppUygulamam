using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Identity
{
    public class ApplicationContext:IdentityDbContext<User>
    {
    }
}
