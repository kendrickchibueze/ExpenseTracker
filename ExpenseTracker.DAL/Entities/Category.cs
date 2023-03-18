using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseTracker.DAL.Entities
{
    public class Category: BaseEntity
    {
        
        public int CategoryId { get; set; }
    
        public string Title { get; set; }

        public string Icon { get; set; } = "";
 
        public string Type { get; set; } = "Expense";
        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; }



        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Title;
            }
        }


    }

   


}
