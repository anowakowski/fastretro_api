﻿// <auto-generated />
using System;
using Fastretro.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fastretro.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fastretro.API.Data.Domain.CurrentUserInRetroBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RetroBoardId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CurrentUserInRetroBoards");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.FirebaseUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrentUserInRetroBoardId")
                        .HasColumnType("int");

                    b.Property<string>("FirebaseUserDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentUserInRetroBoardId");

                    b.ToTable("FirebaseUsers");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.FirebaseUser", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.CurrentUserInRetroBoard", "CurrentUserInRetroBoard")
                        .WithMany("firebaseUsers")
                        .HasForeignKey("CurrentUserInRetroBoardId");
                });
#pragma warning restore 612, 618
        }
    }
}
