using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MockDataGenerator
{
    public class MockDataGenerator<TModel>
    {
        private readonly Dictionary<LambdaExpression, Func<object>> _expressions = new Dictionary<LambdaExpression, Func<object>>();

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Func<TProperty> valueResolver)
        {
            _expressions.Add(expression, () => valueResolver());

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            _expressions.Add(expression, () => value);

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, IDataGenerator<TProperty> randomData)
        {
            _expressions.Add(expression, () => randomData.Get());

            return this;
        }

        public List<TModel> Generate(int count = 50)
        {
            var data = new List<TModel>();

            for (int i = 0; i < count; i++)
            {
                var model = Activator.CreateInstance<TModel>();

                foreach (var expression in _expressions)
                {
                    var memberExpression = (MemberExpression)expression.Key.Body;

                    var pi = (PropertyInfo)memberExpression.Member;

                    pi.SetValue(model, expression.Value(), new object[] { });
                }

                data.Add(model);
            }

            return data;
        }
    }
}
