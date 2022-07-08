using System.Collections.Generic;
using System.Threading.Tasks;
using ManterCursos.API.Data;
using ManterCursos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/categoriasCursos")]
    public class CategoriasCursosController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriasCursosController(DataContext context)
        {
            this._context = context;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCurso>>> GetAll()
        {
            return await _context.CategoriasCursos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaCurso>> GetById(int id)
        {
            CategoriaCurso result = await _context.CategoriasCursos.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriaDespesa body)
        {
            CategoriaDespesa result = await _context.CategoriasDespesas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CategoriaDespesaId = id ;

            _context.Entry<CategoriaDespesa>(result).State = EntityState.Detached;
            _context.Entry<CategoriaDespesa>(body).State = EntityState.Modified;
            
            _context.CategoriasDespesas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }


    }
}