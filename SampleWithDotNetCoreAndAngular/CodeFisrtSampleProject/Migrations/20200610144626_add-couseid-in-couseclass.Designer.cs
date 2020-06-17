﻿// <auto-generated />
using CodeFisrtSampleProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeFisrtSampleProject.Migrations
{
    [DbContext(typeof(OnlineEducationDBContext))]
    [Migration("20200610144626_add-couseid-in-couseclass")]
    partial class addcouseidincouseclass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("CourseCategoryId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.CourseCategory", b =>
                {
                    b.Property<int>("CourseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("CourseCategoryId");

                    b.ToTable("CourseCategories");
                });

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.CourseClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseClasses");
                });

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.CourseClassDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseClassId")
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("FinishTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseClassId");

                    b.ToTable("CourseClassDays");
                });

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.Course", b =>
                {
                    b.HasOne("CodeFisrtSampleProject.Entities.CourseCategory", "CourseCategory")
                        .WithMany("Courses")
                        .HasForeignKey("CourseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.CourseClass", b =>
                {
                    b.HasOne("CodeFisrtSampleProject.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeFisrtSampleProject.Entities.CourseClassDay", b =>
                {
                    b.HasOne("CodeFisrtSampleProject.Entities.CourseClass", "CourseClass")
                        .WithMany("CourseClassDay")
                        .HasForeignKey("CourseClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}