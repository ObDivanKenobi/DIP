using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeSmells.Models
{
    public class Validator
    {
        static Regex NonAlphanumericPattern = new Regex(@"\W");
        static Regex LowercasePattern = new Regex("[a-z]");
        static Regex UppercasePattern = new Regex("[A-Z]");
        static Regex NumericPattern = new Regex(@"\d");

        public int RequiredLength { get; set; } = 1;
        public bool RequireLength { get; set; } = false;

        public bool RequireNonAlphanumeric { get; set; } = false;
        public bool AllowNonAlphanumeric { get; set; } = false;
        public bool RequireLowercase { get; set; } = false;
        public bool RequireUppercase { get; set; } = false;
        public bool RequireNumeric { get; set; } = false;
        public string ErrorMessage { get; set; } = "Разрешены только алфавитно-цифровые символы.";

        public bool IsValid(string str)
        {
            return (!string.IsNullOrWhiteSpace(str)) &&
                   (!RequireLength | str.Length >= RequiredLength) &&
                   (!RequireNonAlphanumeric | NonAlphanumericPattern.IsMatch(str)) &&
                   (AllowNonAlphanumeric | !NonAlphanumericPattern.IsMatch(str)  ) &&
                   (!RequireLowercase | LowercasePattern.IsMatch(str)) &&
                   (!RequireUppercase | UppercasePattern.IsMatch(str)) &&
                   (!RequireNumeric | NumericPattern.IsMatch(str));
        }
    }
}
