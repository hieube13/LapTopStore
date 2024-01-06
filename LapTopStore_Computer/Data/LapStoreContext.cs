using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class LapStoreContext : IdentityDbContext<IdentityUser>
    {
        public LapStoreContext(DbContextOptions<LapStoreContext> options) : base(options)
        { 
        }
    }
}
