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
        public async Task<ActionResult<CategoriaDespesa>> GetById(int id)
        {
            CategoriaDespesa result = await _context.CategoriasDespesas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

    }
}