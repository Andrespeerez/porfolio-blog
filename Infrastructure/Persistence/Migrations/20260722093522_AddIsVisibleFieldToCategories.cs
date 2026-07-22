using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace porfolio_blog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVisibleFieldToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Categories");
        }
    }
}
