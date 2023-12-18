using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgrammingTerm.Repository.Migrations
{
    /// <inheritdoc />
    public partial class locationCityNvarchar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");
        }
    }
}
