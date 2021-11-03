﻿// <auto-generated />
using System;
using Hospitals.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hospitals.Migrations.HospitalData
{
    [DbContext(typeof(HospitalDataContext))]
    partial class HospitalDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Hospitals.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommentText")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReviewID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("CommentID");

                    b.HasIndex("ReviewID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Hospitals.Models.Hospital", b =>
                {
                    b.Property<int>("HospitalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Rating")
                        .HasColumnType("REAL");

                    b.Property<int>("ReviewsCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<int>("Zip")
                        .HasColumnType("INTEGER");

                    b.HasKey("HospitalID");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("Hospitals.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Department")
                        .HasColumnType("TEXT");

                    b.Property<int>("HospitalID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReviewText")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<double>("Salary")
                        .HasColumnType("REAL");

                    b.Property<string>("Speciality")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("ReviewID");

                    b.HasIndex("HospitalID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Hospitals.Models.Comment", b =>
                {
                    b.HasOne("Hospitals.Models.Review", "Review")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Hospitals.Models.Review", b =>
                {
                    b.HasOne("Hospitals.Models.Hospital", "Hospital")
                        .WithMany("Reviews")
                        .HasForeignKey("HospitalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("Hospitals.Models.Hospital", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Hospitals.Models.Review", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
