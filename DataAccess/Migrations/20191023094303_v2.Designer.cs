﻿// <auto-generated />
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20191023094303_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IDataInterface.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CostPrice")
                        .HasColumnType("int");

                    b.Property<long>("ISBNNumber")
                        .HasColumnType("bigint");

                    b.Property<bool>("InLibrary")
                        .HasColumnType("bit");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<int>("ShelfID")
                        .HasColumnType("int");

                    b.HasKey("BookID");

                    b.HasIndex("ShelfID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("IDataInterface.Section", b =>
                {
                    b.Property<int>("SectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SectionNumber")
                        .HasColumnType("int");

                    b.HasKey("SectionID");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("IDataInterface.Shelf", b =>
                {
                    b.Property<int>("ShelfID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SectionID")
                        .HasColumnType("int");

                    b.Property<int>("ShelfNumber")
                        .HasColumnType("int");

                    b.HasKey("ShelfID");

                    b.HasIndex("SectionID");

                    b.ToTable("Shelfs");
                });

            modelBuilder.Entity("IDataInterface.Book", b =>
                {
                    b.HasOne("IDataInterface.Shelf", "Shelf")
                        .WithMany("Books")
                        .HasForeignKey("ShelfID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IDataInterface.Shelf", b =>
                {
                    b.HasOne("IDataInterface.Section", "Section")
                        .WithMany("Shelfs")
                        .HasForeignKey("SectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
