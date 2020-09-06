using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentApplicationBackEnd.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 500, nullable: false),
                    Class = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Subjects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Subjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Marks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false),
                    StudentMark = table.Column<int>(nullable: false),
                    tbl_StudentID = table.Column<int>(nullable: true),
                    tbl_SubjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Marks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_Marks_tbl_Students_tbl_StudentID",
                        column: x => x.tbl_StudentID,
                        principalTable: "tbl_Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Marks_tbl_Subjects_tbl_SubjectID",
                        column: x => x.tbl_SubjectID,
                        principalTable: "tbl_Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tbl_Subjects",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Math" });

            migrationBuilder.InsertData(
                table: "tbl_Subjects",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Physics" });

            migrationBuilder.InsertData(
                table: "tbl_Subjects",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Chemistry" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Marks_tbl_StudentID",
                table: "tbl_Marks",
                column: "tbl_StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Marks_tbl_SubjectID",
                table: "tbl_Marks",
                column: "tbl_SubjectID");

            migrationBuilder.CreateIndex(
                name: "IDX_tbl_Subjects_Name",
                table: "tbl_Subjects",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Marks");

            migrationBuilder.DropTable(
                name: "tbl_Students");

            migrationBuilder.DropTable(
                name: "tbl_Subjects");
        }
    }
}
