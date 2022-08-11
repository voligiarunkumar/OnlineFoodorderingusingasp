using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FoodOrdering.Web.Models
{
    [Table(name: "AdminsTable")]
    public class AdminTable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name ="Admin User Name")]
        public string AdminName { get; set; }

        [Required]
        [MaxLength(12)]
        [Display(Name ="Admin Password")]
        public string Password { get; set; }
    }
}
