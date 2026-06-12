using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelivaryWebAspCore.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerLocation",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerLocation",
                table: "Products");
        }
    }
}
