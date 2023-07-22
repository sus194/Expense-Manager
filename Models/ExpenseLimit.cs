namespace Expense_Manager.Models
{
    public class ExpenseLimit
    {
        public int Id { get; set; }
        public string ExpenseType { get; set; }
        public string Limit { get; set; }  
        

        public ExpenseLimit()
        {

        }
    }
}
