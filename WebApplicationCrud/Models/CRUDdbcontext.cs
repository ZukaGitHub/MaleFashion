using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationCrud.Models
{
    public class CRUDdbcontext : IdentityDbContext
    {
        public CRUDdbcontext(DbContextOptions<CRUDdbcontext> options)
            : base(options)
        {

        }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<TextSize> TextSizes { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(



                new Category { id = 1, name = "Shoes" },
                new Category { id = 2, name = "T-Shirts" },
                new Category { id = 3, name = "Jackets" },
                new Category { id = 4, name = "Trousers" },
                new Category { id = 5, name = "Accessories" },
                new Category { id = 6, name = "Kids" },
                new Category { id = 7, name = "Other" }


            );
            builder.Entity<Brand>().HasData(

                new Brand { id = 1, name = "Hermes" },
                new Brand { id = 2, name = "Prada" },
                new Brand { id = 3, name = "Chanel" },
                new Brand { id = 5, name = "Gucci" },
                new Brand { id = 6, name = "Armani" },
                new Brand { id = 7, name = "Other" }




                );
            builder.Entity<TextSize>().HasData
                (
                new TextSize { id = 1, name = "XS" },
                new TextSize { id = 2, name = "S" },
                new TextSize { id = 3, name = "M" },
                new TextSize { id = 4, name = "XL" },
                new TextSize { id = 5, name = "2XL" },
                new TextSize { id = 6, name = "XXL" },
                new TextSize { id = 7, name = "3XL" },
                new TextSize { id = 8, name = "4XL" }



                );
            base.OnModelCreating(builder);
        }


    }
}
