using EstateAgency.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealEstateApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RealEstateCustomGetController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet("average-prices/{year}")]
        public async Task<IActionResult> GetRealtorAveragePrices(int year)
        {
            var averagePrices = await (from sale in _context.Sales
                                       join realtor in _context.Realtors on sale.RealtorCode equals realtor.RealtorCode
                                       join realEstate in _context.RealEstates on sale.EstateCode equals realEstate.EstateCode
                                       where sale.SaleDate.Year == year && realEstate.EstateTypeCode == 1
                                       group sale by new
                                       {
                                           realtor.RealtorCode,
                                           realtor.FirstName,
                                           realtor.LastName,
                                           realtor.SurName
                                       } into realtorGroup
                                       select new
                                       {
                                           FIO = realtorGroup.Key.FirstName + " " + realtorGroup.Key.LastName + " " + realtorGroup.Key.SurName,
                                           AveragePrice = realtorGroup.Average(s => s.Price)
                                       }).ToListAsync();

            return Ok(averagePrices);
        }

        [HttpGet("realtor_less/{count}")]
        public async Task<IActionResult> GetRealtorsWithFewerSales(int count)
        {
            var result = await (from realtor in _context.Realtors
                                join sale in _context.Sales
                                    on realtor.RealtorCode equals sale.RealtorCode into salesGroup
                                from sales in salesGroup.DefaultIfEmpty()
                                group sales by new
                                {
                                    realtor.RealtorCode,
                                    realtor.FirstName,
                                    realtor.LastName,
                                    realtor.SurName
                                } into groupedData
                                where groupedData.Count(sale => sale != null) < count
                                select new
                                {
                                    FIO = $"{groupedData.Key.FirstName} {groupedData.Key.LastName} {groupedData.Key.SurName}",
                                    SalesCount = groupedData.Count(sale => sale != null)
                                }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("price_per_sqm_less_avg_district")]
        public async Task<IActionResult> GetRealEstateBelowAvgPricePerDistrict()
        {
            var averagePrices = await (from realEstate in _context.RealEstates
                                       group realEstate by realEstate.DistrictCode into districtGroup
                                       select new
                                       {
                                           DistrictCode = districtGroup.Key,
                                           AveragePricePerSqm = districtGroup.Average(r => r.Price / r.Area)
                                       }).ToListAsync();


            var properties = _context.RealEstates
                                .AsEnumerable() 
                                .Where(re => averagePrices.Any(avg => avg.DistrictCode == re.DistrictCode &&
                                                                      (re.Price / re.Area) < avg.AveragePricePerSqm))
                                .Select(re => new
                                {
                                    re.DistrictCode,
                                    re.Address,
                                    PricePerSqm = re.Price / re.Area,
                                    averagePrices.First(avg => avg.DistrictCode == re.DistrictCode).AveragePricePerSqm
                                })
                                .ToList();
            return Ok(properties);
        }



        [HttpGet("realtor_without_sales")]
        public async Task<IActionResult> GetRealtorsNoSalesThisYear()
        {
            var currentYear = DateTime.Now.Year;

            var allRealtors = await _context.Realtors.ToListAsync();

            var realtorsWithSales = await (from realtor in _context.Realtors
                                join sale in _context.Sales
                                on realtor.RealtorCode equals sale.RealtorCode into salesGroup
                                from sale in salesGroup.DefaultIfEmpty()
                                where sale.SaleDate.Year == currentYear
                                select realtor)
                                .Include(r => r.Sales)
                                .Distinct()
                                .ToListAsync();

            // Теперь получаем всех риэлторов, исключая тех, кто имеет продажи в текущем году
            var realtorsWithoutSales = allRealtors.Except(realtorsWithSales).ToList();

            return Ok(realtorsWithoutSales);
        }

        [HttpGet("estates_between/{start}/{end}")]
        public async Task<IActionResult> GetYearsWith2To3Properties(int start, int end)
        {
            var result = await (from realEstate in _context.RealEstates
                                group realEstate by realEstate.AdvertisementDate.Year into g
                                where g.Count() >= start && g.Count() <= end
                                select g.Key) // Получаем только год
                                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("most_expensive_estates")]
        public async Task<IActionResult> GetMostExpensivePropertiesByRegion()
        {
            var result = await (from realEstate in _context.RealEstates
                                join district in _context.Districts on realEstate.DistrictCode equals district.DistrictCode into districtGroup
                                from district in districtGroup.DefaultIfEmpty()
                                select new
                                {
                                    realEstate.Price,
                                    realEstate.District,
                                    realEstate.Address,
                                    district.DistrictName 
                                })
            .GroupBy(r => r.District) 
            .Select(g => new
            {
                District = g.Key,
                MostExpensiveProperty = g.OrderByDescending(r => r.Price).FirstOrDefault()
            })
            .ToListAsync();

            return Ok(result);
        }
    
    }
}
