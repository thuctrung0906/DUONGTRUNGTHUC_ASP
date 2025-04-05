using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUONGTRUNGTHUC_2122110158.Data;
using DUONGTRUNGTHUC_2122110158.Model;

namespace DUONGTRUNGTHUC_2122110158.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        //  Lấy tất cả sản phẩm kèm danh mục
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        //  Lấy sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                                                 .FirstOrDefaultAsync(p => p.ID == id);

            if (product == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });

            return product;
        }

        //  Thêm sản phẩm mới (đã sửa lỗi)
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null)
                return BadRequest(new { message = "CategoryID không tồn tại!" });

            // Gán lại Category để tránh lỗi tạo mới
            product.Category = category;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ID }, product);
        }

        //  Cập nhật sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ID)
                return BadRequest(new { message = "ID không hợp lệ!" });

            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null)
                return BadRequest(new { message = "CategoryID không tồn tại!" });

            // Gán lại Category
            product.Category = category;
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.ID == id))
                    return NotFound(new { message = "Sản phẩm không tồn tại!" });
                else
                    throw;
            }

            return NoContent();
        }

        //  Xoá sản phẩm
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Sản phẩm không tồn tại!" });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
