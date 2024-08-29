using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Switch.Models;

namespace Switch.Context
{
    public class SwitchContext : DbContext
    {
        public SwitchContext(DbContextOptions<SwitchContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Barrios> Barrios { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla 'usuario'
            modelBuilder.Entity<Usuario>().ToTable("usuario").HasKey(u => u.IdUsua);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsua).HasColumnName("id_usua").ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuario>().Property(u => u.NombreUsua).HasColumnName("nombre_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.ApellidoUsua).HasColumnName("apellido_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.GeneroUsua).HasColumnName("genero_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.FechaNacimientoUsua).HasColumnName("fecha_nacimiento_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.CelularUsua).HasColumnName("celular_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.CorreoUsua).HasColumnName("correo_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.ClaveUsua).HasColumnName("clave_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.CorreoElectronicoUsua).HasColumnName("correo_electronico_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.LinksRsUsua).HasColumnName("links_rs_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.CopiaIdBarr).HasColumnName("copia_idbarr");
            modelBuilder.Entity<Usuario>().Property(u => u.CreacionFechaUsua).HasColumnName("creacionfecha_usua");
            modelBuilder.Entity<Usuario>().Property(u => u.ModificacionFechaUsua).HasColumnName("modificacionfecha_usua");

            // Configuración de la tabla 'barrios'
            modelBuilder.Entity<Barrios>().ToTable("barrios").HasKey(b => b.IdBarr);
            modelBuilder.Entity<Barrios>().Property(b => b.IdBarr).HasColumnName("id_barr").ValueGeneratedOnAdd();
            modelBuilder.Entity<Barrios>().Property(b => b.NombreBarr).HasColumnName("nombre_barr");

            // Configuración de la relación entre Usuario y Barrio
            modelBuilder.Entity<Usuario>().HasOne(u => u.Barrio).WithMany(b => b.Usuarios).HasForeignKey(u => u.CopiaIdBarr);

            // Configuración de la tabla 'habilidades'
            modelBuilder.Entity<Habilidad>().ToTable("habilidades").HasKey(h => h.Id_Habi);
            modelBuilder.Entity<Habilidad>().Property(h => h.Id_Habi).HasColumnName("id_habi").ValueGeneratedOnAdd();
            modelBuilder.Entity<Habilidad>().Property(h => h.Nombre_Habi).HasColumnName("nombre_habi");
            modelBuilder.Entity<Habilidad>().Property(h => h.Descripcion_Habi).HasColumnName("descripcion_habi");
        }
    }
}

