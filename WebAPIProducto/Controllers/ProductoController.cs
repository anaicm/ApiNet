using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebAPIProducto.data;
using WebAPIProducto.entidades;

namespace WebAPIProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        //para registrar los eventos 
        private readonly ILogger<ProductoController> _logger;
        //Contexto
        private readonly DataContext _context;
        //constructor
        public ProductoController(ILogger<ProductoController> logger,DataContext context ){
            _logger=logger;
            _context=context;

        }
        //creación de método getAll (Los devuelve todos los registros)
        [HttpGet(Name = "GetProductos")]
        //traer lo productos de tipo asincrona (async)
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() {
            var productos = await _context.Producto.ToListAsync();
            return Ok(productos);
        }
        //método get solo va a devolver un registro 
        [HttpGet("{id}",Name = "GetProducto")]
        
        public async Task<ActionResult<Producto>> GetProducto(int id) {
            var productos = await _context.Producto.FindAsync(id);//Este método encuentra el producto que coincide con el id
            if(productos==null){
                return NotFound();
            }
            return productos;
        
        }

         //método para añadir un registro en la tabla 
        [HttpPost]
        public async Task<ActionResult<Producto>> Post(Producto producto) {
            _context.Add(producto);
            await _context.SaveChangesAsync();
        return new CreatedAtRouteResult ("GetProducto", new{Id=producto.Id},producto);
        }
        //método para actualizar (modificar) en la tabla
        [HttpPut]
         public async Task<ActionResult<Producto>> Put(int id,Producto producto) {
          if(id != producto.Id){
            return BadRequest();
          }
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
           return Ok();
        }

        //método para eliminar registro de la tabla
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>>Delete(int id){
            var producto = await _context.Producto.FindAsync(id);
            if(producto == null){
                return NotFound();
            }
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return producto;
        }
    }
}