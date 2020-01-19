using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class PhoneNumberTests
    {
        [Fact]
        public void PhoneNumberGenerator_ShoulBePhoneGenerate()
        {
            var generator = new PhoneGenerator(format: "+#(###)-###-##-##");
            var phone = generator.Get();

            var regex = new Regex(@"\+\d\(\d{3}\)-\d{3}-\d{2}-\d{2}");

            bool success = regex.IsMatch(phone);

            Assert.True(success);
        }
    }
}
