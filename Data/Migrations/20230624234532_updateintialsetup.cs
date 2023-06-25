using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Manager.Data.Migrations
{
    public partial class updateintialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseLocation",
                table: "Expense");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpenseLocation",
                table: "Expense",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
