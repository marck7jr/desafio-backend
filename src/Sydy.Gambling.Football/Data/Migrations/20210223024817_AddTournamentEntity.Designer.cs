﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sydy.Gambling.Football.Data;

namespace Sydy.Gambling.Football.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210223024817_AddTournamentEntity")]
    partial class AddTournamentEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.MatchResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("MatchResults");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.Match", b =>
                {
                    b.HasOne("Sydy.Gambling.Football.Data.Models.Tournament", null)
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.MatchResult", b =>
                {
                    b.HasOne("Sydy.Gambling.Football.Data.Models.Match", "Match")
                        .WithMany("Results")
                        .HasForeignKey("MatchId");

                    b.HasOne("Sydy.Gambling.Football.Data.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Match");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.Match", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("Sydy.Gambling.Football.Data.Models.Tournament", b =>
                {
                    b.Navigation("Matches");
                });
#pragma warning restore 612, 618
        }
    }
}
