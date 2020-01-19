using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    //https://www.xe.com/symbols.php
    public class CurrencyCodeGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string[] _codes = new[] 
        {
            "TRY",
            "EUR",
            "USD",
            "SAR",
            "JPY",
            "GBP",
            "CNY",
            "MYR",
            "CHF"

        };
        public string Get()
        {
            return _codes[Randomizer.Next(0, _codes.Length - 1)];
        }
    }
}
