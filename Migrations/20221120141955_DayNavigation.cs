using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskDelegatingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class DayNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_Days_DayId",
                table: "TaskAssignment");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "TaskAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_Days_DayId",
                table: "TaskAssignment",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "DayId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_Days_DayId",
                table: "TaskAssignment");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "TaskAssignment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_Days_DayId",
                table: "TaskAssignment",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "DayId");
        }
    }
}
