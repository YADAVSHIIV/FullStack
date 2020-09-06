using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentApplicationBackEnd.Migrations
{
    public partial class foreignkeyadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Marks_tbl_Students_tbl_StudentID",
                table: "tbl_Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Marks_tbl_Subjects_tbl_SubjectID",
                table: "tbl_Marks");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Marks_tbl_StudentID",
                table: "tbl_Marks");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Marks_tbl_SubjectID",
                table: "tbl_Marks");

            migrationBuilder.DropColumn(
                name: "tbl_StudentID",
                table: "tbl_Marks");

            migrationBuilder.DropColumn(
                name: "tbl_SubjectID",
                table: "tbl_Marks");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Marks_StudentID",
                table: "tbl_Marks",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Students_tbl_Marks",
                table: "tbl_Marks",
                column: "StudentID",
                principalTable: "tbl_Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Subjects_tbl_Marks",
                table: "tbl_Marks",
                column: "StudentID",
                principalTable: "tbl_Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Students_tbl_Marks",
                table: "tbl_Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Subjects_tbl_Marks",
                table: "tbl_Marks");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Marks_StudentID",
                table: "tbl_Marks");

            migrationBuilder.AddColumn<int>(
                name: "tbl_StudentID",
                table: "tbl_Marks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tbl_SubjectID",
                table: "tbl_Marks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Marks_tbl_StudentID",
                table: "tbl_Marks",
                column: "tbl_StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Marks_tbl_SubjectID",
                table: "tbl_Marks",
                column: "tbl_SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Marks_tbl_Students_tbl_StudentID",
                table: "tbl_Marks",
                column: "tbl_StudentID",
                principalTable: "tbl_Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Marks_tbl_Subjects_tbl_SubjectID",
                table: "tbl_Marks",
                column: "tbl_SubjectID",
                principalTable: "tbl_Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
