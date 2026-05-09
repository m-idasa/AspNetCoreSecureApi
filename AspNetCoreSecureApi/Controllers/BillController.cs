using AspNetCoreSecureApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace AspNetCoreSecureApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : Controller
{
    private readonly AppDbContext _dbContext;

    public BillController(ILogger<TestController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    public ActionResult TotalCost()
    {
        IEnumerable<BillDto> result = (IEnumerable<BillDto>)(from tot in (from sh in _dbContext.Shoppings 
                                            join it in _dbContext.Items on sh.ItemId equals it.Id select new { sh.Number, sh.ItemId, sh.FactorId, it.Price })
                                 
                     group tot by tot.FactorId into g
                      select new BillDto( g.Key, g.Sum(g => g.Number * g.Price) ));



        return View(result);
    }

}
