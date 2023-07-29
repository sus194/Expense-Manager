namespace Expense_Manager.Models
{
    public class ExpenseLimit
    {
        public int Id { get; set; }
        public string ExpenseType { get; set; }
        public decimal Limit { get; set; }
        public string ExpenseUserId { get; set; }

        public int items { get; set; }

        public ExpenseLimit()
        {

        }
    }
}
