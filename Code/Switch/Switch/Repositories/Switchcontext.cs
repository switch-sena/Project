using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;

namespace SwitchBack.Repositories
{
    public class SwitchContext : DbContext
    {
        public SwitchContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Barrios> Barrios { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<PublModa> PublModa { get; set; }
        public DbSet<Modalidades> Modalidades { get; set; }
        public DbSet<PublHabi> PublHabi { get; set; }
        public DbSet<Habilidades> Habilidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfuguration(modelBuilder);

        }



        private void EntityConfuguration(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla 'barrios'
            modelBuilder.Entity<Barrios>().ToTable("barrios").HasKey(b => b.IdBarr);
            modelBuilder.Entity<Barrios>().Property(b => b.IdBarr).HasColumnName("id_barr").ValueGeneratedOnAdd();
            modelBuilder.Entity<Barrios>().Property(b => b.NombreBarr).HasColumnName("nombre_barr");



            // Configuración de la tabla 'usuario'
            modelBuilder.Entity<Usuarios>().ToTable("usuarios").HasKey(u => u.IdUsua);
            modelBuilder.Entity<Usuarios>().Property(u => u.IdUsua).HasColumnName("id_usua").ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuarios>().Property(u => u.NombreUsua).HasColumnName("nombre_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.ApellidoUsua).HasColumnName("apellido_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.GeneroUsua).HasColumnName("genero_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.FechaNacimientoUsua).HasColumnName("fecha_nacimiento_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.CelularUsua).HasColumnName("celular_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.CorreoUsua).HasColumnName("correo_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.ClaveUsua).HasColumnName("clave_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.CorreoElectronicoUsua).HasColumnName("correo_electronico_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.LinksRsUsua).HasColumnName("links_rs_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.CopiaIdBarr).HasColumnName("copia_idbarr");
            modelBuilder.Entity<Usuarios>().Property(u => u.CreacionFechaUsua).HasColumnName("creacionfecha_usua");
            modelBuilder.Entity<Usuarios>().Property(u => u.ModificacionFechaUsua).HasColumnName("modificacionfecha_usua");

            // Configuración de la relación entre usuarios y Barrio
            modelBuilder.Entity<Usuarios>().HasOne(u => u.Barrio).WithMany(b => b.Usuarios).HasForeignKey(u => u.CopiaIdBarr);



            // Configuración de la tabla 'publicaciones'
            modelBuilder.Entity<Publicaciones>().ToTable("publicaciones").HasKey(b => b.IdPubl);
            modelBuilder.Entity<Publicaciones>().Property(b => b.IdPubl).HasColumnName("id_publ").ValueGeneratedOnAdd();
            modelBuilder.Entity<Publicaciones>().Property(b => b.CopiaIdUsua).HasColumnName("copia_idusua");
            modelBuilder.Entity<Publicaciones>().Property(b => b.TituloPubl).HasColumnName("titulo_publ");
            modelBuilder.Entity<Publicaciones>().Property(b => b.DescripcionPubl).HasColumnName("descripcion_publ");

            // Configuración de la relación entre publicaciones y usuarios
            modelBuilder.Entity<Publicaciones>().HasOne(u => u.Usuarios).WithMany(b => b.Publicaciones).HasForeignKey(u => u.CopiaIdUsua);


            // Configuración de la tabla 'modalidades'
            modelBuilder.Entity<Modalidades>().ToTable("modalidades").HasKey(b => b.IdModa);
            modelBuilder.Entity<Modalidades>().Property(b => b.IdModa).HasColumnName("id_moda").ValueGeneratedOnAdd();
            modelBuilder.Entity<Modalidades>().Property(b => b.NombreModa).HasColumnName("nombre_moda");
            modelBuilder.Entity<Modalidades>().Property(b => b.DescripcionModa).HasColumnName("descripcion_moda");



            // Configuración de la tabla 'publ_moda'
            modelBuilder.Entity<PublModa>().ToTable("publ_moda").HasKey(b => b.Id);
            modelBuilder.Entity<PublModa>().Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<PublModa>().Property(b => b.CopiaIdPubl).HasColumnName("copia_idpubl");
            modelBuilder.Entity<PublModa>().Property(b => b.CopiaIdModa).HasColumnName("copia_idmoda");

            // Configuración de la relación entre publ_moda y publicaciones, publ_moda y modalidades
            modelBuilder.Entity<PublModa>().HasOne(u => u.Publicaciones).WithMany(b => b.PublModa).HasForeignKey(u => u.CopiaIdPubl);
            modelBuilder.Entity<PublModa>().HasOne(u => u.Modalidades).WithMany(b => b.PublModa).HasForeignKey(u => u.CopiaIdModa);



            // Configuración de la tabla 'habilidades'
            modelBuilder.Entity<Habilidades>().ToTable("habilidades").HasKey(h => h.IdHabi);
            modelBuilder.Entity<Habilidades>().Property(h => h.IdHabi).HasColumnName("id_habi").ValueGeneratedOnAdd();
            modelBuilder.Entity<Habilidades>().Property(h => h.NombreHabi).HasColumnName("nombre_habi");
            modelBuilder.Entity<Habilidades>().Property(h => h.DescripcionHabi).HasColumnName("descripcion_habi");



            // Configuración de la tabla 'publ_habi'
            modelBuilder.Entity<PublHabi>().ToTable("publ_habi").HasKey(b => b.Id);
            modelBuilder.Entity<PublHabi>().Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<PublHabi>().Property(b => b.CopiaIdPubl).HasColumnName("copia_idpubl");
            modelBuilder.Entity<PublHabi>().Property(b => b.CopiaIdHabi).HasColumnName("copia_idhabi");

            // Configuración de la relación entre PublHabi y publicaciones, PublHabi y habilidades
            
            modelBuilder.Entity<PublHabi>().HasOne(u => u.Habilidades).WithMany(b => b.PublHabi).HasForeignKey(u => u.CopiaIdHabi);

        }
        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}

