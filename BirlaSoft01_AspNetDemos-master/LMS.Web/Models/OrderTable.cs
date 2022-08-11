using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FoodOrdering.Web.Models
{
    [Table(name: "OrdersTable")]
    public class OrderTable
    {
        [Key]                                                       // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       // Identity Column
        [Display(Name = "Order ID")]
        
        public int OrderID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FoodName { get; set; }
        
        [Required]
        public DateTime DateofOrder { get; set; }  

        

        [Required]
        
        public int Quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerAddress { get; set; }

        [ForeignKey(nameof(OrderTable.FoodName))]
        public FoodTable FoodTable{ get; set; }


    }
}
 