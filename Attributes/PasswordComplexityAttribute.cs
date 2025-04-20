using System.ComponentModel.DataAnnotations;

namespace TrungTamQuanLiDT.Attributes
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Mật khẩu không được để trống");
            }
            var password = value.ToString();
            
            if (password.Length < 8 && password.Length > 18)
            {
                return new ValidationResult("Mật khẩu phải có từ 8 đến 18 ký tự");
            }
            if (!password.Any(char.IsUpper))
            {
                return new ValidationResult("Mật khẩu phải chứa ít nhất một chữ cái viết hoa");
            }
            if (!password.Any(char.IsLower))
            {
                return new ValidationResult("Mật khẩu phải chứa ít nhất một chữ cái viết thường");
            }
            if (!password.Any(char.IsDigit))
            {
                return new ValidationResult("Mật khẩu phải chứa ít nhất một chữ số");
            }
            if (!password.Any(ch => "!@#$%^&*()_+[]{}|;:,.<>?".Contains(ch)))
            {
                return new ValidationResult("Mật khẩu phải chứa ít nhất một ký tự đặc biệt");
            }
            return ValidationResult.Success;
        }
    }
}
