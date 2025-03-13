using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Storage
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<Component> Components { get; set; }

        public DbSet<ComponentComponent> ComponentComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductComponent>()
                .HasKey(pc => new { pc.ProductId, pc.ComponentId });

            modelBuilder
                .Entity<ProductComponent>()
                .HasOne(pc => pc.product)
                .WithMany(p => p.ProductComponents)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder
                .Entity<ProductComponent>()
                .HasOne(pc => pc.component)
                .WithMany(c => c.ProductComponents)
                .HasForeignKey(pc => pc.ComponentId);

            modelBuilder
                .Entity<ComponentComponent>()
                .HasKey(cc => new { cc.ParentComponentId, cc.ChildComponentId });

            modelBuilder
                .Entity<ComponentComponent>()
                .HasOne(cc => cc.ParentComponent)
                .WithMany(c => c.ParentComponents)
                .HasForeignKey(cc => cc.ParentComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<ComponentComponent>()
                .HasOne(cc => cc.ChildComponent)
                .WithMany(c => c.ChildComponents)
                .HasForeignKey(cc => cc.ChildComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Component>()
                .HasData(
                    new List<Component>()
                    {
                        new Component { Id = 1, Name = "Wheels" },
                        new Component { Id = 2, Name = "Rod" },
                        new Component { Id = 3, Name = "Bolts" },
                        new Component { Id = 4, Name = "Wheel Set" },
                        new Component { Id = 5, Name = "Frame" },
                        new Component { Id = 6, Name = "Seat" },
                        new Component { Id = 7, Name = "Pedals" },
                        new Component { Id = 8, Name = "Bicycle Body" },
                        new Component { Id = 9, Name = "Handlebars" },
                        new Component { Id = 10, Name = "Scooter Deck" },
                    }
                );

            modelBuilder
                .Entity<ComponentComponent>()
                .HasData(
                    new List<ComponentComponent>()
                    {
                        new ComponentComponent { ParentComponentId = 4, ChildComponentId = 1 },
                        new ComponentComponent { ParentComponentId = 4, ChildComponentId = 2 },
                        new ComponentComponent { ParentComponentId = 4, ChildComponentId = 3 },
                    }
                );

            modelBuilder
                .Entity<Product>()
                .HasData(
                    new List<Product>()
                    {
                        new Product { Id = 1, Name = "Bicycle" },
                        new Product { Id = 2, Name = "Scooter" },
                    }
                );
        }
    }
}
