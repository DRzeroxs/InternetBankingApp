using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.User
{
    public class EditAdminViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name Field is Required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Email Field is Required")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
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
    }
}
