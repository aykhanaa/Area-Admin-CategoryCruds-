using Fiorello_PB101.Data;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Design;

namespace Fiorello_PB101.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CategoryProductVM>> GetAllWithProductCountAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.Include(m=>m.Products)
                                                                        .ToListAsync();
            return categories.Select(m => new CategoryProductVM
            {
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                ProductCount = m.Products.Count
            });
        }
    }
}
