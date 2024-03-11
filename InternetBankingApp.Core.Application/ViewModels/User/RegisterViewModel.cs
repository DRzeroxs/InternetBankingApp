using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "The Name Field is Required")]
        [DataType (DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name Field is Required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Email Field is Required")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Identification Card Field is Required")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Identification Card must be exactly 9 digits")]
        public string IdentificationCard { get; set; }
        [Required(ErrorMessage = "The User Name Field is Required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The Password Field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Confirn Password Field is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The  Password must be the same")]
        public string ConfirnPassword { get; set; }
        [Required(ErrorMessage = "The Type of User Field is Required")]
        [DataType(DataType.Text)]
        public string TypeOfUser { get; set; }
        [DataType(DataType.Text)]
        public string? StartAmount { get; set; } 
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
