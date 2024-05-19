using Microsoft.EntityFrameworkCore.Migrations;

namespace Spark.Migrations
{
    public partial class addeddCreadtedByToComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreadtedBy",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadtedBy",
                table: "Comments");
        }
    }
}
