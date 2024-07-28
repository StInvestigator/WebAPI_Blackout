using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Blackout.Migrations
{
    /// <inheritdoc />
    public partial class statusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Hour = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerStatus_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerStatus_GroupId",
                table: "PowerStatus",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerStatus");
        }
    }
}
