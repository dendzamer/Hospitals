using Hospitals.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospitals.Data
{
    public class HospitalDataContext : DbContext
    {
        public HospitalDataContext(DbContextOptions<HospitalDataContext> options) : base(options)
        {
        }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}