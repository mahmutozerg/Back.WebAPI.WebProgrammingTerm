using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgrammingTerm.Repository.Migrations
{
    /// <inheritdoc />
    public partial class bdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BirthDate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ProductDetails",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }
    }
}
