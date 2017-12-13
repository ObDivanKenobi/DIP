using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeSmells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CodeSmells.Models.Tests
{
    [TestClass()]
    public class ValidatorTests
    {
        [TestMethod()]
        public void IsValidTestDefaultOptions()
        {
            Validator defaultOptions = new Validator();
            (string password, bool expected)[] testData =
                {
                    ("", false),
                    ("     ", false),
                    ("Non_Alphanumeric$%^", false),
                    ("TenSymbols", true),
                    ("nouppercase", true),
                    ("NOLOWERVASE", true),
                    ("NoAlphanumeric", true)
                };

            foreach (var entry in testData)
            {
                bool actualResult = defaultOptions.IsValid(entry.password);
                if (entry.expected != actualResult)
                {
                    Debug.WriteLine($"Failed at \"{entry.password}\" with expected {entry.expected} and actual {actualResult};");
                    Assert.IsFalse(true);
                }
            }
        }

        [TestMethod()]
        public void IsValidRequireLength_TooShort_Fail()
        {
            int requiredLength = 10;
            Validator requireLength = new Validator { RequireLength = true, RequiredLength = requiredLength };

            StringBuilder tooShort = new StringBuilder();
            tooShort.Append('a', requiredLength - 1);

            Assert.IsFalse(requireLength.IsValid(tooShort.ToString()));
        }

        [TestMethod()]
        public void IsValidRequireLength_Enough_Success()
        {
            int requiredLength = 10;
            Validator requireLength = new Validator { RequireLength = true, RequiredLength = requiredLength };

            StringBuilder tooShort = new StringBuilder();
            tooShort.Append('a', requiredLength);

            Assert.IsTrue(requireLength.IsValid(tooShort.ToString()));
        }

        [TestMethod()]
        public void IsValidRequireNonAlphanumeric_AlphanumericOnly_Fail()
        {
            Validator requireNonAlphanumeric = new Validator { AllowNonAlphanumeric = true, RequireNonAlphanumeric = true };
            string testPassword = "Alpanumeric111";

            Assert.IsFalse(requireNonAlphanumeric.IsValid(testPassword));

        }

        [TestMethod()]
        public void IsValidRequireNonAlphanumeric_ContainsNonAlphanumeric_Success()
        {
            Validator requireNonAlphanumeric = new Validator { AllowNonAlphanumeric = true, RequireNonAlphanumeric = true };
            string testPassword = "Non_Alpanumeric@111";

            Assert.IsTrue(requireNonAlphanumeric.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidAllowNonAlphanumeric_AlphanumericOnly_Success()
        {
            Validator allowNonAlphanumeric = new Validator { AllowNonAlphanumeric = true };
            string testPassword = "Alpanumeric111";

            Assert.IsTrue(allowNonAlphanumeric.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidAllowNonAlphanumeric_ContainsNonAlphanumeric_Success()
        {
            Validator allowNonAlphanumeric = new Validator { AllowNonAlphanumeric = true };
            string testPassword = "Non_Alpanumeric@111";

            Assert.IsTrue(allowNonAlphanumeric.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidNotAllowAlphanumericButRequire_AlphanumericOnly_Fail()
        {
            Validator notAllowAlphanumericButRequire = new Validator { AllowNonAlphanumeric = false, RequireNonAlphanumeric = true };
            string testPassword = "Alpanumeric111";

            Assert.IsFalse(notAllowAlphanumericButRequire.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidNotAllowAlphanumericButRequire_ContainsNonAlphanumeric_Fail()
        {
            Validator notAllowAlphanumericButRequire = new Validator { AllowNonAlphanumeric = false, RequireNonAlphanumeric = true };
            string testPassword = "Non_Alpanumeric@111";

            Assert.IsFalse(notAllowAlphanumericButRequire.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidRequireLowercase_NoLowercase_Fail()
        {
            Validator requireLowercase = new Validator { RequireLowercase = true };
            string testPassword = "UPPERCASEONLY";

            Assert.IsFalse(requireLowercase.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidRequireLowercase_ContainsLowercase_Success()
        {
            Validator requireLowercase = new Validator { RequireLowercase = true };
            string testPassword = "BothLowerAndUppercase";

            Assert.IsTrue(requireLowercase.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidRequireUppercase_NoUppercase_Fail()
        {
            Validator requireUppercase = new Validator { RequireUppercase = true };
            string testPassword = "lowercaseonly";

            Assert.IsFalse(requireUppercase.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidRequireUppercase_ContainsUppercase_Success()
        {
            Validator requireUppercase = new Validator { RequireUppercase = true };
            string testPassword = "BothLowerAndUppercase";

            Assert.IsTrue(requireUppercase.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidRequireNumeric_NoNumeric_Fail()
        {
            Validator requireNumeric = new Validator { RequireNumeric = true };
            string testPassword = "NoNumericSymbols";

            Assert.IsFalse(requireNumeric.IsValid(testPassword));
        }

        [TestMethod()]
        public void IsValidRequireNumeric_ContainsNumeric_Success()
        {
            Validator requireNumeric = new Validator { RequireNumeric = true };
            string testPassword = "123numericSymbols456";

            Assert.IsTrue(requireNumeric.IsValid(testPassword));
        }
    }
}