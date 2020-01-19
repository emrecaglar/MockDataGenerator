using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Text;
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

            Assert.NotNull(phone);
        }
    }
}
