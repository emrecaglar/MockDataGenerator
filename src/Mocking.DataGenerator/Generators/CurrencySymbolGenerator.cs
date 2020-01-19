using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    //https://www.xe.com/symbols.php
    public class CurrencySymbolGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string[] _symbols = new[] 
        {
            "₺",
            "€",
            "$",
            "﷼",
            "¥",
            "£",
            "RM",
            "CHF"
        };
        public string Get()
        {
            return _symbols[Randomizer.Next(0, _symbols.Length - 1)];
        }
    }
}
