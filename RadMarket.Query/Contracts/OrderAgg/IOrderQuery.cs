using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.OrderAgg
{
    public interface IOrderQuery
    {
        Task<OrderQueryVM> GetBy(long userId);
    }
}