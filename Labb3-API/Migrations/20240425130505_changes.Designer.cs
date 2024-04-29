﻿// <auto-generated />
using Labb3_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Labb3_API.Migrations
{
    [DbContext(typeof(PersonInterestDbContext))]
    [Migration("20240425130505_changes")]
    partial class changes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Labb3_API.Models.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterestId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("Labb3_API.Models.Link", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LinkId"));

                    b.Property<int>("FkInterestId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LinkId");

                    b.HasIndex("FkInterestId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Labb3_API.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Labb3_API.Models.PersonInterest", b =>
                {
                    b.Property<int>("PersonInterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonInterestId"));

                    b.Property<int>("FkInterestId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.HasKey("PersonInterestId");

                    b.HasIndex("FkInterestId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("PersonInterests");
                });

            modelBuilder.Entity("Labb3_API.Models.Link", b =>
                {
                    b.HasOne("Labb3_API.Models.Interest", "Interests")
                        .WithMany()
                        .HasForeignKey("FkInterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interests");
                });

            modelBuilder.Entity("Labb3_API.Models.PersonInterest", b =>
                {
                    b.HasOne("Labb3_API.Models.Interest", "Interests")
                        .WithMany()
                        .HasForeignKey("FkInterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Labb3_API.Models.Person", "Persons")
                        .WithMany()
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interests");

                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
