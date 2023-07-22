using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Manager.Data.Migrations
{
    public partial class intialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseLimitId = table.Column<int>(type: "nvarchar(450)", nullable: true),
                    ExpenseUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                                 name: "FK_Expense_AspNetUsers_ExpenseUserId",
                                 column: x => x.ExpenseUserId,
                                 principalTable: "AspNetUsers",
                                 principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseLimit_ExpenseLimitId", // Specify a unique name for the foreign key constraint
                        column: x => x.ExpenseLimitId,
                        principalTable: "ExpenseLimit", // Replace "AnotherTable" with the name of the related table
                        principalColumn: "Id");




    
                });

                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");
        }
    }
}
