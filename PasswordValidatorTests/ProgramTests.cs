using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace PasswordValidatorTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void CountValidPasswords_WhenFileDoesNotExist_ShouldReturn0()
        {
            // Arrange
            var filePath = @"E:\my_project\cSharp\TEST\PasswordValidator\PasswordValidator\Files\passwords.txt";

            // Act
            var validPasswordsCount = CountValidPasswords(filePath);

            // Assert
            Assert.AreEqual(0, validPasswordsCount);
        }

        [Test]
        public void CountValidPasswords_WhenFileContainsOneValidPassword_ShouldReturn1()
        {
            // Arrange
            var filePath = @"E:\my_project\cSharp\TEST\PasswordValidator\PasswordValidator\Files\passwords.txt";

            // Act
            var validPasswordsCount = CountValidPasswords(filePath);

            // Assert
            Assert.AreEqual(1, validPasswordsCount);
        }

        [Test]
        public void CountValidPasswords_WhenFileContainsOneInvalidPassword_ShouldReturn0()
        {
            // Arrange
            var filePath = @"E:\my_project\cSharp\TEST\PasswordValidator\PasswordValidator\Files\passwords.txt";

            // Act
            var validPasswordsCount = CountValidPasswords(filePath);

            // Assert
            Assert.AreEqual(0, validPasswordsCount);
        }

        [Test]
        public void IsValidPassword_WhenRequirementIsValid_ShouldReturnTrue()
        {
            // Arrange
            var requirement = @"a 1-5";
            var password = @"abcdj";

            // Act
            var isValidPassword = IsValidPassword(requirement, password);

            // Assert
            Assert.IsTrue(isValidPassword);
        }

        [Test]
        public void IsValidPassword_WhenRequirementIsInvalid_ShouldReturnFalse()
        {
            // Arrange
            var requirement = @"z 2-4";
            var password = @"asfalseiruqwo";

            // Act
            var isValidPassword = IsValidPassword(requirement, password);

            // Assert
            Assert.IsFalse(isValidPassword);
        }

        private int CountValidPasswords(string filePath)
        {
            int validPasswordsCount = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(':');
                if (parts.Length != 2)
                    continue;

                string requirement = parts[0].Trim();
                string password = parts[1].Trim();

                if (IsValidPassword(requirement, password))
                    validPasswordsCount++;
            }

            return validPasswordsCount;
        }

        private bool IsValidPassword(string request, string password)
        {
            Match match = Regex.Match(request, @"(\w) (\d+)-(\d+)");
            if (!match.Success)
                return false;

            char requiredChar = match.Groups[1].Value[0];
            int minCount = int.Parse(match.Groups[2].Value);
            int maxCount = int.Parse(match.Groups[3].Value);

            int charCount = password.Split(requiredChar).Length - 1;

            return charCount >= minCount && charCount <= maxCount;
        }
    }
}