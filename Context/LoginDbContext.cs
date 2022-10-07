using LoginTokenTask.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginTokenTask.Context
{
    public class LoginDbContext:DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> Context) : base(Context)
        {

        }

        //create table
        public DbSet<LoginUser> UserTbl { get; set; }

        public DbSet<Gadget> GagetTbl { get; set; }
    }
}
