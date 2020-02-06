using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class MockGeneratorTests
    {
        [Fact]
        public void Test()
        {
            var productGenerator = new MockDataGenerator<Product>()
                .Register(x => x.Name, x => x.Name())
                .Register(x => x.Explanation, x => x.LoremIpsum())
                .Register(x => x.Category, x=>x.Object(
                    new MockDataGenerator<Category>()
                        .Register(cat=>cat.Id, cat=>cat.AutoIncrement())
                        .Register(cat=>cat.Name, cat => cat.Random())
                 ))
                .Register(x => x.CategoryId, model => model.Category.Id)
                .Register(x => x.Comments, x => x.List())
                .Register(x => x.Price, x => x.Money())
                .Register(x => x.Unit, x => x.Random(5, 15))
                .Register(x => x.Amount, (model) => model.Price * model.Unit)
                .Register(x => x.Id, x => x.Guid())
                .Register(x => x.Barcodes, x => x.Array())
                .Register(x => x.CurrencySymbol, x => x.CurrencySymbol())
                .Register(x => x.Hash, x => x.MD5())
                .Register(x => x.Size, x => x.FromEnum())
                .Register(x => x.Sales, x => x.List(
                      new MockDataGenerator<Sales>()
                              .Register(sale => sale.CardNumber, sale => sale.CreditCard())
                              .Register(sale => sale.Iban, sale => sale.IBAN())
                              .Register(sale => sale.SaleDate, sale => sale.Random())
                              .Register(sale => sale.Customer, sale => sale.Object(
                                  new MockDataGenerator<Customer>()
                                        .Register(cust => cust.Email, cust => cust.Email())
                                        .Register(cust => cust.Gender, cust => cust.Gender())
                                        .Register(cust => cust.Id, cust => cust.Guid())
                                        .Register(cust => cust.Name, cust => cust.Name())
                                        .Register(cust => cust.PersonalPage, cust => cust.Url(true))
                                        .Register(cust => cust.Phone, cust => cust.Phone())
                                        .Register(cust => cust.Region, cust => cust.Region())
                                        .Register(cust => cust.Surname, cust => cust.Surname())
                              ))
                      ));


            var data = productGenerator.Generate(5);
        }
    }

    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string PersonalPage { get; set; }

        public string Region { get; set; }

        public string Gender { get; set; }
    }

    public class Sales
    {
        public DateTime SaleDate { get; set; }

        public Customer Customer { get; set; }

        public string Iban { get; set; }

        public string CardNumber { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Explanation { get; set; }

        public decimal Price { get; set; }

        public int Unit { get; set; }

        public decimal Amount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string[] Barcodes { get; set; }

        public List<Sales> Sales { get; set; }

        public List<Guid> Comments { get; set; }

        public string Hash { get; set; }

        public string CurrencySymbol { get; set; }

        public SizeCategory Size { get; set; }
    }

    public enum SizeCategory
    {
        Small,
        Medium,
        Large
    }
}
