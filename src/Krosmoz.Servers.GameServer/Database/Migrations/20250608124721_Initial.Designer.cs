﻿// <auto-generated />
using System;
using Krosmoz.Servers.GameServer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20250608124721_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Characters.CharacterRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("AlignmentHonor")
                        .HasColumnType("integer");

                    b.Property<int>("AlignmentSide")
                        .HasColumnType("integer");

                    b.Property<int>("AlignmentStatus")
                        .HasColumnType("integer");

                    b.Property<short>("AlignmentValue")
                        .HasColumnType("smallint");

                    b.Property<int>("Breed")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Characteristics")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DeathCount")
                        .HasColumnType("integer");

                    b.Property<byte>("DeathMaxLevel")
                        .HasColumnType("smallint");

                    b.Property<int>("DeathState")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.PrimitiveCollection<int[]>("Emotes")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<decimal>("Experience")
                        .HasColumnType("numeric(20,0)");

                    b.Property<byte[]>("GeneralShortcutBar")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("Head")
                        .HasColumnType("integer");

                    b.Property<int>("Kamas")
                        .HasColumnType("integer");

                    b.Property<string>("Look")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MandatoryChanges")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Position")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("PossibleChanges")
                        .HasColumnType("integer");

                    b.Property<int>("Restrictions")
                        .HasColumnType("integer");

                    b.Property<bool>("Sex")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("SpellShortcutBar")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.PrimitiveCollection<int[]>("Spells")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int?>("SpouseId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StatusMessage")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.UseCollation(b.HasIndex("Name"), new[] { "case_insensitive" });

                    b.ToTable("characters", (string)null);
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Experiences.ExperienceRecord", b =>
                {
                    b.Property<byte>("Level")
                        .HasColumnType("smallint");

                    b.Property<decimal?>("AlignmentHonor")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("CharacterXp")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("GuildXp")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("JobXp")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("MountXp")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Level");

                    b.ToTable("experiences", (string)null);
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Interactives.InteractiveActionRecord", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Criterion")
                        .HasColumnType("text");

                    b.Property<int>("InteractiveId")
                        .HasColumnType("integer");

                    b.Property<int>("InteractiveTemplateId")
                        .HasColumnType("integer");

                    b.Property<long?>("NameId")
                        .HasColumnType("bigint");

                    b.PrimitiveCollection<string[]>("Parameters")
                        .HasColumnType("text[]");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InteractiveId");

                    b.ToTable("interactives_actions", (string)null);
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Interactives.InteractiveRecord", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("Animated")
                        .HasColumnType("boolean");

                    b.Property<int>("ElementId")
                        .HasColumnType("integer");

                    b.Property<int>("GfxId")
                        .HasColumnType("integer");

                    b.Property<int>("MapId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("MapsData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("interactives", (string)null);
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Items.ItemAppearanceRecord", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("AppearanceId")
                        .HasColumnType("integer");

                    b.Property<string>("CustomLook")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("items_appearances", (string)null);
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Maps.MapRecord", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.PrimitiveCollection<short[]>("BlueCells")
                        .IsRequired()
                        .HasColumnType("smallint[]");

                    b.Property<short?>("BottomCellId")
                        .HasColumnType("smallint");

                    b.Property<int>("BottomNeighbourId")
                        .HasColumnType("integer");

                    b.Property<long>("Capabilities")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Cells")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("HasPriorityOnWorldMap")
                        .HasColumnType("boolean");

                    b.Property<short?>("LeftCellId")
                        .HasColumnType("smallint");

                    b.Property<int>("LeftNeighbourId")
                        .HasColumnType("integer");

                    b.Property<int>("MerchantsMax")
                        .HasColumnType("integer");

                    b.Property<bool>("Outdoor")
                        .HasColumnType("boolean");

                    b.Property<bool>("PlacementGenDisabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("PrismAllowed")
                        .HasColumnType("boolean");

                    b.Property<bool>("PvpDisabled")
                        .HasColumnType("boolean");

                    b.PrimitiveCollection<short[]>("RedCells")
                        .IsRequired()
                        .HasColumnType("smallint[]");

                    b.Property<short?>("RightCellId")
                        .HasColumnType("smallint");

                    b.Property<int>("RightNeighbourId")
                        .HasColumnType("integer");

                    b.Property<bool>("SpawnDisabled")
                        .HasColumnType("boolean");

                    b.Property<int>("SubAreaId")
                        .HasColumnType("integer");

                    b.Property<short?>("TopCellId")
                        .HasColumnType("smallint");

                    b.Property<int>("TopNeighbourId")
                        .HasColumnType("integer");

                    b.Property<int>("WorldMap")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("maps", (string)null);
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Interactives.InteractiveActionRecord", b =>
                {
                    b.HasOne("Krosmoz.Servers.GameServer.Database.Models.Interactives.InteractiveRecord", null)
                        .WithMany("InteractiveActions")
                        .HasForeignKey("InteractiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Interactives.InteractiveRecord", b =>
                {
                    b.HasOne("Krosmoz.Servers.GameServer.Database.Models.Maps.MapRecord", null)
                        .WithMany("Interactives")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Interactives.InteractiveRecord", b =>
                {
                    b.Navigation("InteractiveActions");
                });

            modelBuilder.Entity("Krosmoz.Servers.GameServer.Database.Models.Maps.MapRecord", b =>
                {
                    b.Navigation("Interactives");
                });
#pragma warning restore 612, 618
        }
    }
}
