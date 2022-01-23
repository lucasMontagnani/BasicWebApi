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
    public class CategoryController : ControllerBase
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
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetById([FromServices] DataContext context, int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category category)
        {
            if (ModelState.IsValid) // Validar se os dados passados estão de acordo com as restrições do model
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
                return category;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
        [HttpPut]
        public ActionResult<Category> Put([FromServices] DataContext context, [FromBody] Category category)
        {
            if(category != null)
            {
                context.Entry(category).State = EntityState.Modified; // Para alterar o estado da entidade para modified
                context.SaveChanges();
                return category;
            }
            else
            {
                return NotFound($"A categoria com id = {category.Id} não foi encontrada.");
            }
            
        }
        

        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete([FromServices] DataContext context, int id)
        {
            //var category = context.Categories.Find(id);
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"A categoria com id = {id} não foi encontrada.");
            }
            else
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return category;
            }
        }
    }
}
