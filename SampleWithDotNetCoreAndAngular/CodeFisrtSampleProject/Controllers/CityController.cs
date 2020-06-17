using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeFisrtSampleProject.Data;
using CodeFisrtSampleProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CodeFisrtSampleProject.Controllers
{
    public class CityController : Controller
    {
        OnlineEducationDBContext _context;
        IMemoryCache _cache;
        string cityCacheKey = "CITY_CACHE_KEY_{0}";
        public CityController(OnlineEducationDBContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<IActionResult> GetCitiesWithProvinceId(int provinceId)
        {
            cityCacheKey = string.Format(cityCacheKey, provinceId);
            System.Threading.Thread.Sleep(5000);
            var result = new AjaxActionResult<List<City>>();
            result.IsSuccess = true;
            var cities = new List<City>();
            if (!_cache.TryGetValue(cityCacheKey, out cities))
            {
                result.Data = await _context.City.Where(q => q.ProvinceId == provinceId).ToListAsync();
                _cache.Set(cityCacheKey, result.Data);
            }
            else
            {
                result.Data = cities;
            }
            return Ok(result);
        }
        public async Task<IActionResult> Create(string cityName, int provinceId)
        {
            var city = new City
            {
                CityName = cityName,
                ProvinceId = provinceId
            };

            if (_context.City.Any(q => q.CityName == cityName && q.ProvinceId == provinceId))
            {
                return BadRequest("already exists");
            }
            await _context.AddAsync(city);
            await _context.SaveChangesAsync();

            //clear cache
            cityCacheKey = string.Format(cityCacheKey, provinceId);
            _cache.Remove(cityCacheKey);    
            return RedirectToAction(nameof(Index));
        
        }
    }
}
