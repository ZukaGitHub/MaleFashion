using WebApplicationCrud.Models;
using WebApplicationCrud.Models.BlogModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationCrud.Models.Identity;
using WebApplicationCrud.Models.AdministrationModels;
using WebApplicationCrud.Models.ShoppingCartModels;

namespace WebApplicationCrud.Data.DbContext
{
    public class CRUDdbcontext : IdentityDbContext<ApplicationUser>
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
        public DbSet<StockOnHold> StockOnHolds { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<TextSize> TextSizes { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<FavouriteProduct> FavouriteProducts { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<DeliveryInfo> DeliveryInfos { get; set; }
        public DbSet<ProductInfoStockAndSize> ProductInfoStockAndSize { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(



                new Category { Id = 1, Name = "Shoes" },
                new Category { Id = 2, Name = "T-Shirts" },
                new Category { Id = 3, Name = "Jackets" },
                new Category { Id = 4, Name = "Trousers" },
                new Category { Id = 5, Name = "Accessories" },
                new Category { Id = 6, Name = "Kids" },
                new Category { Id = 7, Name = "Other" }


            );
            builder.Entity<Brand>().HasData(

                new Brand { Id = 1, Name = "Hermes" },
                new Brand { Id = 2, Name = "Prada" },
                new Brand { Id = 3, Name = "Chanel" },
                new Brand { Id = 5, Name = "Gucci" },
                new Brand { Id = 6, Name = "Armani" },
                new Brand { Id = 7, Name = "Other" }




                );
            builder.Entity<TextSize>().HasData
                (
                new TextSize { Id = 1, Name = "XS" },
                new TextSize { Id = 2, Name = "S" },
                new TextSize { Id = 3, Name = "M" },
                new TextSize { Id = 4, Name = "XL" },
                new TextSize { Id = 5, Name = "2XL" },
                new TextSize { Id = 6, Name = "XXL" },
                new TextSize { Id = 7, Name = "3XL" },
                new TextSize { Id = 8, Name = "4XL" }



                );
            builder.Entity<OrderDetails>().
                HasOne(s => s.ProductInfo).WithMany(s=>s.OrderDetails)   
                .HasForeignKey(s=>s.ProductInfoId)
                .OnDelete(DeleteBehavior.NoAction);
           
            base.OnModelCreating(builder);
        }


    }
}
