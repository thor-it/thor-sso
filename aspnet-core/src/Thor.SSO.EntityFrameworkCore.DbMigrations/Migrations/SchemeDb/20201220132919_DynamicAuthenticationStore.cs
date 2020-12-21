using Microsoft.EntityFrameworkCore.Migrations;

namespace Thor.SSO.Migrations.SchemeDb
{
    public partial class DynamicAuthenticationStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Scheme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerializedHandlerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerializedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Scheme);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
