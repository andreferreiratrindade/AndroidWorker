using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rests.Infra.Migrations
{
    /// <inheritdoc />
    public partial class tempCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rests",
                columns: table => new
                {
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    WorkerId = table.Column<string>(type: "char(1)", nullable: false),
                    TimeRestStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeRestEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeActivityBuild = table.Column<byte>(type: "tinyint", nullable: false),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rests", x => x.AggregateId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rests");
        }
    }
}
