﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentRecordsAPI.Data;

#nullable disable

namespace StudentRecordsAPI.Migrations
{
    [DbContext(typeof(StudentRecordsContext))]
    [Migration("20250319232212_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentRecordsAPI.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"));

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<int?>("FacultyID")
                        .HasColumnType("int");

                    b.Property<int?>("TermID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("FacultyID");

                    b.HasIndex("TermID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultyID"));

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultyID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Major", b =>
                {
                    b.Property<int>("MajorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorID"));

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajorID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Student", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MajorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentID");

                    b.HasIndex("MajorID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.StudentCourse", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.HasKey("StudentID", "CourseID");

                    b.HasIndex("CourseID");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.StudentMajor", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MajorID")
                        .HasColumnType("int");

                    b.HasKey("StudentID", "MajorID");

                    b.HasIndex("MajorID");

                    b.ToTable("StudentMajors");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Term", b =>
                {
                    b.Property<int>("TermID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TermID"));

                    b.Property<string>("TermCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TermID");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Course", b =>
                {
                    b.HasOne("StudentRecordsAPI.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentID");

                    b.HasOne("StudentRecordsAPI.Models.Faculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyID");

                    b.HasOne("StudentRecordsAPI.Models.Term", "Term")
                        .WithMany("Courses")
                        .HasForeignKey("TermID");

                    b.Navigation("Department");

                    b.Navigation("Faculty");

                    b.Navigation("Term");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Faculty", b =>
                {
                    b.HasOne("StudentRecordsAPI.Models.Department", "Department")
                        .WithMany("FacultyMembers")
                        .HasForeignKey("DepartmentID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Major", b =>
                {
                    b.HasOne("StudentRecordsAPI.Models.Department", "Department")
                        .WithMany("Majors")
                        .HasForeignKey("DepartmentID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Student", b =>
                {
                    b.HasOne("StudentRecordsAPI.Models.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorID");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.StudentCourse", b =>
                {
                    b.HasOne("StudentRecordsAPI.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentRecordsAPI.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.StudentMajor", b =>
                {
                    b.HasOne("StudentRecordsAPI.Models.Major", "Major")
                        .WithMany("StudentMajors")
                        .HasForeignKey("MajorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentRecordsAPI.Models.Student", "Student")
                        .WithMany("StudentMajors")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("FacultyMembers");

                    b.Navigation("Majors");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Faculty", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Major", b =>
                {
                    b.Navigation("StudentMajors");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");

                    b.Navigation("StudentMajors");
                });

            modelBuilder.Entity("StudentRecordsAPI.Models.Term", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
