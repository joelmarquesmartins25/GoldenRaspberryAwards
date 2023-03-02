namespace GoldenRaspberryAwards.Data
{
    using GoldenRaspberryAwards.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApiDbContext : DbContext
    {
        #region Propriedades
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Producer> Producers { get; set; }
        #endregion

        #region Métodos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MoviesDb");
        }
        #endregion
    }
}