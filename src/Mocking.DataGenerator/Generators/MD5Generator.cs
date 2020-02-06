using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class MD5Generator : RandomizerBase, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            var alg = MD5.Create();

            var hashByte = alg.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));

            return string.Concat(hashByte.Select(x => x.ToString("X2")));
        }
    }
}
