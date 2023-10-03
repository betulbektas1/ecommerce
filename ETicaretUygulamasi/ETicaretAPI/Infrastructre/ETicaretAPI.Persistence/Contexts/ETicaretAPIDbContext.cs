using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Ürünler { get; set; }
        public DbSet<Order> Siparişler { get; set; }
        public DbSet<Customer> Müşteriler { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entitiyler üzerinden yapılam değişikliklerin yada yeni eklenen verinin yakalanmasını sağlayan propertydir. update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlıyor.

            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                var result = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
