﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia;

#nullable disable

namespace Persistencia.Migrations
{
    [DbContext(typeof(CursosOnlineContext))]
    [Migration("20221105134634_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dominio.Comentario", b =>
                {
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComentarioId"), 1L, 1);

                    b.Property<string>("Alumno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComentarioTexto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("Puntaje")
                        .HasColumnType("int");

                    b.HasKey("ComentarioId");

                    b.HasIndex("CursoId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("Dominio.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CursoId"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CursoId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("Dominio.CursoInstructor", b =>
                {
                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.HasKey("CursoId", "InstructorId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CursoInstructor");
                });

            modelBuilder.Entity("Dominio.Directorio", b =>
                {
                    b.Property<string>("DirectorioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DirectorioId");

                    b.ToTable("Directorios");
                });

            modelBuilder.Entity("Dominio.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructorId"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FotoPerfil")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Grado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructorId");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("Dominio.Multipart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CurrentDirectorioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Segmento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentDirectorioId");

                    b.ToTable("Multiparts");
                });

            modelBuilder.Entity("Dominio.Precio", b =>
                {
                    b.Property<int>("PrecioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrecioId"), 1L, 1);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioActual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Promocion")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PrecioId");

                    b.HasIndex("CursoId")
                        .IsUnique();

                    b.ToTable("Precio");
                });

            modelBuilder.Entity("Dominio.Comentario", b =>
                {
                    b.HasOne("Dominio.Curso", "Curso")
                        .WithMany("ComentarioLista")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Dominio.CursoInstructor", b =>
                {
                    b.HasOne("Dominio.Curso", "Curso")
                        .WithMany("InstructorLink")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Instructor", "Instructor")
                        .WithMany("CursoLink")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Dominio.Multipart", b =>
                {
                    b.HasOne("Dominio.Directorio", "Directorio")
                        .WithMany("Multiparts")
                        .HasForeignKey("CurrentDirectorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Directorio");
                });

            modelBuilder.Entity("Dominio.Precio", b =>
                {
                    b.HasOne("Dominio.Curso", "Curso")
                        .WithOne("PrecioPromocion")
                        .HasForeignKey("Dominio.Precio", "CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Dominio.Curso", b =>
                {
                    b.Navigation("ComentarioLista");

                    b.Navigation("InstructorLink");

                    b.Navigation("PrecioPromocion")
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Directorio", b =>
                {
                    b.Navigation("Multiparts");
                });

            modelBuilder.Entity("Dominio.Instructor", b =>
                {
                    b.Navigation("CursoLink");
                });
#pragma warning restore 612, 618
        }
    }
}
