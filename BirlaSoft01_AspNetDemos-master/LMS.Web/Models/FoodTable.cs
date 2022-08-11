
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FoodOrdering.Web.Models
{
    [Table(name: "FoodTables")]
    public class FoodTable
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get;set;}
        [Required]
        [MaxLength(50)]
        [Display(Name =" User Name")]
        public string UserName {set;get;}

        [Key]
        [Required(ErrorMessage = "Food Name Cannot be null")]
        [StringLength(50)]
        [Display(Name ="Food Name")]
        public string FoodName { get;set;}

        [Required(ErrorMessage = "Food Price Cannot be null")]

        [Display(Name ="Food Price")]
        [Range(minimum: 0, maximum: 200000, ErrorMessage = "{0} has to be between {1} and {2}")]
         public int FoodPrice{ get;set;}

        [Required(ErrorMessage ="Food type Cannot be null")]
        [MaxLength(10)]
        [Display(Name ="Food Type")]
        public string FoodType { get;set;}
        public ICollection<OrderTable> OrdersTable { get; set; }

    }
}
