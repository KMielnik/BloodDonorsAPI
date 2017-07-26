using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BloodDonors.Infrastructure.EntityFramework;

namespace BloodDonors.Infrastructure.Migrations
{
    [DbContext(typeof(BloodDonorsContext))]
    [Migration("20170726235400_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BloodDonors.Core.Domain.BloodDonation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BloodTakerPesel");

                    b.Property<string>("BloodTypeAboType");

                    b.Property<string>("BloodTypeRhType");

                    b.Property<DateTime>("DateOfDonation");

                    b.Property<string>("DonorPesel");

                    b.Property<int>("Volume");

                    b.HasKey("Id");

                    b.HasIndex("BloodTakerPesel");

                    b.HasIndex("DonorPesel");

                    b.HasIndex("BloodTypeAboType", "BloodTypeRhType");

                    b.ToTable("BloodDonations");
                });

            modelBuilder.Entity("BloodDonors.Core.Domain.BloodType", b =>
                {
                    b.Property<string>("AboType");

                    b.Property<string>("RhType");

                    b.HasKey("AboType", "RhType");

                    b.ToTable("BloodTypes");
                });

            modelBuilder.Entity("BloodDonors.Core.Domain.Donor", b =>
                {
                    b.Property<string>("Pesel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nchar(20)");

                    b.Property<string>("BloodTypeAboType");

                    b.Property<string>("BloodTypeRhType");

                    b.Property<DateTime?>("LastDonated");

                    b.Property<string>("Mail");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Salt");

                    b.HasKey("Pesel");

                    b.HasIndex("BloodTypeAboType", "BloodTypeRhType");

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("BloodDonors.Core.Domain.Personnel", b =>
                {
                    b.Property<string>("Pesel")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.HasKey("Pesel");

                    b.ToTable("Personnels");
                });

            modelBuilder.Entity("BloodDonors.Core.Domain.BloodDonation", b =>
                {
                    b.HasOne("BloodDonors.Core.Domain.Personnel", "BloodTaker")
                        .WithMany()
                        .HasForeignKey("BloodTakerPesel");

                    b.HasOne("BloodDonors.Core.Domain.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorPesel");

                    b.HasOne("BloodDonors.Core.Domain.BloodType", "BloodType")
                        .WithMany()
                        .HasForeignKey("BloodTypeAboType", "BloodTypeRhType");
                });

            modelBuilder.Entity("BloodDonors.Core.Domain.Donor", b =>
                {
                    b.HasOne("BloodDonors.Core.Domain.BloodType", "BloodType")
                        .WithMany()
                        .HasForeignKey("BloodTypeAboType", "BloodTypeRhType");
                });
        }
    }
}
