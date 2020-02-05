using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class LanguageGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string[] languages = new string[]
        {
            "English", "French", "Arabic", "Spanish", "Portuguese", "Russian", "German",
            "Fula", "Italian", "Malay", "Manding", "Northern Sami", "Swahili", "Danish",
            "Dutch", "Gbe", "Mandarin Chinese", "Tamil", "Persian", "Romanian", "Serbian",
            "Somali", "Soninke", "Tswana", "Turkish", "Bengali", "Armenian", "Aymara & Quechua",
            "Berber", "Catalan", "Chichewa", "Croatian", "Greek", "Hausa", "Hindi", "Korean",
            "Lingala", "Nepali", "Samoan",
            "Slovak", "Songhay-Zarma", "Sotho", "Swati", "Swedish", "Tamasheq", "Tigrinya", "Ukrainian",
            "Venda", "Wolof", "Xhosa", "Guarani", "Bulgarian", "Finnish", "Norwegian", "Icelandic"
        };

        public string Get()
        {
            return languages[Randomizer.Next(0, languages.Length)];
        }
    }
}
