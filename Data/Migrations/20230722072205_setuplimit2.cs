using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Manager.Data.Migrations
{
    public partial class setuplimit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Limit",
                table: "ExpenseLimit",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseLimitId",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseLimitId",
                table: "Expense");

            migrationBuilder.AlterColumn<decimal>(
                name: "Limit",
                table: "ExpenseLimit",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
