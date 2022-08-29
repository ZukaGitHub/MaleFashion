﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.Migrations
{
    [DbContext(typeof(CRUDdbcontext))]
    [Migration("20211026191646_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Brand", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Brands");

                    b.HasData(
                        new { id = 1, name = "Hermes" },
                        new { id = 2, name = "Prada" },
                        new { id = 3, name = "Chanel" },
                        new { id = 5, name = "Hermes" },
                        new { id = 6, name = "Armani" },
                        new { id = 7, name = "Other" }
                    );
                });

            modelBuilder.Entity("WebApplicationCrud.Models.CardInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CCV");

                    b.Property<string>("CardNum");

                    b.Property<string>("ExpDate");

                    b.Property<string>("NameOnCard");

                    b.Property<int?>("PaymentInfoid");

                    b.HasKey("id");

                    b.HasIndex("PaymentInfoid");

                    b.ToTable("CardInfo");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Categories");

                    b.HasData(
                        new { id = 1, name = "Shoes" },
                        new { id = 2, name = "T-Shirts" },
                        new { id = 3, name = "Jackets" },
                        new { id = 4, name = "Trousers" },
                        new { id = 5, name = "Accessories" },
                        new { id = 6, name = "Kids" },
                        new { id = 7, name = "Other" }
                    );
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Form", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Image", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imagename");

                    b.Property<int?>("ProductInfoid");

                    b.Property<int>("productid");

                    b.HasKey("id");

                    b.HasIndex("ProductInfoid");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderStatus");

                    b.Property<float>("OrderTotal");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.OrderDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("Size");

                    b.Property<string>("SizeText");

                    b.Property<string>("color");

                    b.HasKey("id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.OtherMethod", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PaymentInfoid");

                    b.HasKey("id");

                    b.HasIndex("PaymentInfoid");

                    b.ToTable("OtherMethod");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.PaymentInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserInfoId");

                    b.HasKey("id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("PaymentInfos");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName");

                    b.Property<string>("CategoryName");

                    b.Property<float?>("NewPrice");

                    b.Property<string>("OwnerId");

                    b.Property<int?>("SalePercentage");

                    b.Property<int?>("brandid");

                    b.Property<string>("desc");

                    b.Property<string>("name");

                    b.Property<float>("price");

                    b.Property<int>("stock");

                    b.HasKey("id");

                    b.HasIndex("brandid");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.ProductInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("Size");

                    b.Property<string>("SizeText");

                    b.Property<string>("color");

                    b.HasKey("id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInfos");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int?>("ProductInfoid");

                    b.Property<int?>("Productid");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("ProductInfoid");

                    b.HasIndex("Productid");

                    b.ToTable("shoppingCartItems");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<string>("TagName");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.TextSize", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("TextSizes");

                    b.HasData(
                        new { id = 1, name = "XS" },
                        new { id = 2, name = "S" },
                        new { id = 3, name = "M" },
                        new { id = 4, name = "XL" },
                        new { id = 5, name = "2XL" },
                        new { id = 6, name = "XXL" },
                        new { id = 7, name = "3XL" },
                        new { id = 8, name = "4XL" }
                    );
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Thumbnail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int?>("ProductInfoid");

                    b.Property<string>("ThumbnailName");

                    b.HasKey("id");

                    b.HasIndex("ProductInfoid");

                    b.ToTable("Thumbnails");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNum");

                    b.Property<string>("address1");

                    b.HasKey("Id");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplicationCrud.Models.CardInfo", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.PaymentInfo")
                        .WithMany("CardInfo")
                        .HasForeignKey("PaymentInfoid");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Image", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.ProductInfo")
                        .WithMany("Images")
                        .HasForeignKey("ProductInfoid");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.OrderDetails", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplicationCrud.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplicationCrud.Models.OtherMethod", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.PaymentInfo")
                        .WithMany("otherMethods")
                        .HasForeignKey("PaymentInfoid");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.PaymentInfo", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.UserInfo")
                        .WithMany("PaymentInfo")
                        .HasForeignKey("UserInfoId");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Product", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.Brand", "brand")
                        .WithMany()
                        .HasForeignKey("brandid");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.ProductInfo", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.Product")
                        .WithMany("ProductInfos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplicationCrud.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.ProductInfo", "ProductInfo")
                        .WithMany()
                        .HasForeignKey("ProductInfoid");

                    b.HasOne("WebApplicationCrud.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Productid");
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Tag", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.Product", "Product")
                        .WithMany("Tags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplicationCrud.Models.Thumbnail", b =>
                {
                    b.HasOne("WebApplicationCrud.Models.ProductInfo")
                        .WithMany("Thumbnails")
                        .HasForeignKey("ProductInfoid");
                });
#pragma warning restore 612, 618
        }
    }
}
