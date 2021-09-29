using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.CategoryAgg;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoriesController(ICategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public async Task<IActionResult> Index() => View(await _categoryQuery.GetAll());
        
    }
}