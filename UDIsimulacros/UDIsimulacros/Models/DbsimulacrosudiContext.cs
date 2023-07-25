using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UDIsimulacros.models;

public partial class DbsimulacrosudiContext : DbContext
{
    public DbsimulacrosudiContext()
    {
    }

    public DbsimulacrosudiContext(DbContextOptions<DbsimulacrosudiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Informepueba> Informepuebas { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=dbsimulacrosudi;uid=root;pwd=6053769Ma", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Idcarrera).HasName("PRIMARY");

            entity.ToTable("carrera");

            entity.HasIndex(e => e.IdFacultad, "idFacultad_idx");

            entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idFacultad");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.Idfacultad).HasName("PRIMARY");

            entity.ToTable("facultad");

            entity.Property(e => e.Idfacultad).HasColumnName("idfacultad");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Informepueba>(entity =>
        {
            entity.HasKey(e => e.IdInforme).HasName("PRIMARY");

            entity.ToTable("informepueba");

            entity.HasIndex(e => e.IdUsuario, "idUsuario_idx");

            entity.Property(e => e.IdInforme).HasColumnName("idInforme");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Categoria)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("categoria");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fechaHora");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Informepuebas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idUsuario");

            entity.HasMany(d => d.IdPregunta).WithMany(p => p.IdinfoPruebs)
                .UsingEntity<Dictionary<string, object>>(
                    "Infopruebpreg",
                    r => r.HasOne<Preguntum>().WithMany()
                        .HasForeignKey("IdPregunta")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("idPregunta"),
                    l => l.HasOne<Informepueba>().WithMany()
                        .HasForeignKey("IdinfoPrueb")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("idInfoPrueba"),
                    j =>
                    {
                        j.HasKey("IdinfoPrueb", "IdPregunta")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("infopruebpreg");
                        j.HasIndex(new[] { "IdPregunta" }, "idPregunta_idx");
                        j.IndexerProperty<int>("IdinfoPrueb")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("idinfoPrueb");
                        j.IndexerProperty<int>("IdPregunta").HasColumnName("idPregunta");
                    });
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PRIMARY");

            entity.ToTable("pregunta");

            entity.Property(e => e.IdPregunta).HasColumnName("idPregunta");
            entity.Property(e => e.Categoria)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("categoria");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(10000)
                .HasColumnName("descripcion");
            entity.Property(e => e.NivelDeDificultad).HasColumnName("nivelDeDificultad");
            entity.Property(e => e.RespuestaCorrecta)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("respuestaCorrecta");
            entity.Property(e => e.RespuestaDos)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("respuestaDos");
            entity.Property(e => e.RespuestaTres)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("respuestaTres");
            entity.Property(e => e.RespuestaUno)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("respuestaUno");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.IdCarrera, "idCarrera_idx");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Contraseña)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.NivelCiudadanas).HasColumnName("nivelCiudadanas");
            entity.Property(e => e.NivelCuantitativo).HasColumnName("nivelCuantitativo");
            entity.Property(e => e.NivelEscrita).HasColumnName("nivelEscrita");
            entity.Property(e => e.NivelIngles).HasColumnName("nivelIngles");
            entity.Property(e => e.NivelLectura).HasColumnName("nivelLectura");
            entity.Property(e => e.NombreCompleto)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.NumeroCelular)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("numeroCelular");
            entity.Property(e => e.NumeroDocumento)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.Rol)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("rol");
            entity.Property(e => e.Semestre).HasColumnName("semestre");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idCarrera");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
