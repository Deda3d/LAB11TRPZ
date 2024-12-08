using LAB11TRPZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB11TRPZ.Controllers
{
    public class PrinterController : Controller
    {
        private readonly Lab11DBContext _context;

        public PrinterController(Lab11DBContext context)
        {
            _context = context;
        }

        // Метод для отображения списка принтеров
        public async Task<IActionResult> Index(string manufacturer = null)
        {
            try
            {
                // Фильтруем только лазерные принтеры определенной марки
                var printersQuery = _context.Printers
                    .Include(p => p.PrinterModel)
                    .Include(p => p.PrinterType)
                    .Where(p => p.PrinterType.TypeName == "Laser");

                if (!string.IsNullOrEmpty(manufacturer))
                {
                    printersQuery = printersQuery.Where(p => p.PrinterModel.Manufacturer == manufacturer);
                }

                var printers = await printersQuery.ToListAsync();
                return View(printers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке принтеров: {ex.Message}");
                return View(new List<Printer>());
            }
        }
    }
}
