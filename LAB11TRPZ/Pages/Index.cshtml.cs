using LAB11TRPZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAB11TRPZ.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Lab11DBContext _context;

        // Конструктор для внедрения зависимости контекста базы данных
        public IndexModel(Lab11DBContext context)
        {
            _context = context;
        }

        // Свойство для хранения списка принтеров
        public IList<Printer> Printers { get; set; } = default!;

        // Метод для асинхронной загрузки данных принтеров из базы данных
        public async Task OnGetAsync()
        {
            try
            {
                // Загружаем все принтеры из базы данных с включением связанных данных
                Printers = await _context.Printers
                    .Include(p => p.PrinterModel) // Включаем модель принтера
                    .Include(p => p.PrinterType) // Включаем тип принтера
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Обрабатываем исключение, если что-то пошло не так
                // Например, можно логировать ошибку или показывать сообщение пользователю
                Console.WriteLine($"Ошибка при загрузке принтеров: {ex.Message}");
            }
        }
    }
}
