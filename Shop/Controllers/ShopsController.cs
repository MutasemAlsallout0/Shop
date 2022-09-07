using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

 
namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        readonly ShopContext _context;

        public ShopsController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dbItems = _context.Items.Select(x => new { x.ItemName, x.SubCategory.SubCategoryName, x.SubCategory.Category.CategortName }).ToList();

            return Ok(dbItems);
        }


        [HttpGet]
        [Route("FileCsv")]
        public IActionResult ExportFile()
        {
            var csvpath = Path.Combine(Environment.CurrentDirectory, $"All Categories.csv");
            using (var streamWriter = new StreamWriter(csvpath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    var data = _context.Items.Select(x => new { x.Id ,x.ItemName, x.SubCategory.SubCategoryName, x.SubCategory.Category.CategortName }).ToList();
                    csvWriter.WriteRecords(data);
                }
            }
             return Ok("Download Successfully");
        }

    }
}
