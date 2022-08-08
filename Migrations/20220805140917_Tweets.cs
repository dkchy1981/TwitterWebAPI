using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterDemoAPI.Migrations
{
    public partial class Tweets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Tweets",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tweets",
                newName: "Username");
        }
    }
}
