using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class RegionGeneratorTests
    {
        [Fact]
        public void RegionGeneratorTest()
        {
            //var regionGenerator = new RegionGenerator();

            //string region = regionGenerator.Get();

            //Assert.NotNull(region);


            var dataGenerator = new MockDataGenerator<Member>()
                .Register(x => x.BirthDate, (model) => 
                {
                    return DateTime.Now;
                });
        }
    }

    public class Member
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }

        public string Phone { get; set; }

        public bool IsPhoneVerified { get; set; }

        public string Tckn { get; set; }

        public bool IsTcknVerified { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }


        /// <summary>
        /// 1-Aktif, 2-Pasif, 3-Blocked
        /// </summary>
        public MemberStatus Status { get; set; }
    }

    public enum MemberStatus
    {
        Active = 1,

        Passive = 2,

        Blocked = 3
    }
}
