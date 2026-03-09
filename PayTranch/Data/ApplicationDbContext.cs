using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayTranch.Models;

namespace PayTranch.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<AdminSupDeCo> AdminSupDeCos { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}