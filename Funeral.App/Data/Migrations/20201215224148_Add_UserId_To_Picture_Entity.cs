using Microsoft.EntityFrameworkCore.Migrations;

namespace Funeral.Data.Migrations
{
    public partial class Add_UserId_To_Picture_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pictures");
        }
    }
}
