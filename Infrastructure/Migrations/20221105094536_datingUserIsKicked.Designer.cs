﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatingBotDbCotext))]
    [Migration("20221105094536_datingUserIsKicked")]
    partial class datingUserIsKicked
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.BlockedUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BlockedUserChatId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long>("UserChatId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("BlockedUsers");
                });

            modelBuilder.Entity("Domain.Entities.DatingUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("HasPhotos")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HaveSacred")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsBdsmLover")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsBeliever")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsChildfree")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsCosmopolitan")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsExistLover")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsHeelsUser")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsKicked")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsMakeupUser")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsTattooed")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsVegan")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OtherInfo")
                        .HasColumnType("text");

                    b.Property<bool>("PassedTheStiTest")
                        .HasColumnType("boolean");

                    b.Property<bool?>("ShaveHead")
                        .HasColumnType("boolean");

                    b.Property<bool?>("ShaveLegs")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("DatingUsers");
                });

            modelBuilder.Entity("Domain.Entities.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BodyPart")
                        .HasColumnType("integer");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAvatar")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PathToPhoto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Domain.Entities.Request", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<long>("DatingUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("DatingUserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Domain.Entities.TlgUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsKicked")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TlgUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ChatId = 444343256L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAdmin = true,
                            IsDeleted = false,
                            IsKicked = false,
                            Username = "noncredistka"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Request", b =>
                {
                    b.HasOne("Domain.Entities.DatingUser", "DatingUser")
                        .WithMany("Requests")
                        .HasForeignKey("DatingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatingUser");
                });

            modelBuilder.Entity("Domain.Entities.DatingUser", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
