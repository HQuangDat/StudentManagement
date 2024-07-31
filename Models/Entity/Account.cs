using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models.Entity
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Gender")]
        public bool gender { get; set; }
    }
}