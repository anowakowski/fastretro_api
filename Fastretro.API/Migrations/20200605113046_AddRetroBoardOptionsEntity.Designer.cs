﻿// <auto-generated />
using System;
using Fastretro.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fastretro.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200605113046_AddRetroBoardOptionsEntity")]
    partial class AddRetroBoardOptionsEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Fastretro.API.Data.Domain.CurrentUserVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RetroBoardCardId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetroBoardId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CurrentUserVotes");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.FirebaseUserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChosenAvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrentUserInRetroBoardId")
                        .HasColumnType("int");

                    b.Property<string>("DateOfExistingCheck")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirebaseUserDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentUserInRetroBoardId");

                    b.ToTable("FirebaseUsersData");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoardOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxVouteCount")
                        .HasColumnType("int");

                    b.Property<string>("RetroBoardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShouldBlurRetroBoardCardText")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("RetroBoardOptions");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.FirebaseUserData", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.CurrentUserInRetroBoard", "CurrentUserInRetroBoard")
                        .WithMany("firebaseUsersData")
                        .HasForeignKey("CurrentUserInRetroBoardId");
                });
#pragma warning restore 612, 618
        }
    }
}
