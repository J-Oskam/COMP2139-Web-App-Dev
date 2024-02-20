using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Labs.Migrations
{
    /// <inheritdoc />
    public partial class updatedprojectTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                table: "ProjectTasks");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectTasks",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectTaskId",
                table: "ProjectTasks",
                newName: "ProjectTaskID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_ProjectId",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_ProjectID");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectID",
                table: "ProjectTasks",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectID",
                table: "ProjectTasks");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "ProjectTasks",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "ProjectTaskID",
                table: "ProjectTasks",
                newName: "ProjectTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_ProjectID",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_ProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
