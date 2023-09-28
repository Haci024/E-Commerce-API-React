using Data.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var data = await _context.Products.ToListAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            if (id == null)
            {

                return Ok("This product not found in Database");

            }
            var data = _context.Products.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {

                return NotFound("This product not found in Database");
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bütün dataları doldurun");
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dbproduct = _context.Products.FirstOrDefault(x => x.Id == id);
            bool isExist=_context.Products.Any(x=>x.Name==product.Name);
            if (isExist)
            {
                return Ok("This Name already isExist");
            }
            if (dbproduct == null)
            {

                return NotFound();
            }
            dbproduct.Price = product.Price;
            dbproduct.Name = product.Name;
            dbproduct.IsDeactive = product.IsDeactive;
            _context.Products.Update(dbproduct);
            _context.SaveChanges();
            return Ok("Product updated succesfully");
        }
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound("This product not found in database");
            }
            var data =await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {

                return Ok("This product not found in database!");
            }
            _context.Products.Remove(data);
            _context.SaveChanges();
            return Ok("Data deleted successfully!");

        }

       
       }//https://localhost:44337/api/Program
     



    
}