using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Activities.Infra.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersActivity_Activities_ActivityId",
                table: "WorkersActivity");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersActivity_Activities_ActivityId",
                table: "WorkersActivity",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersActivity_Activities_ActivityId",
                table: "WorkersActivity");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersActivity_Activities_ActivityId",
                table: "WorkersActivity",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
