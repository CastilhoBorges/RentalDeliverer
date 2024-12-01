﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalDeliverer.Migrations
{
    /// <inheritdoc />
    public partial class ResyncDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.MotorcycleId);
                });

            migrationBuilder.CreateTable(
                name: "RentalTypes",
                columns: table => new
                {
                    RentalTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalTypes", x => x.RentalTypeId);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Deliverer",
                columns: table => new
                {
                    DelivererId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    CNH = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CNHType = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CNHImgPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverer", x => x.DelivererId);
                    table.ForeignKey(
                        name: "FK_Deliverer_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DelivererId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Deliverer_DelivererId",
                        column: x => x.DelivererId,
                        principalTable: "Deliverer",
                        principalColumn: "DelivererId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "MotorcycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_RentalTypes_RentalTypeId",
                        column: x => x.RentalTypeId,
                        principalTable: "RentalTypes",
                        principalColumn: "RentalTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliverer_CNH",
                table: "Deliverer",
                column: "CNH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverer_CNPJ",
                table: "Deliverer",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverer_UserId",
                table: "Deliverer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_LicensePlate",
                table: "Motorcycles",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DelivererId",
                table: "Rentals",
                column: "DelivererId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalTypeId",
                table: "Rentals",
                column: "RentalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Mail",
                table: "user",
                column: "Mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Deliverer");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "RentalTypes");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}