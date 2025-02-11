using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gg_test.Data;
using gg_test.Models;

namespace gg_test.Controllers
{
    [ApiController]
    [Route("api/kpi-summary")]
    public class KPISummaryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KPISummaryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetKPISummary()
        {
            var kpiSummary = await _dbContext.Companies
                .Select(c => new
                {
                    CompanyName = c.Name,
                    AverageMonthlyRevenue = _dbContext.KPIs.Where(k => k.CompanyId == c.Id).Average(k => k.MonthlyRevenue),
                    AverageNetProfit = _dbContext.KPIs.Where(k => k.CompanyId == c.Id).Average(k => k.NetProfit),
                    AverageProfitMargin = _dbContext.KPIs.Where(k => k.CompanyId == c.Id).Average(k => k.ProfitMargin)
                })
                .ToListAsync();

            return Ok(kpiSummary);
        }
    }
}