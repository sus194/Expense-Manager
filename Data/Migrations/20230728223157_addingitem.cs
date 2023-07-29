using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Manager.Data.Migrations
{
    public partial class addingitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "items",
                table: "ExpenseLimit",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "items",
                table: "ExpenseLimit");
        }
    }
}
