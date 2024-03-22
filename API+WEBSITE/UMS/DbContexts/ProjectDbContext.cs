using Microsoft.EntityFrameworkCore;

using UMS.Models;

namespace UMS.DbContexts
{
    public class ProjectDbContext : DbContext
    {

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UMS.Models.Test> Test { get; set; }
    }
}
