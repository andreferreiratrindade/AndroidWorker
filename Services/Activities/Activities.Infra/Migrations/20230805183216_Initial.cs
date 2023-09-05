using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Activities.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeActivityBuild = table.Column<byte>(type: "tinyint", nullable: false),
                    TimeActivityStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeActivityEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkersActivity",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    WorkerId = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkersActivity", x => new { x.ActivityId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_WorkersActivity_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkersActivity");

            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
