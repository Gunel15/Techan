using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Account
{
    public class RegisterVM
    {
        [MaxLength(64)]
        public string FullName {  get; set; }
        [MaxLength(64)]
        public string Username {  get; set; }
        [MaxLength(128),DataType(DataType.EmailAddress)]
        public string Email {  get; set; }
        [MaxLength(128),MinLength(6),DataType(DataType.Password)]
        public string Password {  get; set; }
        [MaxLength(128), MinLength(6),DataType(DataType.Password),Compare(nameof(Password))]
        public string RepeatPassword {  get; set; }
       
    }
}
