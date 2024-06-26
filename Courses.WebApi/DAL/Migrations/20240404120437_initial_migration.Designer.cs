﻿// <auto-generated />
using System;
using Courses.WebApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Courses.WebApi.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240404120437_initial_migration")]
    partial class initial_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Courses.WebApi.Entities.ContactMessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Service")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ContactMessages");
                });

            modelBuilder.Entity("Courses.WebApi.Entities.CourseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleCount")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("CertificateOfCompletion")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("DiscountedPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("DownloadableResourcesCount")
                        .HasColumnType("integer");

                    b.Property<decimal>("HoursOfContent")
                        .HasColumnType("numeric");

                    b.Property<bool>("LifetimeAccess")
                        .HasColumnType("boolean");

                    b.Property<decimal>("LikesCount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ReviewCount")
                        .HasColumnType("numeric");

                    b.Property<int>("StarsCount")
                        .HasColumnType("integer");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Topics")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Courses.WebApi.Entities.CourseProgramDetailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Step")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseProgramDetails");
                });

            modelBuilder.Entity("Courses.WebApi.Entities.SubscribeEntity", b =>
                {
                    b.Property<string>("EmailAddress")
                        .HasColumnType("text");

                    b.Property<bool>("AdvertisingUpdates")
                        .HasColumnType("boolean");

                    b.Property<bool>("DailyNewsletter")
                        .HasColumnType("boolean");

                    b.Property<bool>("EventUpdates")
                        .HasColumnType("boolean");

                    b.Property<bool>("Podcasts")
                        .HasColumnType("boolean");

                    b.Property<bool>("StartupsWeekly")
                        .HasColumnType("boolean");

                    b.Property<bool>("WeekInReview")
                        .HasColumnType("boolean");

                    b.HasKey("EmailAddress");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("Courses.WebApi.Entities.CourseProgramDetailEntity", b =>
                {
                    b.HasOne("Courses.WebApi.Entities.CourseEntity", "Course")
                        .WithMany("ProgramDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Courses.WebApi.Entities.CourseEntity", b =>
                {
                    b.Navigation("ProgramDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
