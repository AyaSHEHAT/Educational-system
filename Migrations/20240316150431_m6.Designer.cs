﻿// <auto-generated />
using System;
using Demo1_Day2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo1_Day2.Migrations
{
    [DbContext(typeof(ITIContext))]
    [Migration("20240316150431_m6")]
    partial class m6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.Property<int>("CoursesCrsId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentsDeptId")
                        .HasColumnType("int");

                    b.HasKey("CoursesCrsId", "DepartmentsDeptId");

                    b.HasIndex("DepartmentsDeptId");

                    b.ToTable("CourseDepartment");
                });

            modelBuilder.Entity("Demo1_Day2.Course", b =>
                {
                    b.Property<int>("CrsId")
                        .HasColumnType("int");

                    b.Property<string>("CrsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.HasKey("CrsId");

                    b.HasIndex("CrsName")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Demo1_Day2.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("DeptId");

                    b.HasIndex("DeptName")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Demo1_Day2.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeptNo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("StudentId");

                    b.HasIndex("DeptNo");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Demo1_Day2.StudentCourse", b =>
                {
                    b.Property<int>("StdId")
                        .HasColumnType("int");

                    b.Property<int>("CrsId")
                        .HasColumnType("int");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.HasKey("StdId", "CrsId");

                    b.HasIndex("CrsId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.HasOne("Demo1_Day2.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo1_Day2.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Demo1_Day2.Models.Student", b =>
                {
                    b.HasOne("Demo1_Day2.Models.Department", "DepartmentNavigation")
                        .WithMany("Students")
                        .HasForeignKey("DeptNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartmentNavigation");
                });

            modelBuilder.Entity("Demo1_Day2.StudentCourse", b =>
                {
                    b.HasOne("Demo1_Day2.Course", "CourseNavigation")
                        .WithMany("StudentCourse")
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo1_Day2.Models.Student", "StudentNavigation")
                        .WithMany("StudentCourse")
                        .HasForeignKey("StdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseNavigation");

                    b.Navigation("StudentNavigation");
                });

            modelBuilder.Entity("Demo1_Day2.Course", b =>
                {
                    b.Navigation("StudentCourse");
                });

            modelBuilder.Entity("Demo1_Day2.Models.Department", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Demo1_Day2.Models.Student", b =>
                {
                    b.Navigation("StudentCourse");
                });
#pragma warning restore 612, 618
        }
    }
}
