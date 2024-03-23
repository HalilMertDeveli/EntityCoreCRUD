using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_for_six_week.Migrations
{
    /// <inheritdoc />
    public partial class AuthorMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorDbSet",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorNationality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDbSet", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "AuthorInfoDbSet",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    AuthorInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwardsReceived = table.Column<int>(type: "int", nullable: false),
                    FamousWork = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorInfoDbSet", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_AuthorInfoDbSet_AuthorDbSet_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AuthorDbSet",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookDbSet",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDbSet", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_BookDbSet_AuthorDbSet_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AuthorDbSet",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDbSet_AuthorId",
                table: "BookDbSet",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorInfoDbSet");

            migrationBuilder.DropTable(
                name: "BookDbSet");

            migrationBuilder.DropTable(
                name: "AuthorDbSet");
        }
    }
}
