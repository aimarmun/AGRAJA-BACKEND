using AgrajaBackend.Models;
using Microsoft.EntityFrameworkCore;
using configConst = AgrajaBackend.Constants.Config.Database;

namespace AgrajaBackend
{
    /// <summary>
    /// Contexto de base de datos de la aplicación
    /// </summary>
    public class AppDbContext : DbContext
    {
       // private readonly IConfigService _configService;

        private readonly IConfiguration _config;

        /// <summary>
        /// Set de datos de cajas
        /// </summary>
        public DbSet<Crate> Crates { get; set; }

        /// <summary>
        /// Set de datos de clientes
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Set de datos de agricultores
        /// </summary>
        public DbSet<Farmer> Farmers { get; set; }

        /// <summary>
        /// Set de dato de opciones de pago
        /// </summary>
        public DbSet<PayOption> PayOptions { get; set; }

        /// <summary>
        /// Set de datos de compras de cajas
        /// </summary>
        public DbSet<CrateSale> CratesSales { get; set; }   

        /// <summary>
        /// Set de datos de contrataciones de agricultores
        /// </summary>
        public DbSet<FarmerHiring> FarmerHirings { get; set; }

        /// <summary>
        /// Set de datos de tipos de cultivo
        /// </summary>
        public DbSet<CropType> CropTypes { get; set; }

        /// <summary>
        /// Set de datos de ciudades
        /// </summary>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// Set de usuarios que podrán manejar la aplicación
        /// </summary>
        public DbSet<User> Users { get; set; }

        ///// <summary>
        ///// Constructor de la clase
        ///// </summary>
        //public AppDbContext() { }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public AppDbContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Método que se ejecuta al iniciar configuración de BBDD
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString;
            var core = _config.GetValue(configConst.KEY_CORE, "");

            if (core.Equals(configConst.SQLSERVER_CORE))
            {
                connectionString = _config.GetValue(configConst.CONNECTIONSTRING_SQLSERVER, "");
                optionsBuilder.UseSqlServer(connectionString);
                return;
            }

            if (core.Equals(configConst.MYSQL_CORE))
            {
                connectionString = _config.GetValue(configConst.CONNECTIONSTRING_MYSQL, "");
                optionsBuilder.UseMySQL(connectionString);
                return;
            }
            
            throw new Exception("Motor de base de datos no válido");
        }

        /// <summary>
        /// Permite terminar de configurar las propiedades de las tablas por fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crate>()
                .Property(c => c.Price)
                .HasColumnType("decimal(6,4)");

            modelBuilder.Entity<Crate>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<CrateSale>()
                .Property(c => c.TotalPrice)
                .HasColumnType("decimal(6,4)");

            modelBuilder.Entity<Client>()
                .HasIndex(i => i.Dni)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(i => i.Email)
                .IsUnique();

            modelBuilder.Entity<Farmer>()
                .HasIndex(i => i.Dni)
                .IsUnique();

            modelBuilder.Entity<Farmer>()
                .HasIndex(i => i.Email)
                .IsUnique();

            modelBuilder.Entity<PayOption>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<CropType>()
                .HasIndex(i => i.Name) 
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(i => i.Name)
                .IsUnique();
        }
    }
}
