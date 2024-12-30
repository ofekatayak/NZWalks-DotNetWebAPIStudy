using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties
            //Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("54f977ba-098c-4188-aa43-c1ad2126d0b7"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("0ff1715e-907f-416c-bf40-d73576e12601"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("6419a4a1-3252-4e16-a84e-6c8b1647985b"),
                    Name = "Hard"
                }
            };

            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for regions
            var regions = new List<Region>()
            {
                new Region() {
                    Id = Guid.Parse("07ddab4e-a3b3-4743-92e7-c3f750d17b26"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = null
                },
                new Region() {
                    Id = Guid.Parse("c6d37314-715d-45c2-9dd7-6c1cc5d3bc3e"),
                    Name = "Miami",
                    Code = "MIA",
                    RegionImageUrl = null
                },
                new Region() {
                    Id = Guid.Parse("a98d01aa-fd0f-4a0b-8823-f78516394cc9"),
                    Name = "Portland",
                    Code = "PTL",
                    RegionImageUrl = null
                },
                new Region() {
                    Id = Guid.Parse("1f6f4b7b-086b-4f16-811d-e45c6ed00be1"),
                    Name = "New York City",
                    Code = "NYC",
                    RegionImageUrl = null
                },
                new Region() {
                    Id = Guid.Parse("1c0940ab-79b3-4599-a77f-ca35ee17b7e2"),
                    Name = "Aucland",
                    Code = "AKL",
                    RegionImageUrl = null
                }
            };

            //Seed regions to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
