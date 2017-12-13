using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeOk.Models
{
    public class NotEmpty : IValidator<string>
    {
        public string ErrorMessage { get; }

        public NotEmpty(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public bool IsValid(string dataToValidate)
        {
            return !string.IsNullOrWhiteSpace(dataToValidate);
        }
    }

    public class AlphanumericOnly : IValidator<string>
    {
        static Regex NonAlphanumericPattern = new Regex(@"\W");

        public string ErrorMessage { get; }

        public AlphanumericOnly(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

    public bool IsValid(string dataToValidate)
        {
            return !NonAlphanumericPattern.IsMatch(dataToValidate);
        }
    }

    public class RequireMinLength : IValidator<string>
    {
        public int RequiredLength { get; } = 1;

        public string ErrorMessage { get; }

        public RequireMinLength(int requiredLength, string errorMessage)
        {
            ErrorMessage = errorMessage;
            RequiredLength = requiredLength;
        }

        public bool IsValid(string dataToValidate)
        {
            return dataToValidate.Length >= RequiredLength;
        }
    }

    public class RestrictMaxLength : IValidator<string>
    {
        public int MaxLength { get; } = 10;

        public string ErrorMessage { get; private set; }

        public RestrictMaxLength(int maxLength, string errorMessage)
        {
            MaxLength = maxLength;
            ErrorMessage = errorMessage;
        }

        public bool IsValid(string dataToValidate)
        {
            return dataToValidate.Length <= MaxLength;
        }
    }

    public class RequireUppercase: IValidator<string>
    {
        static Regex UppercasePattern = new Regex("[A-Z]");

        public string ErrorMessage { get; }

        public RequireUppercase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public bool IsValid(string dataToValidate)
        {
            return UppercasePattern.IsMatch(dataToValidate);
        }
    }

    public class RequireLowercase : IValidator<string>
    {
        static Regex LowercasePattern = new Regex("[a-z]");

        public string ErrorMessage { get; }

        public RequireLowercase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public bool IsValid(string dataToValidate)
        {
            return LowercasePattern.IsMatch(dataToValidate);
        }
    }

    public class RequireNumeric : IValidator<string>
    {
        static Regex NumericPattern = new Regex("[0-9]");

        public string ErrorMessage { get; }

        public RequireNumeric(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public bool IsValid(string dataToValidate)
        {
            return NumericPattern.IsMatch(dataToValidate);
        }
    }
}
