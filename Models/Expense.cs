namespace Expense_Manager.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseType { get; set; }
        
        public decimal ExpenseAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        
        
        public string ExpenseUserId { get; set; }
        

        public Expense()
        {
            
        }
    }
}
