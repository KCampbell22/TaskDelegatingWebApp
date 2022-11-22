﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskDelegatingWebApp.Data;

#nullable disable

namespace TaskDelegatingWebApp.Migrations
{
    [DbContext(typeof(TaskDelegatingWebAppContext))]
    [Migration("20221120141955_DayNavigation")]
    partial class DayNavigation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Day", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DayId"));

                    b.Property<string>("DayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeekID")
                        .HasColumnType("int");

                    b.HasKey("DayId");

                    b.HasIndex("WeekID");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Friday")
                        .HasColumnType("bit");

                    b.Property<bool>("Monday")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Saturday")
                        .HasColumnType("bit");

                    b.Property<bool>("Sunday")
                        .HasColumnType("bit");

                    b.Property<int?>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<bool>("Thursday")
                        .HasColumnType("bit");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("bit");

                    b.HasKey("PersonId");

                    b.HasIndex("TaskItemId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.TaskAssignment", b =>
                {
                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.HasKey("TaskItemId", "PersonId");

                    b.HasIndex("DayId");

                    b.HasIndex("PersonId");

                    b.ToTable("TaskAssignment", (string)null);
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.TaskItem", b =>
                {
                    b.Property<int>("TaskItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskItemId"));

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("TaskDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeOfDay")
                        .HasColumnType("int");

                    b.HasKey("TaskItemId");

                    b.HasIndex("DayId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Week", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("WeekEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WeekStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Day", b =>
                {
                    b.HasOne("TaskDelegatingWebApp.Models.Week", "Week")
                        .WithMany("Days")
                        .HasForeignKey("WeekID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Week");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Person", b =>
                {
                    b.HasOne("TaskDelegatingWebApp.Models.TaskItem", null)
                        .WithMany("Persons")
                        .HasForeignKey("TaskItemId");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.TaskAssignment", b =>
                {
                    b.HasOne("TaskDelegatingWebApp.Models.Day", "Day")
                        .WithMany("TaskAssignments")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskDelegatingWebApp.Models.Person", "Person")
                        .WithMany("TaskAssignments")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskDelegatingWebApp.Models.TaskItem", "TaskItem")
                        .WithMany("TaskAssignments")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("Person");

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.TaskItem", b =>
                {
                    b.HasOne("TaskDelegatingWebApp.Models.Day", "Day")
                        .WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Day", b =>
                {
                    b.Navigation("TaskAssignments");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Person", b =>
                {
                    b.Navigation("TaskAssignments");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.TaskItem", b =>
                {
                    b.Navigation("Persons");

                    b.Navigation("TaskAssignments");
                });

            modelBuilder.Entity("TaskDelegatingWebApp.Models.Week", b =>
                {
                    b.Navigation("Days");
                });
#pragma warning restore 612, 618
        }
    }
}