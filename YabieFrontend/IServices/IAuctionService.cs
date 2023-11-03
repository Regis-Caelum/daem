using YabieFrontend.Models;

namespace YabieFrontend.IServices;

public interface IAuctionService
{
    Task<IEnumerable<Auction>> ListAllAuctions();
    Task<Auction> GetAuction(string auctionId);
}