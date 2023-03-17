﻿// <auto-generated />
using System;
using ChatApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatApp.Core.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatApp.Core.Domain.Inbox", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("OwnerDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uuid");

                    b.Property<bool>("ReceiverDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ReceiverID")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedByID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("ReceiverID");

                    b.ToTable("Inboxes", (string)null);
                });

            modelBuilder.Entity("ChatApp.Core.Domain.Message", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("DeliveredStatus")
                        .HasColumnType("boolean");

                    b.Property<Guid>("InboxID")
                        .HasColumnType("uuid");

                    b.Property<bool>("ReceiverDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("SeenStatus")
                        .HasColumnType("boolean");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SenderID")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedByID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.HasIndex("InboxID");

                    b.HasIndex("SenderID");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("ChatApp.Core.Domain.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImageSrc")
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedByID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ChatApp.Core.Domain.Inbox", b =>
                {
                    b.HasOne("ChatApp.Core.Domain.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatApp.Core.Domain.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("ChatApp.Core.Domain.Message", b =>
                {
                    b.HasOne("ChatApp.Core.Domain.Inbox", "Inbox")
                        .WithMany()
                        .HasForeignKey("InboxID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatApp.Core.Domain.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inbox");

                    b.Navigation("Sender");
                });
#pragma warning restore 612, 618
        }
    }
}
