using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZzzProject.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Guest_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    E_mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guest_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_number = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Guest_id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Inventory_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inventory_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Inventory_Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_rate = table.Column<double>(type: "float", nullable: false),
                    room_status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.room_id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    reservation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    no_of_adults = table.Column<int>(type: "int", nullable: false),
                    no_of_children = table.Column<int>(type: "int", nullable: false),
                    Check_out = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Check_in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    no_of_nights = table.Column<int>(type: "int", nullable: false),
                    Guest_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestsGuest_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Roomsroom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.reservation_id);
                    table.ForeignKey(
                        name: "FK_Reservations_Guests_GuestsGuest_id",
                        column: x => x.GuestsGuest_id,
                        principalTable: "Guests",
                        principalColumn: "Guest_id");
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_Roomsroom_id",
                        column: x => x.Roomsroom_id,
                        principalTable: "Rooms",
                        principalColumn: "room_id");
                });

            migrationBuilder.CreateTable(
                name: "Users_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Staffs_UserId",
                        column: x => x.UserId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Bill_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stay_dates = table.Column<int>(type: "int", nullable: false),
                    total_bill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reservation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reservationsreservation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Roomsroom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Bill_id);
                    table.ForeignKey(
                        name: "FK_Bills_Reservations_Reservationsreservation_id",
                        column: x => x.Reservationsreservation_id,
                        principalTable: "Reservations",
                        principalColumn: "reservation_id");
                    table.ForeignKey(
                        name: "FK_Bills_Rooms_Roomsroom_id",
                        column: x => x.Roomsroom_id,
                        principalTable: "Rooms",
                        principalColumn: "room_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Reservationsreservation_id",
                table: "Bills",
                column: "Reservationsreservation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Roomsroom_id",
                table: "Bills",
                column: "Roomsroom_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestsGuest_id",
                table: "Reservations",
                column: "GuestsGuest_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Roomsroom_id",
                table: "Reservations",
                column: "Roomsroom_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roles_RoleId",
                table: "Users_Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roles_UserId",
                table: "Users_Roles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Users_Roles");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
