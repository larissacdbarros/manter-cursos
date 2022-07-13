using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManterCursos.API.Data;
using ManterCursos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly DataContext _context;

        public LogsController(DataContext context)
        {
            this._context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetAll()
        {
            return await _context.Logs
            .Include(log=> log.Curso)
            .ThenInclude(curso => curso.CategoriaCurso)
            .Include(log=> log.Usuario)
            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetById(int id)
        {
            Log log = await _context.Logs.FindAsync(id);

            Curso curso = await _context.Cursos.FindAsync(log.CursoId);
            log.Curso = curso;

            CategoriaCurso categoriaCurso = await _context.CategoriasCursos.FindAsync(log.Curso.CategoriaCursoId);
            log.Curso.CategoriaCurso = categoriaCurso;

            Usuario usuario = await _context.Usuarios.FindAsync(log.UsuarioId);
            log.Usuario = usuario;

            if (log== null)
            {
                return NotFound();
            }
            return log;
        }

        [HttpGet("curso/{cursoId}")]
        public async Task<ActionResult<Log>> GetByIdCurso(int cursoId)
        {

            var log = _context.Logs
                    .Include(log=> log.Curso)
                    .ThenInclude (curso => curso.CategoriaCurso)
                    .Include (log => log.Usuario)
                    .Where(log => log.CursoId == cursoId)
                    .FirstOrDefault();

            if(log == null){
                return NotFound("Log não encontrado.");
            } 

            return log;
        }

    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log result = await _context.Logs.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}