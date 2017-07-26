using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonors.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    AboType = table.Column<string>(nullable: false),
                    RhType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => new { x.AboType, x.RhType });
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Pesel = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Pesel);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Pesel = table.Column<string>(type: "nchar(20)", nullable: false),
                    BloodTypeAboType = table.Column<string>(nullable: true),
                    BloodTypeRhType = table.Column<string>(nullable: true),
                    LastDonated = table.Column<DateTime>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Pesel);
                    table.ForeignKey(
                        name: "FK_Donors_BloodTypes_BloodTypeAboType_BloodTypeRhType",
                        columns: x => new { x.BloodTypeAboType, x.BloodTypeRhType },
                        principalTable: "BloodTypes",
                        principalColumns: new[] { "AboType", "RhType" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodDonations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BloodTakerPesel = table.Column<string>(nullable: true),
                    BloodTypeAboType = table.Column<string>(nullable: true),
                    BloodTypeRhType = table.Column<string>(nullable: true),
                    DateOfDonation = table.Column<DateTime>(nullable: false),
                    DonorPesel = table.Column<string>(nullable: true),
                    Volume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonations_Personnels_BloodTakerPesel",
                        column: x => x.BloodTakerPesel,
                        principalTable: "Personnels",
                        principalColumn: "Pesel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodDonations_Donors_DonorPesel",
                        column: x => x.DonorPesel,
                        principalTable: "Donors",
                        principalColumn: "Pesel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodDonations_BloodTypes_BloodTypeAboType_BloodTypeRhType",
                        columns: x => new { x.BloodTypeAboType, x.BloodTypeRhType },
                        principalTable: "BloodTypes",
                        principalColumns: new[] { "AboType", "RhType" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_BloodTakerPesel",
                table: "BloodDonations",
                column: "BloodTakerPesel");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_DonorPesel",
                table: "BloodDonations",
                column: "DonorPesel");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_BloodTypeAboType_BloodTypeRhType",
                table: "BloodDonations",
                columns: new[] { "BloodTypeAboType", "BloodTypeRhType" });

            migrationBuilder.CreateIndex(
                name: "IX_Donors_BloodTypeAboType_BloodTypeRhType",
                table: "Donors",
                columns: new[] { "BloodTypeAboType", "BloodTypeRhType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodDonations");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "BloodTypes");
        }
    }
}
