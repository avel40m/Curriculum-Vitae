using System.Threading;
using Microsoft.EntityFrameworkCore;
using resumemanager.Models;
using resumemanager.obj;

namespace resumemanager.Data
{
    public class ResumenDbContext : DbContext
    {
        public ResumenDbContext(DbContextOptions<ResumenDbContext> options): base(options)
        {
            
        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
    }
}