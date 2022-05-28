using Code_Snippets.Code_Snippets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Patterns.Patterns
{
    public class concreteClass : Object
    {
        string Name { get; set; }
        Guid dataId { get; set; }
        byte[] data { get; set; }
        byte[] dataNotStored { get; set; }

        public class SpecificContext : DbContext
        {
            public DbSet<concreteClass> objects { get; set; }


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                //    optionsBuilder
                //        .UseLazyLoadingProxies
                //        .UseSqlite("Data Source=orders.db");
                //
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //If the data isn't to be loaded by entity framework on init, then ignore it.
                //Instead, on access, fetching that data is more appropriate, using a proxy.
                modelBuilder.Entity<concreteClass>()
                    .Ignore(c => c.dataNotStored);

                base.OnModelCreating(modelBuilder);
            }
        }

        public class concreteClassProxy : concreteClass
        {
            public byte[] Load
            {
                get
                {
                    if (base.data == null)
                    {
                        base.data = dataService.GetFor(Name);
                    }

                    return base.data;
                }
            }
        }
        internal class dataService
        {
            public static byte[] GetFor(string lookup)
            {
                var data = new byte[1024];
                return data;
            }

            public static concreteClass[][] All()
            {
                concreteClass[][] data = new concreteClass[5][];

                data[0] = new concreteClass[5];
                data[1] = new concreteClass[1024];

                return data;
            }

            public interface IRepository<T>
            {
                T Add(T entity);
                T Update(T entity);
                T Get(Guid id);
                IEnumerable<T> All();
                IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
                void SaveChanges();
            }

            public abstract class GenericRepository<T> : IRepository<T> where T : class
            {
                protected SpecificContext context;

                public GenericRepository(SpecificContext context)
                {
                    this.context = context;
                }

                public virtual T Add(T entity)
                {
                    return context
                        .Add(entity)
                        .Entity;
                }

                public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
                {
                    return context.Set<T>()
                        .AsQueryable()
                        .Where(predicate).ToList();
                }

                public virtual T Get(Guid id)
                {
                    return context.Find<T>(id);
                }

                public virtual IEnumerable<T> All()
                {
                    return context.Set<T>()
                        .AsQueryable()
                        .ToList();
                }

                public virtual T Update(T entity)
                {
                    return context.Update(entity)
                        .Entity;
                }

                public void SaveChanges()
                {
                    context.SaveChanges();
                }
            }


            public class specificRepository : GenericRepository<concreteClass>
            {
                public specificRepository(SpecificContext context) : base(context)
                {
                }

                public override concreteClass Get(Guid id)
                {
                    var dataId = context.objects
                        .Where(c => c.dataId == id)
                        .Select(c => c.dataId)
                        .Single();

                    return context.objects.Single(c => c.dataId == id);
                }

                public override concreteClass Update(concreteClass entity)
                {
                    var data = context.objects
                        .Single(c => c.dataId == entity.dataId);

                    data.Name = entity.Name;


                    return base.Update(data);
                }
            }
        }
    }
}

