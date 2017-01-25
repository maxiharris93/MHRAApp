using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MHRAApp.Models;

namespace MHRAApp.Migrations
{
    [DbContext(typeof(HorseContext))]
    [Migration("20170125103338_AcademyData")]
    partial class AcademyData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("MHRAApp.Models.Horse", b =>
                {
                    b.Property<int>("HorseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Breed");

                    b.Property<string>("HorseDiscipline");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.HasKey("HorseId");

                    b.ToTable("Horses");
                });
        }
    }
}
