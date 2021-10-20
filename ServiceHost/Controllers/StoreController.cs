using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.StoreAgg;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreQuery _storeQuery;

        public StoreController(IStoreQuery storeQuery)
        {
            _storeQuery = storeQuery;
        }

        [HttpGet("store{id}/{name}")]
        public async Task<IActionResult> Index(long id, string name)
        {
            return View(await _storeQuery.GetBy(id));
        }
    }
}
