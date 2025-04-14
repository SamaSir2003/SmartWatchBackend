using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWatchRepair.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceModels_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceModelId = table.Column<int>(type: "int", nullable: false),
                    IssueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairIssues_DeviceModels_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
     name: "Bookings",
     columns: table => new
     {
         Id = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
         DeviceModelId = table.Column<int>(type: "int", nullable: false),
         RepairIssueId = table.Column<int>(type: "int", nullable: false),
         Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
         BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
         EstimatedDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
         TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
         TechnicianId = table.Column<string>(type: "nvarchar(max)", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Bookings", x => x.Id);
         table.ForeignKey(
             name: "FK_Bookings_DeviceModels_DeviceModelId",
             column: x => x.DeviceModelId,
             principalTable: "DeviceModels",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);  // Keep cascade for DeviceModelId if necessary
         table.ForeignKey(
             name: "FK_Bookings_RepairIssues_RepairIssueId",
             column: x => x.RepairIssueId,
             principalTable: "RepairIssues",
             principalColumn: "Id",
             onDelete: ReferentialAction.Restrict);  // Change to Restrict or NoAction
         table.ForeignKey(
             name: "FK_Bookings_Users_UserId",
             column: x => x.UserId,
             principalTable: "Users",
             principalColumn: "Id",
             onDelete: ReferentialAction.Restrict);  // Change to Restrict or NoAction
     });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicianId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianRatings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianRatings_Users_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DeviceModelId",
                table: "Bookings",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RepairIssueId",
                table: "Bookings",
                column: "RepairIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceModels_BrandId",
                table: "DeviceModels",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairIssues_DeviceModelId",
                table: "RepairIssues",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianRatings_BookingId",
                table: "TechnicianRatings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianRatings_TechnicianId",
                table: "TechnicianRatings",
                column: "TechnicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TechnicianRatings");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "RepairIssues");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DeviceModels");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
