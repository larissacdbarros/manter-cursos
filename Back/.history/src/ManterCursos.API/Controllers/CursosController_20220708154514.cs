using System.Collections.Generic;
using System.Threading.Tasks;
using ManterCursos.API.Data;
using ManterCursos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController: ControllerBase
    {
        private readonly DataContext _context;
        public CursosController(DataContext context)
        {
            this._context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetAll()
        {
            return await _context.CategoriasCursos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(int id)
        {
            Curso result = await _context.Cursos.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Curso body)
        {
            Curso result = await _context.CategoriasCursos.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CursoId = id ;

            _context.Entry<Curso>(result).State = EntityState.Detached;
            _context.Entry<Curso>(body).State = EntityState.Modified;
            
            _context.CategoriasCursos.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> Create(Curso body)
        {
            await _context.CategoriasCursos.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Curso result = await _context.CategoriasCursos.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}