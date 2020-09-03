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

                    b.Property<string>("ChosenAvatarName")
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

            modelBuilder.Entity("Fastretro.API.Data.Domain.MergedRetroBoardCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RetroBoardCardId")
                        .HasColumnType("int");

                    b.Property<int?>("RetroBoardCardMergedGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RetroBoardCardId");

                    b.HasIndex("RetroBoardCardMergedGroupId");

                    b.ToTable("MergedRetroBoardCards");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RetroBoardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetroBoardName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SprintNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RetroBoards");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoardAdditionalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RetroBoardActionCount")
                        .HasColumnType("int");

                    b.Property<string>("RetroBoardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RetroBoardIndexCount")
                        .HasColumnType("int");

                    b.Property<string>("TeamFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RetroBoardAdditionalInfos");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoardCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsHidenMergedChild")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMerged")
                        .HasColumnType("bit");

                    b.Property<bool>("IsShowMergedParent")
                        .HasColumnType("bit");

                    b.Property<string>("RetroBoardCardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RetroBoardCardMergedGroupId")
                        .HasColumnType("int");

                    b.Property<string>("RetroBoardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RetroBoardCardMergedGroupId");

                    b.ToTable("RetroBoardCards");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoardCardMergedGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("RetroBoardCardMergedGroups");
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

                    b.Property<bool>("ShouldHideVoutCountInRetroBoardCard")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("RetroBoardOptions");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoardStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifyDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetroBoardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RetroBoardStatuses");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UserNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatonDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("NotyficationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNotificationFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("userNotifications");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UserNotificationWorkspaceWithRequiredAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorUserFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserNotificationId")
                        .HasColumnType("int");

                    b.Property<int>("UserWaitingToApproveWorkspaceJoinId")
                        .HasColumnType("int");

                    b.Property<string>("UserWantToJoinFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspceWithRequiredAccessFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserNotificationId");

                    b.HasIndex("UserWaitingToApproveWorkspaceJoinId");

                    b.ToTable("UserNotificationWorkspaceWithRequiredAccesses");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UserNotificationWorkspaceWithRequiredAccessResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserJoinedToWorkspaceFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserNotificationId")
                        .HasColumnType("int");

                    b.Property<int>("UserWaitingToApproveWorkspaceJoinId")
                        .HasColumnType("int");

                    b.Property<string>("WorkspaceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspceWithRequiredAccessFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserNotificationId");

                    b.HasIndex("UserWaitingToApproveWorkspaceJoinId");

                    b.ToTable("UserNotificationWorkspaceWithRequiredAccessResponses");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UserWaitingToApproveWorkspaceJoin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorUserFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApprovalByCreator")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifyDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RequestIsApprove")
                        .HasColumnType("bit");

                    b.Property<string>("UserWantToJoinFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspceWithRequiredAccessFirebaseId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("userWaitingToApproveWorkspaceJoins");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UsersInAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RetroBoardActionCardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetroBoardCardFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UsersInActions");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UsersInTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChosenAvatarName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceFirebaseDocId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UsersInTeams");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.FirebaseUserData", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.CurrentUserInRetroBoard", "CurrentUserInRetroBoard")
                        .WithMany("firebaseUsersData")
                        .HasForeignKey("CurrentUserInRetroBoardId");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.MergedRetroBoardCard", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.RetroBoardCard", "RetroBoardCard")
                        .WithMany("MergetRetroBoardCards")
                        .HasForeignKey("RetroBoardCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fastretro.API.Data.Domain.RetroBoardCardMergedGroup", "RetroBoardCardMergedGroup")
                        .WithMany()
                        .HasForeignKey("RetroBoardCardMergedGroupId");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.RetroBoardCard", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.RetroBoardCardMergedGroup", "RetroBoardCardMergedGroup")
                        .WithMany()
                        .HasForeignKey("RetroBoardCardMergedGroupId");
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UserNotificationWorkspaceWithRequiredAccess", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.UserNotification", "UserNotification")
                        .WithMany()
                        .HasForeignKey("UserNotificationId");

                    b.HasOne("Fastretro.API.Data.Domain.UserWaitingToApproveWorkspaceJoin", "UserWaitingToApproveWorkspaceJoin")
                        .WithMany()
                        .HasForeignKey("UserWaitingToApproveWorkspaceJoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fastretro.API.Data.Domain.UserNotificationWorkspaceWithRequiredAccessResponse", b =>
                {
                    b.HasOne("Fastretro.API.Data.Domain.UserNotification", "UserNotification")
                        .WithMany()
                        .HasForeignKey("UserNotificationId");

                    b.HasOne("Fastretro.API.Data.Domain.UserWaitingToApproveWorkspaceJoin", "UserWaitingToApproveWorkspaceJoin")
                        .WithMany()
                        .HasForeignKey("UserWaitingToApproveWorkspaceJoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
