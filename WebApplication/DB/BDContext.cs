using System;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using Entity;

namespace DB
{
    public class BDContext:DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Voyage> Voyages { get; set; }
        public DbSet<User> Users { get; set; }
        
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Voyage>()
                .HasRequired(c => c.Depart)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Voyage>()
                .HasRequired(c => c.Arrival)
                .WithMany()
                .WillCascadeOnDelete(false);
            
            /*modelBuilder.Entity<Entity.Voyage>()
                .HasRequired(v => v.Depart)
                .WithMany(vi => vi.Departs)
                .HasForeignKey(v => v.DepartId);
            modelBuilder.Entity<Entity.Voyage>()
                .HasRequired(v => v.Arrival)
                .WithMany(vi => vi.Arrivals)
                .HasForeignKey(v => v.ArrivalId);*/
            /* modelBuilder.Entity<Entity.Voyage>()
                 .HasRequired(v => v.Depart);
             modelBuilder.Entity<Entity.Voyage>()
                 .HasRequired(v => v.Arrival);
             modelBuilder.Entity<Entity.Voyage>()
                 .HasRequired(v => v.Vehicule);*/

        }  
    }
}