using Switch.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Switch.Context
{
    public class SwitchContext : DbContext
    {
        public SwitchContext() : base("name=SwitchContext")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Barrio> Barrios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla 'usuario'
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsua);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsua).HasColumnName("id_usua").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
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
            modelBuilder.Entity<Barrio>().ToTable("barrios");
            modelBuilder.Entity<Barrio>().HasKey(u => u.IdBarr);
            modelBuilder.Entity<Barrio>().Property(u => u.IdBarr).HasColumnName("id_barr").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Barrio>().Property(u => u.NombreBarr).HasColumnName("nombre_barr");

            //Configuración de la relacion entre Usuario y Barrio
            modelBuilder.Entity<Usuario>()
               .HasRequired(u => u.Barrio)
               .WithMany(b => b.Usuarios)
               .HasForeignKey(u => u.CopiaIdBarr);
        }
    }
}