using ExpenseTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.BLL.Models
{
    public class TransactionVM
    {
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }
        public CategoryVM Category { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
