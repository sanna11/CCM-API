using CCM.Core.Handlers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCM.Data.Models.DBContext
{
    public class CCBDBContext : IdentityDbContext<AuthUser>
    {
        public CCBDBContext(DbContextOptions<CCBDBContext> options)
            : base(options)
        {
        }

        public CCBDBContext()
           : base()
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                var now = DateTime.UtcNow;
                dynamic entity = entry.Entity;

                if (ObjectHandler.IsPropertyExist(entity, "UpdatedOn"))
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (ObjectHandler.IsPropertyExist(entity, "CreatedOn"))
                        {
                            entity.CreatedOn = now;
                        }
                        if (ObjectHandler.IsPropertyExist(entity, "InternalId"))
                        {
                            entity.InternalId = Guid.NewGuid();
                        }
                        entity.UpdatedOn = now;
                    }
                    else
                    {
                        entity.UpdatedOn = now;
                    }
                }
            }
            this.ChangeTracker.DetectChanges();

            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<MovieCast> MovieCasts { get; set; }
        public virtual DbSet<Theatre> Theatres { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }
        public virtual DbSet<TheatreHall> TheatreHalls { get; set; }
        public virtual DbSet<TheatreSession> TheatreSessions { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}
