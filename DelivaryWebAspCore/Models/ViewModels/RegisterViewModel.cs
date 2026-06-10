using System.ComponentModel.DataAnnotations;

namespace DelivaryWebAspCore.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength (100)]
        public string Name { get; set; }



        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage ="Password must be 4 character")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm password is Required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password & cconfirm password do not match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Shop name is required")]
        [StringLength(150)]
        public string ShopeName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public long Phone { get; set; }
    }
}
