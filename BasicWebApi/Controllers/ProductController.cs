using BasicWebApi.Data;
using BasicWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProductController : ControllerBase
    {
        /*
         * Injeção do contexto agora acessivel via [FromServices]
         * 
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }
        */

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).ToListAsync(); // Include() linka as chaves
            return products;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            // var products = await _context.Products.SingleOrDefault(x => x.Id.Equals(id)); // Ou...
            var product = await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpGet("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(x => x.Category).Where(x => x.CategoryId == id).ToListAsync();
            return products;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product product)
        {
            if (ModelState.IsValid) // Validar se os dados passados estão de acordo com as restrições do model
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<Product> Put([FromServices] DataContext context, [FromBody] Product product)
        {
            if (product != null)
            {
                context.Entry(product).State = EntityState.Modified; // Para alterar o estado da entidade para modified
                context.SaveChanges();
                return product;
            }
            else
            {
                return NotFound($"O produto com id = {product.Id} não foi encontrado.");
            }

        }


        [HttpDelete("{id:int}")]
        public ActionResult<Product> Delete([FromServices] DataContext context, int id)
        {
            //var product = context.Products.Find(id);
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"O produto com id = {product.Id} não foi encontrado.");
            }
            else
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return product;
            }
        }
    }
}
