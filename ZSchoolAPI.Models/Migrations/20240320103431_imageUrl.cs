using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZSchoolAPI.Models.Migrations
{
    /// <inheritdoc />
    public partial class imageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seance_Classrooms_ClassroomId",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_Teachers_TeacherId",
                table: "Seance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seance",
                table: "Seance");

            migrationBuilder.RenameTable(
                name: "Seance",
                newName: "Seances");

            migrationBuilder.RenameIndex(
                name: "IX_Seance_TeacherId",
                table: "Seances",
                newName: "IX_Seances_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Seance_ClassroomId",
                table: "Seances",
                newName: "IX_Seances_ClassroomId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seances",
                table: "Seances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seances_Classrooms_ClassroomId",
                table: "Seances",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seances_Teachers_TeacherId",
                table: "Seances",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seances_Classrooms_ClassroomId",
                table: "Seances");

            migrationBuilder.DropForeignKey(
                name: "FK_Seances_Teachers_TeacherId",
                table: "Seances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seances",
                table: "Seances");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Seances",
                newName: "Seance");

            migrationBuilder.RenameIndex(
                name: "IX_Seances_TeacherId",
                table: "Seance",
                newName: "IX_Seance_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Seances_ClassroomId",
                table: "Seance",
                newName: "IX_Seance_ClassroomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seance",
                table: "Seance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_Classrooms_ClassroomId",
                table: "Seance",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_Teachers_TeacherId",
                table: "Seance",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
