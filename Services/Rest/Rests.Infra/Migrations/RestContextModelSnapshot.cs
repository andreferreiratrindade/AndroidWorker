﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rests.Infra;

#nullable disable

namespace Rests.Infra.Migrations
{
    [DbContext(typeof(RestContext))]
    partial class RestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Rests.Domain.Models.Entities.Rest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeRestEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeRestStart")
                        .HasColumnType("datetime2");

                    b.Property<byte>("TypeActivityBuild")
                        .HasColumnType("tinyint");

                    b.Property<string>("WorkerId")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.HasKey("Id");

                    b.ToTable("Rests", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
