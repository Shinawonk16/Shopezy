using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IApplicationDbContext
{
     public DbSet<User> User { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    // public DbSet<Rider> Riders { get; set; }
    // public DbSet<SaleRep> SaleReps { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Managers> Managers { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    // public DbSet<Cart> Carts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Sales> Sales { get; set; }
    // public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Verification> Verifications { get; set; }
    public DbSet<Request> Requests { get; set; }


}
