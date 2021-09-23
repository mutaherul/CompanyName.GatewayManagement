using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using CompanyName.GatewayManagement.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CompanyName.GatewayManagement.Domain.Tests.FakeDBWrapper
{
    public class FakeConcreteDbWrapper
    {
        public void AddDbset<TPersistent>(IEnumerable<TPersistent> _fakeResults, ref Mock<GatewayDbContext> context) where TPersistent : class
        {
            var results = _fakeResults.AsQueryable();
            var dbSet = new Mock<DbSet<TPersistent>>();

            dbSet.Setup(setup => setup.Find(It.IsAny<object[]>())).Returns((object[] input) => results.SingleOrDefault(x => GetPrimaryKeyValueOfEntity(x) == (long)input.First()));
            dbSet.As<IAsyncEnumerable<TPersistent>>()
                .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new TestAsyncEnumerator<TPersistent>(results.GetEnumerator()));

            dbSet.As<IQueryable<TPersistent>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<TPersistent>(results.Provider));
            dbSet.As<IQueryable<TPersistent>>().Setup(m => m.Expression).Returns(results.Expression);
            dbSet.As<IQueryable<TPersistent>>().Setup(m => m.ElementType).Returns(results.ElementType);
            dbSet.As<IQueryable<TPersistent>>().Setup(m => m.GetEnumerator()).Returns(results.GetEnumerator());

            context.Setup(c => c.Set<TPersistent>()).Returns(dbSet.Object);

            context.Setup(c => c.AddAsync(It.IsAny<TPersistent>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(EntityEntry<TPersistent>));
            context.Setup(c => c.Remove(It.IsAny<TPersistent>())).Returns(default(EntityEntry<TPersistent>));
            context.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<int>());
        }

        private long GetPrimaryKeyValueOfEntity<T>(T persistent) where T : class
        {
            PropertyInfo[] properties = persistent.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute))
                    as KeyAttribute;

                if (attribute != null) // This property has a KeyAttribute
                {
                    // Do something, to read from the property:
                    return (long)property.GetValue(persistent);
                }
            }

            return 0;
        }
    }
}
