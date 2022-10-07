using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginTokenTask.Migrations
{
    public partial class l : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GagetTbl",
                columns: table => new
                {
                    GadgetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GadgetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GadgetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GadgetPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GagetTbl", x => x.GadgetId);
                });

            migrationBuilder.CreateTable(
                name: "UserTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTbl", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GagetTbl");

            migrationBuilder.DropTable(
                name: "UserTbl");
        }
    }
}
