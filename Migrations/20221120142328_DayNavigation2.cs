using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskDelegatingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class DayNavigation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskAssignment",
                table: "TaskAssignment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskAssignment",
                table: "TaskAssignment",
                columns: new[] { "TaskItemId", "PersonId", "DayId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskAssignment",
                table: "TaskAssignment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskAssignment",
                table: "TaskAssignment",
                columns: new[] { "TaskItemId", "PersonId" });
        }
    }
}
