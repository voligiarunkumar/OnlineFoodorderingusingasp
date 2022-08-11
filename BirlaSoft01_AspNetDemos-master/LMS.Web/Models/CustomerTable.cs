using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FoodOrdering.Web.Models
{
    [Table(name: "CustomersTable")]
    public class CustomerTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "First Name")]
        public string FistName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Customer Contact Number")]
        public string CustomerContactNumber { get; set; }


        #region Navigation Properties to the OrderTable Model

        


        #endregion


    }
}
