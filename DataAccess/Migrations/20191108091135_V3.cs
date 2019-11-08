using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCondition",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostumerID",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScrapListID",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Costumers",
                columns: table => new
                {
                    CostumerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostumerBirthDate = table.Column<string>(nullable: true),
                    CostumerName = table.Column<string>(nullable: true),
                    CostumerAddress = table.Column<string>(nullable: true),
                    IsInDebt = table.Column<bool>(nullable: false),
                    HasBorrowedBook = table.Column<bool>(nullable: false),
                    AmountOfBooks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costumers", x => x.CostumerID);
                });

            migrationBuilder.CreateTable(
                name: "ScrapLists",
                columns: table => new
                {
                    ScrapListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryEmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapLists", x => x.ScrapListID);
                });

            migrationBuilder.CreateTable(
                name: "LibraryBills",
                columns: table => new
                {
                    LibraryBillID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryBillNumber = table.Column<int>(nullable: false),
                    MonthlyFee = table.Column<decimal>(nullable: false),
                    DelayFee = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CostumerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBills", x => x.LibraryBillID);
                    table.ForeignKey(
                        name: "FK_LibraryBills_Costumers_CostumerID",
                        column: x => x.CostumerID,
                        principalTable: "Costumers",
                        principalColumn: "CostumerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CostumerID",
                table: "Books",
                column: "CostumerID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ScrapListID",
                table: "Books",
                column: "ScrapListID");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBills_CostumerID",
                table: "LibraryBills",
                column: "CostumerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Costumers_CostumerID",
                table: "Books",
                column: "CostumerID",
                principalTable: "Costumers",
                principalColumn: "CostumerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ScrapLists_ScrapListID",
                table: "Books",
                column: "ScrapListID",
                principalTable: "ScrapLists",
                principalColumn: "ScrapListID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Costumers_CostumerID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_ScrapLists_ScrapListID",
                table: "Books");

            migrationBuilder.DropTable(
                name: "LibraryBills");

            migrationBuilder.DropTable(
                name: "ScrapLists");

            migrationBuilder.DropTable(
                name: "Costumers");

            migrationBuilder.DropIndex(
                name: "IX_Books_CostumerID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ScrapListID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookCondition",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CostumerID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ScrapListID",
                table: "Books");
        }
    }
}
