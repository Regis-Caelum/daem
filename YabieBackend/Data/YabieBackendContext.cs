using Microsoft.EntityFrameworkCore;
using YabieBackend.Models;

namespace YabieBackend.Data
{
    public class YabieBackendContext : DbContext
    {
        public YabieBackendContext(DbContextOptions<YabieBackendContext> options)
            : base(options)
        {
        }

        public DbSet<Auction> Auctions { get; set; } = default!;
        public DbSet<Bid> Bids { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<UserAuction> UserAuctions { get; set; } = default!;
    }
}