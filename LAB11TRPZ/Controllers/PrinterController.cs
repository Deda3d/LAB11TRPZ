using LAB11TRPZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB11TRPZ.Controllers;

public class PrintersController : Controller
{
    private readonly Lab11DBContext _context;

    public PrintersController(Lab11DBContext context)
    {
        _context = context;
    }

    // Вивести лазерні принтери однієї марки
    public async Task<IActionResult> LaserPrintersByBrand(string brand)
    {
        if (string.IsNullOrEmpty(brand))
        {
            return BadRequest("Марка не вказана.");
        }

        // Фільтрація лазерних принтерів певної марки
        var printers = await _context.Printers
            .Include(p => p.PrinterModel)
            .Include(p => p.PrinterType)
            .Where(p => p.PrinterType.TypeName == "Laser" && p.PrinterModel.Manufacturer == brand)
            .ToListAsync();

        return View(printers);
    }
}
