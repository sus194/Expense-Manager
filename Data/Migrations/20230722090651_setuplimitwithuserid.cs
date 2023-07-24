using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Manager.Data.Migrations
{
    public partial class setuplimitwithuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseLimitId",
                table: "Expense");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseUserId",
                table: "ExpenseLimit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseUserId",
                table: "ExpenseLimit");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseLimitId",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
