using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgrammingTerm.Repository.Migrations
{
    /// <inheritdoc />
    public partial class imagePathMaxlength450 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "varchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(450)");
        }
    }
}
